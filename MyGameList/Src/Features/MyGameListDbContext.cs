using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features.Categories.Models;
using MyGameList.Src.Features.Games.Models;

namespace MyGameList.Src.Features
{
    public partial class MyGameListDbContext : DbContext
    {
        public MyGameListDbContext(DbContextOptions<MyGameListDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        //Categories
        public DbSet<AgeRating> AgeRating => Set<AgeRating>();
        public DbSet<Gender> Gender => Set<Gender>();
        public DbSet<Genre> Genre => Set<Genre>();
        public DbSet<Mode> Mode => Set<Mode>();
        public DbSet<Platform> Platform => Set<Platform>();

        //Games
        public DbSet<Game> Game => Set<Game>();
    }
}
