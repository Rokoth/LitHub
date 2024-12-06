using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LitHub.DB.Model
{
    public class MainDbContext: DbContext
    {
        public DbSet<Hub> Hubs { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Settings> Settings { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Hub>(new HubConfiguration());


            
        }
    }

    public class HubConfiguration : IEntityTypeConfiguration<Hub>
    {
        public void Configure(EntityTypeBuilder<Hub> builder)
        {
            builder.Property(s => s.Author).HasField("author");
            builder.Property(s => s.DateModified).HasField("date_modified");
            builder.Property(s => s.Description).HasField("description");
            builder.Property(s => s.Id).HasField("id");
            builder.Property(s => s.Name).HasField("name");
            builder.Property(s => s.VersionDate).HasField("version_date");
        }
    }

    
    public class Settings
    {        
        public int Id { get; set; }
       
        public string ParamName { get; set; }
       
        public string ParamValue { get; set; }
    }
}
