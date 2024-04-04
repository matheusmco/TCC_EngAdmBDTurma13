using System.ComponentModel.DataAnnotations.Schema;

public class POCO
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int AccountId { get; set; }
    public double Value { get; set; }
    public long Timestamp { get; set; }

    public override string ToString()
    {
        return $"AccountId: {AccountId}, Value: {Value}, Timestamp: {Timestamp}";
    }
}