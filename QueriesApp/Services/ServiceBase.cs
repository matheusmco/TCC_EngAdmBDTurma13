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

    public void Execute()
    {
        // TODO: insert one transaction at a time, assyncronously
        var inserts = CreateInserts();
        ExecuteInserts(inserts);

        // TODO: execute one at a time, assynchronously
        ExecuteSelects();

        IEnumerable<SelectQuery> CreateSelects()
        {
            int[] dateRanges = { 30, 45, 60, 90 };
            for (int i = 0; i < _quantity; i++)
            {
                var range = dateRanges[Random.Next(0, dateRanges.Count())];
                yield return new SelectQuery
                {
                    AccountId = Random.Next(AccountsRange[0], AccountsRange[1]),
                    Timestamp = new DateTimeOffset(DateTime.Now.AddDays(range * -1)).ToUnixTimeSeconds()
                };
            }
        }

        void ExecuteSelects()
        {
            var times = new List<long>();

            var selects = CreateSelects();

            var beginTime = DateTime.Now.Ticks;
            foreach (var select in selects)
            {
                var lastTick = DateTime.Now.Ticks;
                DoSelect(select);
                var time = DateTime.Now.Ticks - lastTick;
                times.Add(time);
            }
            Console.WriteLine($"Finished in {TimeSpan.FromTicks(DateTime.Now.Ticks - beginTime).TotalSeconds}");
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