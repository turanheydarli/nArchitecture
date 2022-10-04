using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Contexts;

public class BaseDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }

    public DbSet<Brand> Brands { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }


    public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("PgSql")
                                     ?? throw new NullReferenceException(
                                         "Assign connection string in app settings.json"))
                .EnableSensitiveDataLogging();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(b =>
        {
            b.ToTable("Brands");
            b.Property(p => p.Id).HasColumnName("Id");
            b.Property(p => p.Name).HasColumnName("Name");

            b.HasMany(p => p.Models);
        });


        modelBuilder.Entity<Model>(m =>
        {
            m.ToTable("Models");
            m.Property(p => p.Id).HasColumnName("Id");
            m.Property(p => p.BrandId).HasColumnName("BrandId");
            m.Property(p => p.Name).HasColumnName("Name");
            m.Property(p => p.DailyPrice).HasColumnName("DailyPrice");
            m.Property(p => p.ImageUrl).HasColumnName("ImageUrl");

            m.HasOne(p => p.Brand);
        });

        Brand[] brandEntitySeeds = { new Brand { Id = 1, Name = "BMW" }, new Brand { Id = 2, Name = "Mec" } };
        modelBuilder.Entity<Brand>().HasData(brandEntitySeeds);

    }
}