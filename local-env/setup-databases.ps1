Write-Host "Execute migrations"
dotnet ef database update -p ..\src\Checkout\Checkout.Orders\
dotnet ef database update -p ..\src\Checkout\Checkout.Cart\
dotnet ef database update -p ..\src\Checkout\Checkout.Payments\
dotnet ef database update -p ..\src\Products\Products.Catalog\
dotnet ef database update -p ..\src\Products\Products.Photos\

Write-Host "Insert sample products"
sqlcmd -i .\sample-products.sql -s localhost -U sa -P sqlDevPassw0rd -d Products.Catalog -f 65001

Write-Host "Setup Elasticsearch index"
$elasticProductsModel = Get-Content .\products-model.json
Invoke-RestMethod -Uri http://localhost:9200/products -Method Put -Body $elasticProductsModel -ContentType "application/json"

Write-Host "Setup local Azure storage"
az storage container create --name 'productsphotos' --connection-string 'DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;'