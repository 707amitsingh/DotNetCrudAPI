using System.Collections.Generic;
using dotnet_rzp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using dotnet_rzp.Services;
using System.Threading.Tasks;
using dotnet_rzp.DTO.Character;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace dotnet_rzp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {

        private ICharacterController _characters;

        public CharacterController(ICharacterController characterService)
        {
            _characters = characterService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll() {
            return Ok(await _characters.getAllCharacters());
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id) {
            return Ok(await _characters.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddCharacter(AddCharacterDto character) {
            return Ok(await _characters.addCharacter(character));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateChar(int id, [FromBody] UpdateCharacterDto updateChar) {
            var response = await _characters.updateCharacter(id, updateChar);
            if(response.Data == null) {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteCharacter(int id){
            var response = await _characters.deleteCharacter(id);
            if(response.Data == null)
                return NotFound(response);
            
            return Ok(response);
        }
}
}