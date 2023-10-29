$ErrorActionPreference = "Stop"

$DEBUG_CONTAINER_ENDPOINT = "https://$env:BINDING_DEBUG_CONTAINER_FQDN/"
$SERVICE_BINDING_DB_CONNECTION_STRING = ((Invoke-RestMethod -Headers @{Authorization = "Bearer $env:BINDING_DEBUG_CONTAINER_SHARED_KEY"} -Uri $DEBUG_CONTAINER_ENDPOINT).POSTGRES_CONNECTION_STRING -replace 'host=', 'Host=' -replace 'database=', 'Database=' -replace 'user=', 'Username=' -replace 'password=', 'Password=' -replace ' ', ';')
azd env set SERVICE_BINDING_DB_CONNECTION_STRING "${SERVICE_BINDING_DB_CONNECTION_STRING}"