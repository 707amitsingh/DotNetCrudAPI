using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rzp.Data;
using dotnet_rzp.DTO.Character;
using dotnet_rzp.DTO.Wapon;
using dotnet_rzp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rzp.Services
{
    public class WaponService : IWaponService
    {
        private DataContext _context { get; set; }
        private IHttpContextAccessor _httpContextAccessor { get; set; }
        private IMapper _mapper { get; set; }
        public WaponService(DataContext dataContext, IHttpContextAccessor httpContentAccessor, IMapper mapper)
        {
            _context = dataContext;
            _httpContextAccessor = httpContentAccessor;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetCharacterDto>> AddWapon(AddWaponDto wapon)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();

            try
            {
                Characters character = await _context.Characterss
                .FirstOrDefaultAsync(c => c.Id == wapon.CharacterId
                && c.User.Id == int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
                if (character == null)
                {
                    serviceResponse.isSuccess = true;
                    serviceResponse.message = "The character not found";
                    return serviceResponse;
                } else {
                    Wapon newWapon = new Wapon();
                    newWapon.Name = wapon.Name;
                    newWapon.Damage = wapon.Damage;
                    newWapon.Character = character;

                    await _context.Wapons.AddAsync(newWapon);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
                    serviceResponse.isSuccess = true;
                    serviceResponse.message = "Wapon added to the character";
                }
            }
            catch (Exception Ex)
            {
                serviceResponse.isSuccess = false;
                serviceResponse.message = Ex.Message;
            }

            return serviceResponse;
        }
    }
}