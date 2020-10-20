using AutoMapper;
using dotnet_rzp.DTO.Character;
using dotnet_rzp.DTO.Wapon;
using dotnet_rzp.Models;

namespace dotnet_rzp
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Characters, GetCharacterDto>();
            CreateMap<AddCharacterDto, Characters>();
            CreateMap<UpdateCharacterDto, Characters>();
            CreateMap<Wapon, GetWaponDto>();
        }
    }
}