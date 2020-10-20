using System.Threading.Tasks;
using AutoMapper;
using dotnet_rzp.Data;
using dotnet_rzp.DTO.Character;
using dotnet_rzp.DTO.CharacterSkill;
using dotnet_rzp.Models;
using Microsoft.AspNetCore.Http;

namespace dotnet_rzp.Services
{
    public class CharacterSkillsService : ICharacterSkillsService
    {
        private IMapper _mapper { get; set; }
        private IHttpContextAccessor _httpContextAccessor { get; set; }
        private DataContext _context {get; set; }
        public CharacterSkillsService(IMapper mapper, IHttpContextAccessor httpContextAccessor, DataContext dataContext)
        {
            _context = dataContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill) {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();

            return serviceResponse;
        }
    }
}