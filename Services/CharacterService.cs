using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rzp.Data;
using dotnet_rzp.DTO.Character;
using dotnet_rzp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rzp.Services
{
    public class CharacterService : ICharacterController
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private IHttpContextAccessor _httpContextAccessor { get; set; }
        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        private int getUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        public async Task<ServiceResponse<List<GetCharacterDto>>> addCharacter(AddCharacterDto newCharacter)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var newChar = _mapper.Map<Characters>(newCharacter);
            var id = getUserId();
            newChar.User = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);
            await _context.Characterss.AddAsync(newChar);
            await _context.SaveChangesAsync();
            
            serviceResponse.Data = _context.Characterss.Where(c => c.Id == id).Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            serviceResponse.isSuccess = true;
            serviceResponse.message = "The Character was saved";
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> getAllCharacters()
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characterss.Where(c => c.User.Id == getUserId()).ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            serviceResponse.isSuccess = true;
            serviceResponse.message = "Here is the list of all characters";
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            var userId = getUserId();
            Characters theCharacter = await _context.Characterss.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == getUserId());
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(theCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> updateCharacter(int id, UpdateCharacterDto updateChar)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                var theCharacter = await _context.Characterss.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == id);

                if (theCharacter.User.Id == getUserId())
                {
                    var tempCharacter = _mapper.Map<Characters>(updateChar);
                    theCharacter.name = tempCharacter.name;
                    theCharacter.HitPoints = tempCharacter.HitPoints;
                    theCharacter.Intelligence = tempCharacter.Intelligence;
                    theCharacter.Defence = tempCharacter.Defence;
                    theCharacter.Strength = tempCharacter.Strength;
                    theCharacter.Type = tempCharacter.Type;

                    _context.Characterss.Update(theCharacter);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _mapper.Map<GetCharacterDto>(theCharacter);
                    serviceResponse.isSuccess = true;
                    serviceResponse.message = "Updated successfully";
                } else {
                    serviceResponse.message = "Cannot find the character";
                    serviceResponse.isSuccess = false;
                }

            }
            catch (System.Exception ex)
            {
                serviceResponse.Data = null;
                serviceResponse.isSuccess = false;
                serviceResponse.message = ex.Message;
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> deleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                //Note: First throws an exception if the condition was not met.
                var temp = await _context.Characterss.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == getUserId());
                if (temp != null)
                {
                    Console.WriteLine("Found the char");
                    _context.Characterss.Remove(temp);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _context.Characterss.Where(c => c.User.Id == getUserId()).Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
                    serviceResponse.isSuccess = true;
                }
                else
                {
                    serviceResponse.message = "Character not found";
                    serviceResponse.isSuccess = false;
                }

            }
            catch (System.Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                serviceResponse.message = Ex.Message;
            }
            return serviceResponse;
        }
    }
}