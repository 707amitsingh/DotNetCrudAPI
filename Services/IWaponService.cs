using System.Threading.Tasks;
using dotnet_rzp.DTO.Character;
using dotnet_rzp.DTO.Wapon;
using dotnet_rzp.Models;

namespace dotnet_rzp.Services
{
    public interface IWaponService
    {
         Task<ServiceResponse<GetCharacterDto>> AddWapon(AddWaponDto wapon);
    }
}