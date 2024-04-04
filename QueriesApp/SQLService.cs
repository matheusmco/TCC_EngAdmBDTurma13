public class SQLService
{
    public void Execute()
    {
        var beginTime = DateTime.Now.Ticks;
        int qty = 10;
        int[] accountsRange = { 1, qty };
        Random random = new Random();

        using var db = new SQLContext();

        // TODO: insert one transaction at a time, assyncronously
        // db.Database.ExecuteSqlRaw("DELETE FROM POCOS");
        var inserts = CreateInserts();
        ExecuteInserts();

        // TODO: execute one at a time, assynchronously
        ExecuteSelects();

        IEnumerable<SQLPOCO> CreateInserts()
        {
            double[] valuesRange = { qty * qty * -1, qty * qty };
            long[] timestampRange = { 0, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds() };
            for (int i = 0; i < qty; i++)
            {
                yield return new SQLPOCO
                {
                    AccountId = random.Next(accountsRange[0], accountsRange[1]),
                    Value = double.Round(random.NextDouble() * valuesRange[random.Next(0, 2)], 2),
                    Timestamp = random.NextInt64(timestampRange[0], timestampRange[1])
                };
            }
        }

        IEnumerable<IEnumerable<SQLPOCO>> CreateSelects()
        {
            int[] dateRanges = { 30, 45, 60, 90 };
            for (int i = 0; i < qty; i++)
            {
                var range = dateRanges[random.Next(0, dateRanges.Count())];
                var accountId = random.Next(accountsRange[0], accountsRange[1]);
                var timestamp = new DateTimeOffset(DateTime.Now.AddDays(range * -1)).ToUnixTimeSeconds();
                yield return db.POCOS.Where(_ => _.AccountId == accountId && _.Timestamp > timestamp);
            }
        }

        void ExecuteSelects()
        {
            var times = new List<long>();
            var selects = CreateSelects();
            beginTime = DateTime.Now.Ticks;
            foreach (var s in selects)
            {
                var lastTick = DateTime.Now.Ticks;
                s.ToList();
                var time = DateTime.Now.Ticks - lastTick;
                times.Add(time);
            }
            Console.WriteLine($"Finished in {TimeSpan.FromTicks(DateTime.Now.Ticks - beginTime).TotalSeconds}");
        }

        void ExecuteInserts()
        {
            db.AddRange(inserts);
            db.SaveChanges();
            Console.WriteLine($"finished insert:{TimeSpan.FromTicks(DateTime.Now.Ticks - beginTime).TotalSeconds}");
        }
    }
}