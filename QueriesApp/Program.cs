// See https://aka.ms/new-console-template for more information
int qty = 10;
int[] accountsRange = { 0, qty };
double[] valuesRange = { qty * qty * -1, qty * qty };
long[] timestampRange = { 0, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds() };
Random random = new Random();
var pocos = new List<POCO>();
for (int i = 0; i < qty; i++)
{
    pocos.Add(new POCO
    {
        AccountId = random.Next(accountsRange[0], accountsRange[1]),
        Value = double.Round(random.NextDouble() * valuesRange[random.Next(0, 2)], 2),
        Timestamp = random.NextInt64(timestampRange[0], timestampRange[1])
    });
}

Console.WriteLine("Finished creating inserts");

List<POCO> repositoryMock = new List<POCO>();
int[] dateRanges = { 30, 45, 60, 90 };
List<IEnumerable<POCO>> selects = new List<IEnumerable<POCO>>();
for (int i = 0; i < qty; i++)
{
    var range = dateRanges[random.Next(0, dateRanges.Count())];
    var accountId = random.Next(accountsRange[0], accountsRange[1]);
    var timestamp = new DateTimeOffset(DateTime.Now.AddDays(range * -1)).ToUnixTimeSeconds();
    selects.Add(repositoryMock.Where(_ => _.AccountId == accountId && _.Timestamp > timestamp));
}

Console.WriteLine("Finished creating selects");

class POCO
{
    public int AccountId { get; set; }
    public double Value { get; set; }
    public long Timestamp { get; set; }
}