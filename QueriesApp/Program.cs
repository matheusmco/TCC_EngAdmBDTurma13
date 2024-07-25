/* TODO:
*   - select type (insert/select) by args
*/
string executionType = args[0];
int quantity = int.Parse(args[1]);
string database = args[2];

switch (database)
{
    case "sql":
        new SQLService(quantity).Execute(executionType);
        break;
    case "mongo":
        new MongoService(quantity).Execute(executionType);
        break;
    default:
        Console.WriteLine("Invalid database");
        break;
}