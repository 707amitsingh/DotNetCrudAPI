using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_rzp.DTO.Character;
using dotnet_rzp.Models;

namespace dotnet_rzp.Services
{
    public interface ICharacterController
    {
         Task<ServiceResponse<List<GetCharacterDto>>> getAllCharacters();
         Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
         Task<ServiceResponse<List<GetCharacterDto>>> addCharacter(AddCharacterDto newCharacter);
         Task<ServiceResponse<GetCharacterDto>> updateCharacter(int Id, UpdateCharacterDto updateChar);

         Task<ServiceResponse<List<GetCharacterDto>>> deleteCharacter(int id);
    }
}