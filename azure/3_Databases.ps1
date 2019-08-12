Add-Type -AssemblyName System.web

$sqlUserName = "sqlroot"
$sqlPassword = "$([System.Web.Security.Membership]::GeneratePassword(20,0).Replace(';','L').Replace('&','$').Replace('''', 'x').Replace('|', 'q'))"
Write-Host "Using password $sqlPassword"

az sql server create -g pjmicrostore -n pjmicrostore-sql --location westeurope -u $sqlUserName -p "$sqlPassword"

az sql server firewall-rule create -g pjmicrostore -s pjmicrostore-sql -n allow-azure-services --start-ip-address 0.0.0.0 --end-ip-address 0.0.0.0

az sql elastic-pool create --name pjmicrostore-sql-sp --resource-group pjmicrostore --server pjmicrostore-sql --edition Basic --capacity 50

az sql db create --name Checkout.Cart --resource-group pjmicrostore --server pjmicrostore-sql --elastic-pool pjmicrostore-sql-sp
az sql db create --name Products.Catalog --resource-group pjmicrostore --server pjmicrostore-sql --elastic-pool pjmicrostore-sql-sp
az sql db create --name Identity --resource-group pjmicrostore --server pjmicrostore-sql --elastic-pool pjmicrostore-sql-sp
az sql db create --name Checkout.Orders --resource-group pjmicrostore --server pjmicrostore-sql --elastic-pool pjmicrostore-sql-sp
az sql db create --name Checkout.Payments --resource-group pjmicrostore --server pjmicrostore-sql --elastic-pool pjmicrostore-sql-sp
az sql db create --name Products.Photos --resource-group pjmicrostore --server pjmicrostore-sql --elastic-pool pjmicrostore-sql-sp

kubectl create secret generic db-user-pass --from-literal=db_username=$sqlUserName --from-literal=db_password="$sqlPassword" --from-literal=db_server=pjmicrostore-sql.database.windows.net -n checkout
kubectl create secret generic db-user-pass --from-literal=db_username=$sqlUserName --from-literal=db_password="$sqlPassword" --from-literal=db_server=pjmicrostore-sql.database.windows.net -n common
kubectl create secret generic db-user-pass --from-literal=db_username=$sqlUserName --from-literal=db_password="$sqlPassword" --from-literal=db_server=pjmicrostore-sql.database.windows.net -n external
kubectl create secret generic db-user-pass --from-literal=db_username=$sqlUserName --from-literal=db_password="$sqlPassword" --from-literal=db_server=pjmicrostore-sql.database.windows.net -n identity
kubectl create secret generic db-user-pass --from-literal=db_username=$sqlUserName --from-literal=db_password="$sqlPassword" --from-literal=db_server=pjmicrostore-sql.database.windows.net -n products
kubectl create secret generic db-user-pass --from-literal=db_username=$sqlUserName --from-literal=db_password="$sqlPassword" --from-literal=db_server=pjmicrostore-sql.database.windows.net -n infrastructure