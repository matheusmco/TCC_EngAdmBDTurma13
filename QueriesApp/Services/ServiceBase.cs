public abstract class ServiceBase<T>
{
    private int _quantity { get; set; }

    protected int[] AccountsRange { get; set; }
    protected double[] ValuesRange = { -100, 100 };
    protected long[] TimestampRange = { 0, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds() };
    protected Random Random = new Random();

    public ServiceBase(int quantity)
    {
        _quantity = quantity;
        AccountsRange = [1, quantity];
    }

    public void Execute(string executionType)
    {
        switch(executionType)
        {
            case "insert":
                var inserts = CreateInserts();
                ExecuteInserts(inserts);
                break;
            case "select":
                ExecuteSelects();
                break;
            default:
                Console.WriteLine("Invalid execution type");
                break;
        }
    }

    private void ExecuteSelects()
    {
        var selects = CreateSelects();

        var beginTime = DateTime.Now.Ticks;
        foreach (var select in selects)
            DoSelect(select);
        Console.WriteLine($"{TimeSpan.FromTicks(DateTime.Now.Ticks - beginTime).TotalSeconds}");

        IEnumerable<SelectQuery> CreateSelects()
        {
            for (int i = 0; i < _quantity; i++)
            {
                yield return new SelectQuery
                {
                    AccountId = Random.Next(AccountsRange[0], AccountsRange[1])
                };
            }
        }
    }

    protected IEnumerable<T> CreateInserts()
    {
        for (int i = 0; i < _quantity; i++)
            yield return MakePoco();
    }

    protected abstract void ExecuteInserts(IEnumerable<T> inserts);

    protected abstract T MakePoco();
    protected abstract void DoSelect(SelectQuery select);

    protected class SelectQuery
    {
        public int AccountId { get; set; }
        public long Timestamp { get; set; }
    }
}