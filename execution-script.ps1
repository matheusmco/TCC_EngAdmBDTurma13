for($i = 0; $i -lt 10; $i++)
{
    docker-compose -f .\docker-compose-mongo.yml up -d
    dotnet run --project .\QueriesApp\QueriesApp.csproj insert 10000 mongo_event >> result_10K_insert_mongo_event.txt
    docker-compose -f .\docker-compose-mongo.yml down
}

for($i = 0; $i -lt 10; $i++)
{
    docker-compose -f .\docker-compose-mongo.yml up -d
    dotnet run --project .\QueriesApp\QueriesApp.csproj insert 100000 mongo_event >> result_100K_insert_mongo_event.txt
    docker-compose -f .\docker-compose-mongo.yml down
}

for($i = 0; $i -lt 10; $i++)
{
    docker-compose -f .\docker-compose-mongo.yml up -d
    dotnet run --project .\QueriesApp\QueriesApp.csproj insert 1000000 mongo_event >> result_1M_insert_mongo_event.txt
}

for($i = 0; $i -lt 10; $i++)
{
    dotnet run --project .\QueriesApp\QueriesApp.csproj select 1 mongo_event >> result_100_select_mongo_event.txt
}

for($i = 0; $i -lt 10; $i++)
{
    dotnet run --project .\QueriesApp\QueriesApp.csproj select 10 mongo_event >> result_100_select_mongo_event.txt
}

for($i = 0; $i -lt 10; $i++)
{
    dotnet run --project .\QueriesApp\QueriesApp.csproj select 100 mongo_event >> result_100_select_mongo_event.txt
}