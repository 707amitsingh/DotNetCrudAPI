using System.Collections.Generic;

namespace dotnet_rzp.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
        public List<CharacterSkills> characterSkills { get; set; }
    }
}