az storage account create -n pjmicrostorestorage -g pjmicrostore -l westeurope --sku Standard_LRS

az storage container create --name 'productsphotos' --account-name pjmicrostorestorage --public-access blob

$connectionString = "(az storage account show-connection-string -g pjmicrostore -n pjmicrostorestorage | ConvertFrom-Json).connectionString"

kubectl create secret generic photo-storage --from-literal=photo_storage=$connectionString -n products