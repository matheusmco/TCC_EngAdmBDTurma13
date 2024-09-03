using MongoDB.Driver;

public class MongoAsEventService : MongoServiceBase
{
    public MongoAsEventService(int quantity) : base(quantity)
    {
    }

    protected override void ExecuteInserts(IEnumerable<MongoPOCO> pocos)
    {
        var beginTime = DateTime.Now.Ticks;
        db.InsertMany(pocos);
        Console.WriteLine($"{TimeSpan.FromTicks(DateTime.Now.Ticks - beginTime).TotalSeconds}");
    }
}