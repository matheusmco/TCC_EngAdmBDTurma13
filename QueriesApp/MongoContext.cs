using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;

public class MongoContext : POCOContext
{
    private IMongoDatabase _database {get;}

    // public static MongoContext Create()
    // {
    //     var client = new MongoClient("mongodb://mongodb:27017");

    //     var database = client.GetDatabase("pocos");

    //     return new(new DbContextOptionsBuilder<MongoContext>()
    //         .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
    //         .Options);
    // }

    // public MongoContext(DbContextOptions options)
    //     : base(options)
    // {
    // }

    public MongoContext()
    {
        var client = new MongoClient("mongodb://mongodb:27017");
        _database = client.GetDatabase("pocos");
    }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     base.OnModelCreating(modelBuilder);
    //     modelBuilder.Entity<POCO>().ToCollection("pocos");
    // }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseMongoDB(_database.Client, _database.DatabaseNamespace.DatabaseName);
}