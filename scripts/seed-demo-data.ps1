param(
    [switch]$CatalogOnly,
    [switch]$BasketOnly
)

$repoRoot = Split-Path -Parent $PSScriptRoot
$composeFile = Join-Path $repoRoot "docker-compose.yml"
$catalogSql = Join-Path $repoRoot "scripts\\seed\\catalog-demo-data.sql"
$basketSql = Join-Path $repoRoot "scripts\\seed\\basket-demo-data.sql"

function Invoke-SeedScript {
    param(
        [string]$ServiceName,
        [string]$DatabaseName,
        [string]$SqlPath
    )

    $containerId = docker compose -f $composeFile ps -q $ServiceName

    if (-not $containerId) {
        throw "Service '$ServiceName' is not running. Start it first with: docker compose up -d $ServiceName"
    }

    Get-Content -Raw $SqlPath | docker exec -i $containerId psql -U postgres -d $DatabaseName
}

if (-not $BasketOnly) {
    Write-Host "Seeding CatalogDb..."
    Invoke-SeedScript -ServiceName "catalogpostgresdb" -DatabaseName "CatalogDb" -SqlPath $catalogSql
}

if (-not $CatalogOnly) {
    Write-Host "Seeding BasketDb..."
    Invoke-SeedScript -ServiceName "basketpostgresdb" -DatabaseName "BasketDb" -SqlPath $basketSql
}

Write-Host "Done."
