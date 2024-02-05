using Daw.Modells;
using DAW.Modells;
using Microsoft.EntityFrameworkCore;

namespace DAW.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Category> Categories { set; get; }

        public DbSet<SERVER> Servers { set; get; }

        public DbSet<Player> Players { get; set; }

        public DbSet<GamePlayer> GamePlayers { get; set; }

        public DbSet<GameCategory> GameCategories { get; set; }

        public DbSet<Review> Reviews {  get; set; }

        public DbSet<Reviewer> Reviewers { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameCategory>().HasKey(p => new { p.GameId, p.CategoryID });

            modelBuilder.Entity<GameCategory>().HasOne(p => p.Game)
                                               .WithMany(p => p.GameCategories)
                                               .HasForeignKey(p => p.GameId);


            modelBuilder.Entity<GameCategory>().HasOne(p => p.Category)
                                               .WithMany(p => p.GameCategories)
                                               .HasForeignKey(p => p.CategoryID);




            modelBuilder.Entity<GamePlayer>().HasKey(p => new { p.GameId, p.PlayerId });

            modelBuilder.Entity<GamePlayer>().HasOne(p => p.Game)
                                               .WithMany(p => p.GamePlayers)
                                               .HasForeignKey(p => p.GameId);


            modelBuilder.Entity<GamePlayer>().HasOne(p => p.Player)
                                               .WithMany(p => p.gamePlayers)
                                               .HasForeignKey(p => p.PlayerId);
        }
    }
}
