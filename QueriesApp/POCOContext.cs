using Microsoft.EntityFrameworkCore;

public abstract class POCOContext : DbContext
{
    public DbSet<POCO> POCOS { get; init; }

    public POCOContext(DbContextOptions options)
        : base(options)
    {
    }

    public POCOContext()
    {
    }
}