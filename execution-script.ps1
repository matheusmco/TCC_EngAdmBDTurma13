param ($itemsNumber = 1, $executionType = 'select', $database = 'sql')

for($i = 0; $i -lt 100; $i++)
{
    dotnet run --project .\QueriesApp\QueriesApp.csproj $executionType $itemsNumber $database >> result.txt
}