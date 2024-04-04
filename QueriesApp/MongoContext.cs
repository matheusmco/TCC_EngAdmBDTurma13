using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;

public class MongoContext
{
    // TODO: modify to access mongo database
    public DbSet<SQLPOCO> POCO { get; init; }
    public static MongoContext Create(IMongoDatabase database) =>
        new(new DbContextOptionsBuilder<MongoContext>()
            .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
            .Options);
    public MongoContext(DbContextOptions options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<SQLPOCO>().ToCollection("pocos");
    }
}