namespace dotnet_rzp.Models
{
    public class Wapon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
        public Characters Character { get; set; }
        public int CharacterId { get; set; }
    }
}