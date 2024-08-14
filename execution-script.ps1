for($i = 0; $i -lt 100; $i++)
{
    dotnet run --project .\QueriesApp\QueriesApp.csproj select 10000 mongo >> result_10k_select_mongo.txt
}

for($i = 0; $i -lt 100; $i++)
{
    dotnet run --project .\QueriesApp\QueriesApp.csproj select 100000 mongo >> result_100k_select_mongo.txt
}

for($i = 0; $i -lt 100; $i++)
{
    dotnet run --project .\QueriesApp\QueriesApp.csproj select 1000000 mongo >> result_1M_select_mongo.txt
}