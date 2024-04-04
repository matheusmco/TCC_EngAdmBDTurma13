using Microsoft.EntityFrameworkCore;

public class SQLContext : POCOContext
{
    public SQLContext()
    {
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer("Server=localhost;Database=poco;User Id=SA;Password=yourStrong(!)Password;TrustServerCertificate=true");
}