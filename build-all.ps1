$DOCKER_REPOSITORY_NAME = 'mwpro'

# checkout
docker build -t $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-cart ./src -f ./src/Checkout/Checkout.Cart/Dockerfile
docker build -t $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-orders ./src -f ./src/Checkout/Checkout.Orders/Dockerfile
docker build -t $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-payments ./src -f ./src/Checkout/Checkout.Payments/Dockerfile
docker build -t $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-shipping ./src -f ./src/Checkout/Checkout.Shipping/Dockerfile

# common
docker build -t $DOCKER_REPOSITORY_NAME/pjmicrostore-common-emailsender ./src -f ./src/Common/Common.EmailSender/Dockerfile

# frontapigateway
docker build -t $DOCKER_REPOSITORY_NAME/pjmicrostore-adminapigateway ./src -f ./src/External/External.AdminApiGateway/Dockerfile
docker build -t $DOCKER_REPOSITORY_NAME/pjmicrostore-frontapigateway ./src -f ./src/External/External.FrontApiGateway/Dockerfile

# identity
docker build -t $DOCKER_REPOSITORY_NAME/pjmicrostore-identity-api ./src -f ./src/Identity/Identity.Api/Dockerfile

# products
docker build -t $DOCKER_REPOSITORY_NAME/pjmicrostore-products-catalog ./src -f ./src/Products/Products.Catalog/Dockerfile
docker build -t $DOCKER_REPOSITORY_NAME/pjmicrostore-products-search ./src -f ./src/Products/Products.Search/Dockerfile
docker build -t $DOCKER_REPOSITORY_NAME/pjmicrostore-products-photos ./src -f ./src/Products/Products.Photos/Dockerfile

# frontend
docker build -t $DOCKER_REPOSITORY_NAME/pjmicrostore-front-admin ./front-admin -f ./front-admin/Dockerfile
docker build -t $DOCKER_REPOSITORY_NAME/pjmicrostore-front-store ./front-store -f ./front-store/Dockerfile