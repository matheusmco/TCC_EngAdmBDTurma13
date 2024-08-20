for($i = 0; $i -lt 10; $i++)
{
    dotnet run --project .\QueriesApp\QueriesApp.csproj select 1 mongo >> result_1_select_mongo.txt
}

for($i = 0; $i -lt 10; $i++)
{
    dotnet run --project .\QueriesApp\QueriesApp.csproj select 10 mongo >> result_10_select_mongo.txt
}

for($i = 0; $i -lt 10; $i++)
{
    dotnet run --project .\QueriesApp\QueriesApp.csproj select 100 mongo >> result_100_select_mongo.txt
}