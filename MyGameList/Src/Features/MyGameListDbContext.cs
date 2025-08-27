using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.Categories.Models;
using MyGameList.Src.Features.GameMakers.Models;
using MyGameList.Src.Features.Games.Models;

namespace MyGameList.Src.Features
{
    public partial class MyGameListDbContext : DbContext
    {
        public MyGameListDbContext(DbContextOptions<MyGameListDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        // Categories
        public DbSet<AgeRating> AgeRating => Set<AgeRating>();
        public DbSet<Gender> Gender => Set<Gender>();
        public DbSet<Genre> Genre => Set<Genre>();
        public DbSet<Mode> Mode => Set<Mode>();
        public DbSet<Platform> Platform => Set<Platform>();

        // Game Makers
        public DbSet<Developer> Developer => Set<Developer>();
        public DbSet<Person> Person => Set<Person>();
        public DbSet<Publisher> Publisher => Set<Publisher>();

        // Games
        public DbSet<Game> Game => Set<Game>();

        // Users

        // Characters

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasOne(p => p.Gender)
                .WithMany(g => g.People)
                .HasForeignKey(p => p.GenderId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
