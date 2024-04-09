using MongoDB.Driver;
using MongoDB.Driver.Linq;

public class MongoService
{
    // TODO: access database
    // TODO: create inserts
    // TODO: create selects

    public void Execute()
    {
        // move to a base class
        var beginTime = DateTime.Now.Ticks;
        int qty = 10000;
        int[] accountsRange = { 1, qty };
        Random random = new Random();

        var db = new MongoContext().POCO;

        // TODO: insert one transaction at a time, assyncronously
        // db.Database.ExecuteSqlRaw("DELETE FROM POCOS");
        var inserts = CreateInserts();
        ExecuteInserts();

        // TODO: execute one at a time, assynchronously
        ExecuteSelects();

        IEnumerable<MongoPOCO> CreateInserts()
        {
            double[] valuesRange = { -100, 100 };
            long[] timestampRange = { 0, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds() };
            for (int i = 0; i < qty; i++)
            {
                yield return new MongoPOCO
                {
                    AccountId = random.Next(accountsRange[0], accountsRange[1]),
                    Value = double.Round(random.NextDouble() * valuesRange[random.Next(0, 2)], 2),
                    Timestamp = random.NextInt64(timestampRange[0], timestampRange[1])
                };
            }
        }

        IEnumerable<SelectQuery> CreateSelects()
        {
            int[] dateRanges = { 30, 45, 60, 90 };
            for (int i = 0; i < qty; i++)
            {
                var range = dateRanges[random.Next(0, dateRanges.Count())];
                yield return new SelectQuery
                {
                    AccountId = random.Next(accountsRange[0], accountsRange[1]),
                    Timestamp = new DateTimeOffset(DateTime.Now.AddDays(range * -1)).ToUnixTimeSeconds()
                };
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
                var found = db.Find(_ => _.AccountId == s.AccountId && _.Timestamp > s.Timestamp).ToList();
                var time = DateTime.Now.Ticks - lastTick;
                times.Add(time);
                foreach(var f in found)
                    Console.WriteLine(f);
            }
            Console.WriteLine($"Finished in {TimeSpan.FromTicks(DateTime.Now.Ticks - beginTime).TotalSeconds}");
        }

        void ExecuteInserts()
        {
            db.InsertMany(inserts);
            Console.WriteLine($"finished insert:{TimeSpan.FromTicks(DateTime.Now.Ticks - beginTime).TotalSeconds}");
        }
    }

    class SelectQuery
    {
        public int AccountId { get; set; }
        public long Timestamp { get; set; }
    }
}