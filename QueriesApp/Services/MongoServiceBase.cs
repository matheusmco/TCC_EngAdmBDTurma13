using MongoDB.Driver;

public abstract class MongoServiceBase : ServiceBase<MongoPOCO>
{
    protected IMongoCollection<MongoPOCO> db { get; set; }
    public MongoServiceBase(int quantity) : base(quantity)
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
}