# Setup

- install docker desktop: https://www.docker.com/products/docker-desktop/
- execute compose for database: `docker-compose -f docker-compose-<database>.yml`

* for SQL Server
- install dotnet ef: `dotnet tool install --global dotnet-ef`
- migrate up `dotnet ef --project QueriesApp/ database update`

* for MongoDB
- setup database

# Running
`dotnet run --project .\QueriesApp\QueriesApp.csproj <operation> <quantity> <database>`
where `database`:
    - `sql` for SQL Server
    - `mongo` for MongoDB