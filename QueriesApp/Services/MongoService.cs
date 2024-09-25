using MongoDB.Driver;

public class MongoService : ServiceBase<MongoPOCO>
{
    protected IMongoCollection<MongoPOCO> db { get; set; }
    public MongoService(int quantity) : base(quantity)
    {
        db = new MongoContext().POCO;
    }

    protected override void DoSelect(SelectQuery s)
    {
        var _ = db.Find(_ => _.AccountId == s.AccountId).FirstOrDefault();
    }

    protected override MongoPOCO MakePoco()
        => new MongoPOCO
        {
            AccountId = Random.Next(AccountsRange[0], AccountsRange[1]),
            Statement = [ new MongoStatement
            {
                Value = double.Round(Random.NextDouble() * ValuesRange[Random.Next(0, 2)], 2),
                Timestamp = Random.NextInt64(TimestampRange[0], TimestampRange[1])
            }]
        };

    protected override void ExecuteInserts(IEnumerable<MongoPOCO> pocos)
    {
        var beginTime = DateTime.Now.Ticks;
        db.InsertMany(pocos);
        Console.WriteLine($"{TimeSpan.FromTicks(DateTime.Now.Ticks - beginTime).TotalSeconds}");
    }
}