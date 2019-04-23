Write-Host "Execute migrations"
dotnet ef database update -p ..\src\Checkout\Checkout.Orders\
dotnet ef database update -p ..\src\Checkout\Checkout.Cart\
dotnet ef database update -p ..\src\Checkout\Checkout.Payments\
dotnet ef database update -p ..\src\Products\Products.Catalog\

Write-Host "Insert sample products"
sqlcmd -i .\sample-products.sql -s localhost -U sa -P sqlDevPassw0rd -d Products.Catalog

Write-Host "Setup Elasticsearch index"
$elasticProductsModel = Get-Content .\products-model.json
Invoke-RestMethod -Uri http://localhost:9200/products -Method Put -Body $elasticProductsModel -ContentType "application/json"
