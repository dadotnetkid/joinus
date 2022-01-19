using Microsoft.EntityFrameworkCore;

namespace Fleet.Files.Repository
{
    public class FileDbContext : DbContext
    {
        public FileDbContext(DbContextOptions<FileDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FileDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Entities.Files> Files { get; set; }
    }
}
