$sp = (az ad sp create-for-rbac --skip-assignment  --name "http://pjmicrostore-aks-sp" | ConvertFrom-Json)
Write-Host "Created principal $sp"

az aks create --resource-group pjmicrostore --name pjmicrostore-aks --node-count 3 --service-principal $sp.appId --client-secret $sp.password --no-wait
    
az aks wait --resource-group pjmicrostore --name pjmicrostore-aks --created
az aks get-credentials --resource-group pjmicrostore --name pjmicrostore-aks
kubectl create clusterrolebinding kubernetes-dashboard --clusterrole=cluster-admin --serviceaccount=kube-system:kubernetes-dashboard

az aks enable-addons --resource-group pjmicrostore --name pjmicrostore-aks --addons http_application_routing


# Create a namespace for your ingress resources
kubectl create namespace ingress-basic
# Use Helm to deploy an NGINX ingress controller
helm install stable/nginx-ingress  --namespace ingress-basic  --set controller.replicaCount=2  --set controller.nodeSelector."beta\.kubernetes\.io/os"=linux --set defaultBackend.nodeSelector."beta\.kubernetes\.io/os"=linux

kubectl apply -f ../k8s/configs/.
kubectl apply -f ../k8s/azure_infrastructure/.
kubectl apply -f ../k8s/.
