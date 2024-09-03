for($i = 0; $i -lt 10; $i++)
{
    dotnet run --project .\QueriesApp\QueriesApp.csproj select 1 mongo_event >> result_1_select_mongo_event.txt
}

for($i = 0; $i -lt 10; $i++)
{
    dotnet run --project .\QueriesApp\QueriesApp.csproj select 10 mongo_event >> result_10_select_mongo_event.txt
}

for($i = 0; $i -lt 10; $i++)
{
    dotnet run --project .\QueriesApp\QueriesApp.csproj select 100 mongo_event >> result_100_select_mongo_event.txt
}