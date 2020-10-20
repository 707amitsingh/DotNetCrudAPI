namespace dotnet_rzp.Models
{
    public class Characters
    {
        public int Id { get; set; }
        public string name { get; set; } = "Frodo";
        public int Strength { get; set; } = 10;
        public int HitPoints { get; set; } = 100;
        public int Defence { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public CharType Type { get; set; } = CharType.fireType;
        public User User { get; set; }
        public Wapon Wapon { get; set; }
    }
}