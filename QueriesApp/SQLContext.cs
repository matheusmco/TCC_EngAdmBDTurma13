using Microsoft.EntityFrameworkCore;

public class SQLContext : DbContext
{
    public string DbPath { get; }
    public DbSet<POCO> POCOS { get; set; }
    public SQLContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "test.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer("Server=localhost;Database=poco;User Id=SA;Password=yourStrong(!)Password;TrustServerCertificate=true");
}