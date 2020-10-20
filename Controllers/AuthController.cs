using System.Threading.Tasks;
using dotnet_rzp.Data;
using dotnet_rzp.DTO.User;
using dotnet_rzp.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rzp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {

        private IAuthRepository _authRepo;
        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto user){
            var response = await _authRepo.RegisterUser(
                new User { UserName = user.username},
                user.password
            );
            if(response.isSuccess == false)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("Login")]
        public async Task<IActionResult> Login(UserLoginDto user){
            var response = await _authRepo.Login(user.username, user.password);
            if(response.isSuccess)
                return Ok(response);
            return BadRequest(response);
        }
    }
}