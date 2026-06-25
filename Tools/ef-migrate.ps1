param(
    [Parameter(Mandatory=$true)]
    [string]$MigrationName,
	
	[Parameter(Mandatory=$false)]
    [switch]$Migrate,

    [Parameter(Mandatory=$false)]
    [switch]$UpdateDatabase,

    [Parameter(Mandatory=$false)]
    [switch]$Scaffold
)

$Infrastructure = "Finervo.Infrastructure"
$API = "Finervo.API"
$ConnectionString = "Host=localhost;Port=5432;Database=finervo;Username=postgres;Password=p@55w0rd"

Write-Host "Finervo EF Core Tool" -ForegroundColor Cyan

if ($Scaffold)
{
    Write-Host "Scaffolding from database..." -ForegroundColor Yellow

    dotnet ef dbcontext scaffold $ConnectionString `
        Npgsql.EntityFrameworkCore.PostgreSQL `
        --project $Infrastructure `
        --startup-project $API `
        --output-dir Persistence/Entities `
        --context FinervoDbContext `
        --context-dir Persistence `
        --force `
        --no-onconfiguring

    Write-Host "Scaffold complete" -ForegroundColor Green
    exit
}

if($Migrate)
{
	Write-Host "Adding migration: $MigrationName" -ForegroundColor Yellow

	dotnet ef migrations add $MigrationName `
		--project $Infrastructure `
		--startup-project $API `
		--output-dir Persistence/Migrations

	if ($LASTEXITCODE -ne 0)
	{
		Write-Host "Migration failed" -ForegroundColor Red
		exit 1
	}

	Write-Host "Migration added successfully" -ForegroundColor Green
}

if ($UpdateDatabase)
{
    Write-Host "Updating database..." -ForegroundColor Yellow

    dotnet ef database update `
        --project $Infrastructure `
        --startup-project $API

    if ($LASTEXITCODE -ne 0)
    {
        Write-Host "Database update failed" -ForegroundColor Red
        exit 1
    }

    Write-Host "Database updated successfully" -ForegroundColor Green
}
else
{
    Write-Host "Run with -UpdateDatabase to apply migration" -ForegroundColor Yellow
}