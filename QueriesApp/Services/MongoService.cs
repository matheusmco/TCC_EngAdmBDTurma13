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
        var _ = db.Find(_ => _.AccountId == s.AccountId).FirstOrDefault();
    }

    protected override void ExecuteInserts(IEnumerable<MongoPOCO> pocos)
    {
        var inserts = GroupPocosByAccountId(pocos);

        var newDocuments = new List<MongoPOCO>();
        var updates = new List<(FilterDefinition<MongoPOCO>, UpdateDefinition<MongoPOCO>)>();
        
        var beginTime = DateTime.Now.Ticks;
        foreach (var i in inserts)
        {
            var document = db.Find(_ => _.AccountId == i.AccountId).FirstOrDefault();
            if (document == null)
                newDocuments.Add(i);
            else
                updates.Add(UpdatePoco(i, document));
        }

        InsertNewPocos(newDocuments);
        
        foreach(var u in updates)
            db.UpdateOne(u.Item1, u.Item2);

        Console.WriteLine($"{TimeSpan.FromTicks(DateTime.Now.Ticks - beginTime).TotalSeconds}");
    }

    private static IEnumerable<MongoPOCO> GroupPocosByAccountId(IEnumerable<MongoPOCO> pocos)
        => from i in pocos
               group i by i.AccountId into g
               select new MongoPOCO { AccountId = g.Key, Statement = g.SelectMany(_ => _.Statement).ToList() };

    private (FilterDefinition<MongoPOCO>, UpdateDefinition<MongoPOCO>) UpdatePoco(MongoPOCO i, MongoPOCO document)
    {
        var statement = document.Statement;
        statement.AddRange(i.Statement);
        var filter = Builders<MongoPOCO>.Filter.Eq(_ => _.AccountId, i.AccountId);
        var update = Builders<MongoPOCO>.Update.Set(_ => _.Statement, statement);
        return (filter, update);
    }

    private void InsertNewPocos(List<MongoPOCO> newDocuments)
    {
        if (newDocuments.Any())
            db.InsertMany(newDocuments);
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