for($i = 0; $i -lt 10; $i++)
{
    docker-compose -f .\docker-compose-mongo.yml up -d
    dotnet run --project .\QueriesApp\QueriesApp.csproj insert 10000 mongo >> result_10k_insert_mongo.txt
    docker-compose -f .\docker-compose-mongo.yml down

}

for($i = 0; $i -lt 10; $i++)
{
    docker-compose -f .\docker-compose-mongo.yml up -d
    dotnet run --project .\QueriesApp\QueriesApp.csproj insert 100000 mongo >> result_100k_insert_mongo.txt
    docker-compose -f .\docker-compose-mongo.yml down

}

for($i = 0; $i -lt 10; $i++)
{
    docker-compose -f .\docker-compose-mongo.yml up -d
    dotnet run --project .\QueriesApp\QueriesApp.csproj insert 1000000 mongo >> result_1M_insert_mongo.txt
}

# for($i = 0; $i -lt 100; $i++)
# {
#     dotnet run --project .\QueriesApp\QueriesApp.csproj select 10000 mongo >> result_10k_select_mongo.txt
# }

# for($i = 0; $i -lt 100; $i++)
# {
#     dotnet run --project .\QueriesApp\QueriesApp.csproj select 100000 mongo >> result_100k_select_mongo.txt
# }

# for($i = 0; $i -lt 100; $i++)
# {
#     dotnet run --project .\QueriesApp\QueriesApp.csproj select 1000000 mongo >> result_1M_select_mongo.txt
# }