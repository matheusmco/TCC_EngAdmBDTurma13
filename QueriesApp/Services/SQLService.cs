public class SQLService : ServiceBase<SQLPOCO>
{
    private SQLContext db { get; set; }
    public SQLService(int quantity) : base(quantity)
    {
        db = new SQLContext();
    }

    protected override void DoSelect(SelectQuery select)
    {
        var found = db.POCOS.Where(_ => _.AccountId == select.AccountId && _.Timestamp > select.Timestamp);
        foreach (var f in found)
            Console.WriteLine(f);
    }

    protected override void ExecuteInserts(IEnumerable<SQLPOCO> inserts)
    {
        var beginTime = DateTime.Now.Ticks;
        db.AddRange(inserts);
        db.SaveChanges();
        Console.WriteLine($"Finished in {TimeSpan.FromTicks(DateTime.Now.Ticks - beginTime).TotalSeconds}");
    }

    protected override SQLPOCO MakePoco()
        => new SQLPOCO
        {
            AccountId = Random.Next(AccountsRange[0], AccountsRange[1]),
            Value = double.Round(Random.NextDouble() * ValuesRange[Random.Next(0, 2)], 2),
            Timestamp = Random.NextInt64(TimestampRange[0], TimestampRange[1])
        };
}