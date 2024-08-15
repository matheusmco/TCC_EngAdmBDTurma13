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
        var found = db.Find(_ => _.AccountId == s.AccountId).ToList();
    }

    protected override void ExecuteInserts(IEnumerable<MongoPOCO> inserts)
    {
        var beginTime = DateTime.Now.Ticks;
        var newDocuments = new List<MongoPOCO>();
        foreach (var i in inserts)
        {
            var document = db.Find(_ => _.AccountId == i.AccountId).First();
            if (document == null)
                newDocuments.Add(i);
            else
            {
                var statement = document.Statement;
                statement.Add(i.Statement.First());
                var filter = Builders<MongoPOCO>.Filter.Eq(_ => _.AccountId, i.AccountId);
                var update = Builders<MongoPOCO>.Update.Set(_ => _.Statement, statement);
                db.UpdateMany(filter, update);
            }
        }
        db.InsertMany(newDocuments);
        Console.WriteLine($"{TimeSpan.FromTicks(DateTime.Now.Ticks - beginTime).TotalSeconds}");
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
}