using Microsoft.EntityFrameworkCore;

namespace FirstApp.DataContext;
public class AppDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public AppDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
    }

    public DbSet<ClassInfo> ClassInfos { get; set; }
    public DbSet<Faculty> Faculties { get; set; }

}