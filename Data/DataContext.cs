using dotnet_rzp.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rzp.Data
{
    // An instance of DbContext provides a session with the database
    // That means you can query data using some direct methods
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        // The name of the DbSet will be the name of the table in the database
        // Here i have spelled it as Characterss just to verify it using SSMS.

        // Whenever you want to see a representation of your model in the database - you need to create a DbSet
        // that's how Entity framework know what table it should create
        public DbSet<Characters> Characterss { get; set; }

        // After this you will have to provide a connection string in you appsetting.json
        // and provide your DbContext in the service collection

        public DbSet<User> Users { get; set; }
        public DbSet<Wapon> Wapons { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<CharacterSkills> CharacterSkills { get; set; }

        // The below method is required for many to many relationships - This takes an modelbulder
        // argument that defines the shape of the entities, the relation between them and how they map
        // to the Database. 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CharacterSkills>()
            .HasKey(cs => new {cs.CharacterId, cs.SkillId});
        }
    }
}