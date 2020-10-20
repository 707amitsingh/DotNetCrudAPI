using System.Collections.Generic;

namespace dotnet_rzp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] passwordHash { get; set; }
        public byte[] salt { get; set; }
        public List<Characters> Characters { get; set; }
    }
}