using System.Collections.Generic;
using dotnet_rzp.DTO.Skill;
using dotnet_rzp.DTO.Wapon;
using dotnet_rzp.Models;

namespace dotnet_rzp.DTO.Character
{
    public class GetCharacterDto
    {
        public int Id { get; set; }
        public string name { get; set; } = "Frodo";
        public int Strength { get; set; } = 10;
        public int HitPoints { get; set; } = 100;
        public int Defence { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public CharType Type { get; set; } = CharType.fireType;
        public GetWaponDto Wapon { get; set; }
        public List<GetSkillDto> Skilles { get; set; }
    }
}