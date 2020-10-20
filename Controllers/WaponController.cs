using System.Threading.Tasks;
using dotnet_rzp.DTO.Wapon;
using dotnet_rzp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rzp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WaponController : ControllerBase
    {
        public IWaponService _waponService { get; set; }
        public WaponController(IWaponService waponService)
        {
            _waponService = waponService;
        }

        [HttpPost]
        public async Task<IActionResult> AddWapon(AddWaponDto newWapon) {
            return Ok(await _waponService.AddWapon(newWapon));
        }
    }
}