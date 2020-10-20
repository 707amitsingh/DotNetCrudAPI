namespace dotnet_rzp.Models
{
    public class CharacterSkills
    {
        public int CharacterId { get; set; }
        public int SkillId { get; set; }
        public Skill skill { get; set; }
        public Characters character { get; set; }
    }
}