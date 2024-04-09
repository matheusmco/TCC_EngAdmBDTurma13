using MongoDB.Driver;

public class MongoContext
{
    // TODO: modify to access mongo database
    public IMongoCollection<MongoPOCO> POCO { get; init; }


    public MongoContext()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("poco_db");
        POCO = database.GetCollection<MongoPOCO>("pocos");
    }
}