using CheapGames.Models;
using Microsoft.EntityFrameworkCore;

namespace CheapGames.Data
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Game> Games { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<FavoriteGame> FavoriteGames { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FavoriteGame>()
                .HasKey(fg => new { fg.UserId, fg.GameId });

            modelBuilder.Entity<FavoriteGame>()
                .HasOne(fg => fg.User)
                .WithMany(u => u.FavoriteGames)
                .HasForeignKey(fg => fg.UserId);

            modelBuilder.Entity<FavoriteGame>()
                .HasOne(fg => fg.Game)
                .WithMany(g => g.FavoriteGames)
                .HasForeignKey(fg => fg.GameId);
        }
    }
}
