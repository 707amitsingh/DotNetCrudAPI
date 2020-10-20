using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using dotnet_rzp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace dotnet_rzp.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public AuthRepository(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<bool> isRegisted(string username)
        {
            var isExisting = await _context.Users.AnyAsync(c => c.UserName.ToLower() == username.ToLower());
            if (isExisting)
                return true;
            return false;
        }

        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var serviceResponse = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(c => c.UserName.ToLower().Equals(username.ToLower()));
            if(user == null){
                serviceResponse.isSuccess = false;
                serviceResponse.message = "Unable to find the user";
                return serviceResponse;
            } else if(!checkPassword(password, user.passwordHash, user.salt)){
                serviceResponse.isSuccess = false;
                serviceResponse.message = "Invalid creadientials";
                return serviceResponse;
            } else {
                serviceResponse.Data = createToken(user);
                serviceResponse.isSuccess = true;
                serviceResponse.message = "Login successfull";
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<int>> RegisterUser(User user, string password)
        {
            var serviceResponse = new ServiceResponse<int>();
            var isExisting = await isRegisted(user.UserName);
            if (isExisting == true)
            {
                serviceResponse.isSuccess = false;
                serviceResponse.message = "Username is taken";
                return serviceResponse;
            }

            createPasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.passwordHash = passwordHash;
            user.salt = passwordSalt;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            serviceResponse.Data = user.Id;
            serviceResponse.isSuccess = true;
            return serviceResponse;
        }

        private void createPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool checkPassword(string password, byte[] passwordHash, byte[] passwordSalt) {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)) {
                var userInput = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0; i < userInput.Length; i++){
                    if(userInput[i] != passwordHash[i])
                        return false;
                }
                return true;
            }
        }

        private string createToken(User user) {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}