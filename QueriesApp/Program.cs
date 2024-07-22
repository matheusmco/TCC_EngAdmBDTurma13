/* TODO:
*   - select type (insert/select) by args
*/
int quantity = int.Parse(args[0]);
string database = args[1];

if(database == "sql")
    new SQLService(quantity).Execute();
else if (database == "mongo")
    new MongoService(quantity).Execute();
else
    Console.WriteLine("No valid database informed");