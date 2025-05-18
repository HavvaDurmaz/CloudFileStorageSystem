using FileMetadataAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileMetadataAPI.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Domain.Entities.File> Files { get; set; }
        public DbSet<Domain.Entities.FileShare> FileShares { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entities.File>().ToTable("files");
            modelBuilder.Entity<Domain.Entities.FileShare>()
                .HasOne(fs => fs.File)
                .WithMany(f => f.FileShares)
                .HasForeignKey(fs => fs.FileId);
        }
    }
}


