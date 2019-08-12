$sp = (az ad sp create-for-rbac --skip-assignment  --name "http://pjmicrostore-aks-sp" | ConvertFrom-Json)
Write-Host "Created principal $sp"

$cluster = (az aks create --resource-group pjmicrostore --name pjmicrostore-aks --node-count 3 --service-principal $sp.appId --client-secret $sp.password --enable-addons http_application_routing | ConvertFrom-Json)

$clusterPublicUrl = $cluster.addonProfiles.httpApplicationRouting.config.HTTPApplicationRoutingZoneName
Write-Host "Cluster domain $clusterPublicUrl"

az aks get-credentials --resource-group pjmicrostore --name pjmicrostore-aks
kubectl create clusterrolebinding kubernetes-dashboard --clusterrole=cluster-admin --serviceaccount=kube-system:kubernetes-dashboard

kubectl apply -f ../k8s/.
kubectl apply -f ../k8s/azure/.

kubectl create configmap external-urls -n external --from-literal=admin=http://admin.$clusterPublicUrl --from-literal=login=http://login.$clusterPublicUrl --from-literal=store=http://store.$clusterPublicUrl
kubectl create configmap external-urls -n identity --from-literal=admin=http://admin.$clusterPublicUrl --from-literal=login=http://login.$clusterPublicUrl --from-literal=store=http://store.$clusterPublicUrl

(Get-Content -path ..\k8s\azure-templated\admin-ingress.yaml -Raw) -replace 'CLUSTER_PUBLIC_URL',$clusterPublicUrl | kubectl apply -f -
(Get-Content -path ..\k8s\azure-templated\login-ingress.yaml -Raw) -replace 'CLUSTER_PUBLIC_URL',$clusterPublicUrl | kubectl apply -f -
(Get-Content -path ..\k8s\azure-templated\store-ingress.yaml -Raw) -replace 'CLUSTER_PUBLIC_URL',$clusterPublicUrl | kubectl apply -f -