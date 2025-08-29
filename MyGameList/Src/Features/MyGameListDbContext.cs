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
                .HasOne(person => person.Gender)
                .WithMany(gender => gender.Peoples)
                .HasForeignKey(person => person.GenderId);
            modelBuilder.Entity<Game>()
                .HasOne(game => game.AgeRating)
                .WithMany(ageRating => ageRating.Games)
                .HasForeignKey(game => game.AgeRatingId);
            modelBuilder.Entity<Game>()
                .HasMany(game => game.Developers)
                .WithMany(developer => developer.Games)
                .UsingEntity(join => join.ToTable("game_developers"));
            modelBuilder.Entity<Game>()
                .HasMany(game => game.Publishers)
                .WithMany(publisher => publisher.Games)
                .UsingEntity(join => join.ToTable("game_publishers"));
            modelBuilder.Entity<Game>()
                .HasMany(game => game.Producers)
                .WithMany(producer => producer.Games)
                .UsingEntity(join => join.ToTable("game_producers"));
            modelBuilder.Entity<Game>()
                .HasMany(game => game.Genres)
                .WithMany(genre => genre.Games)
                .UsingEntity(join => join.ToTable("game_genres"));
            modelBuilder.Entity<Game>()
                .HasMany(game => game.Modes)
                .WithMany(mode => mode.Games)
                .UsingEntity(join => join.ToTable("game_modes"));
            modelBuilder.Entity<Game>()
                .HasMany(game => game.Platforms)
                .WithMany(platform => platform.Games)
                .UsingEntity(join => join.ToTable("game_platforms"));

            base.OnModelCreating(modelBuilder);
        }
    }
}
