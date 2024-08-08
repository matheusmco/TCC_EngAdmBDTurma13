param ($timesToExecute = 1, $executionType = 'select', $database = 'sql')

dotnet run --project .\QueriesApp\QueriesApp.csproj $executionType $timesToExecute $database | Out-File -FilePath .\results.txt