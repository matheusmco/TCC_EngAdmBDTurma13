string executionType = args[0];
int quantity = int.Parse(args[1]);
string database = args[2];

switch (database)
{
    case "sql":
        new SQLService(quantity).Execute(executionType);
        break;
    case "mongo_document":
        new MongoAsDocumentService(quantity).Execute(executionType);
        break;
    case "mongo_event":
        new MongoAsEventService(quantity).Execute(executionType);
        break;
    default:
        Console.WriteLine("Invalid database");
        break;
}