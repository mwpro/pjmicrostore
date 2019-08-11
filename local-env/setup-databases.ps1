Write-Host "Execute migrations"
dotnet ef database update -p ..\src\Checkout\Checkout.Orders\
dotnet ef database update -p ..\src\Checkout\Checkout.Cart\
dotnet ef database update -p ..\src\Checkout\Checkout.Payments\
dotnet ef database update -p ..\src\Products\Products.Catalog\
dotnet ef database update -p ..\src\Products\Products.Photos\
dotnet ef database update -p ..\src\Identity\Identity.Api\
dotnet run /seed --project ..\src\Identity\Identity.Api\

# todo if k8s -> kubectl port-forward deployment/sqlserver  1433:1433  -n infrastructure
Write-Host "Insert sample products"
sqlcmd -i .\sample-products.sql -s 127.0.0.1 -U sa -P sqlDevPassw0rd -d Products.Catalog -f 65001

Write-Host "Setup Elasticsearch index"
$elasticProductsModel = Get-Content .\products-model.json
# todo if k8s -> kubectl port-forward deployment/elasticsearch 9200:9200  -n infrastructure
Invoke-RestMethod -Uri http://localhost:9200/products -Method Put -Body $elasticProductsModel -ContentType "application/json"

# todo if k8s -> kubectl port-forward deployment/search 80:80 -n products
Invoke-RestMethod -Uri http://127.0.0.1:80/api/search/import -Method Get

Write-Host "Setup local Azure storage"
az storage container create --name 'productsphotos' --connection-string 'DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;'