using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class SQLPOCO
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int AccountId { get; set; }
    public double Value { get; set; }
    public long Timestamp { get; set; }
}

public class MongoPOCO
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public int AccountId { get; set; }
    public double Value { get; set; }
    public long Timestamp { get; set; }
}