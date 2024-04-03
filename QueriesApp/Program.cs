// See https://aka.ms/new-console-template for more information
int qty = 10;
int[] accountsRange = { 0, 10 };
double[] valuesRange = { -100, 100 };
long[] timestampRange = { 0, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds() };
Random random = new Random();
var pocos = new List<POCO>();
for (int i = 0; i < qty; i++)
{
    pocos.Add(new POCO
    {
        // TODO: add accountId to hash to use in selects
        AccountId = random.Next(accountsRange[0], accountsRange[1]),
        Value = double.Round(random.NextDouble() * valuesRange[random.Next(0, 2)], 2),
        Timestamp = random.NextInt64(timestampRange[0], timestampRange[1])
    });
}

Console.WriteLine("Finished creating inserts");

// TODO: create selects
/*
*   use randomized accounts from hashset
*   randomize ranges (30, 45, 60, 90)
*/

class POCO
{
    public int AccountId { get; set; }
    public double Value { get; set; }
    public long Timestamp { get; set; }
}