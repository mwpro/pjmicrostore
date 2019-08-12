# Kubernetes readme

## Kubernetes in Docker Desktop for Windows

### Create ingress:

```kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/master/deploy/static/mandatory.yaml```

```kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/master/deploy/static/provider/cloud-generic.yaml```

### Add to hosts file

```
127.0.0.1 	store.pjmicrostore.local
127.0.0.1   admin.pjmicrostore.local
127.0.0.1   login.pjmicrostore.local
127.0.0.1	azurestorage.infrastructure
```

### Deploy local infrastructure

```kubectl apply -f .\local_infrastructure\.```

### Setup Azure Storage Emulator to public mode

Use Azure Storage Explorer and port forwading 

```kubectl port-forward deployment/azurestorage 10000:80 -n infrastructure```

## Common for all providers

### Deploy all services

``` kubectl apply -f .```