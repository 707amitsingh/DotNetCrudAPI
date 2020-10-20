using dotnet_rzp.Models;

namespace dotnet_rzp.DTO.Character
{
    public class UpdateCharacterDto
    {
        public string name { get; set; } = "Frodo";
        public int Strength { get; set; } = 10;
        public int HitPoints { get; set; } = 100;
        public int Defence { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public CharType Type { get; set; } = CharType.fireType;
    }
}