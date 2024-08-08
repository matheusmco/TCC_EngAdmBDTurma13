using MongoDB.Driver;

public class MongoService : ServiceBase<MongoPOCO>
{
    private IMongoCollection<MongoPOCO> db { get; set; }
    public MongoService(int quantity) : base(quantity)
    {
        db = new MongoContext().POCO;
    }

    protected override void DoSelect(SelectQuery s)
    {
        var found = db.Find(_ => _.AccountId == s.AccountId && _.Timestamp > s.Timestamp).ToList();
        foreach (var f in found)
            Console.WriteLine(f);
    }

    protected override void ExecuteInserts(IEnumerable<MongoPOCO> inserts)
    {
        var beginTime = DateTime.Now.Ticks;
        db.InsertMany(inserts);
        Console.WriteLine($"{TimeSpan.FromTicks(DateTime.Now.Ticks - beginTime).TotalSeconds}");
    }

    protected override MongoPOCO MakePoco()
        => new MongoPOCO
        {
            AccountId = Random.Next(AccountsRange[0], AccountsRange[1]),
            Value = double.Round(Random.NextDouble() * ValuesRange[Random.Next(0, 2)], 2),
            Timestamp = Random.NextInt64(TimestampRange[0], TimestampRange[1])
        };
}