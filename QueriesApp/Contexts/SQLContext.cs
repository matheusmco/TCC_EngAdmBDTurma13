using Microsoft.EntityFrameworkCore;

public class SQLContext : DbContext
{
    public DbSet<SQLPOCO> POCOS { get; init; }

    public SQLContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer("Server=localhost;Database=poco;User Id=SA;Password=yourStrong(!)Password;TrustServerCertificate=true");
}