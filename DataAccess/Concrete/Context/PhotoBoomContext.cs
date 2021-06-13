using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.Context
{
    public class PhotoBoomContext : DbContext
    {
        const string connectionString = "Server=.\\SQLEXPRESS;Database=PhotoBoom;Trusted_Connection=true";

        public PhotoBoomContext() : base() { }

        public PhotoBoomContext(DbContextOptions<PhotoBoomContext> options) : base(options) { }
        public DbSet<User> User { get; set; }
        public DbSet<Photo> Photo { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Photo>()
                 .HasOne<User>(s => s.User)
                 .WithMany(g => g.Photos)
                .HasForeignKey(s => s.UserId);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
