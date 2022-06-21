namespace main.Helpers;

using Microsoft.EntityFrameworkCore;
using main.Models;

public class MainDBContext : DbContext
{
    protected readonly IConfiguration _configuration;
    private ILogger<MainDBContext> _logger;
    public MainDBContext(IConfiguration configuration, ILogger<MainDBContext> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnectionString");
        _logger.LogInformation(connectionString);
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }

    public DbSet<Product> Products { get; set; }
}