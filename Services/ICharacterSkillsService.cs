using System.Threading.Tasks;
using dotnet_rzp.DTO.Character;
using dotnet_rzp.DTO.CharacterSkill;
using dotnet_rzp.Models;

namespace dotnet_rzp.Services
{
    public interface ICharacterSkillsService
    {
        Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto characterSkill);
    }
}