$DOCKER_REPOSITORY_NAME = 'mwpro'
$DOCKER_IMAGE_VERSION = Get-Date -UFormat %Y%m%d-%H%M

# checkout
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-cart $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-cart:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-cart:$DOCKER_IMAGE_VERSION 
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-cart:latest
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-orders $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-orders:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-orders:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-orders:latest
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-payments $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-payments:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-payments:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-payments:latest
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-shipping $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-shipping:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-shipping:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-shipping:latest

# common
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-common-emailsender $DOCKER_REPOSITORY_NAME/pjmicrostore-common-emailsender:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-common-emailsender:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-common-emailsender:latest

# frontapigateway
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-adminapigateway $DOCKER_REPOSITORY_NAME/pjmicrostore-adminapigateway:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-adminapigateway:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-adminapigateway:latest
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-frontapigateway $DOCKER_REPOSITORY_NAME/pjmicrostore-frontapigateway:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-frontapigateway:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-frontapigateway:latest

# identity
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-identity-api $DOCKER_REPOSITORY_NAME/pjmicrostore-identity-api:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-identity-api:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-identity-api:latest

# products
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-products-catalog $DOCKER_REPOSITORY_NAME/pjmicrostore-products-catalog:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-products-catalog:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-products-catalog:latest
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-products-search $DOCKER_REPOSITORY_NAME/pjmicrostore-products-search:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-products-search:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-products-search:latest
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-products-photos $DOCKER_REPOSITORY_NAME/pjmicrostore-products-photos:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-products-photos:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-products-photos:latest

# frontend
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-front-admin $DOCKER_REPOSITORY_NAME/pjmicrostore-front-admin:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-front-admin:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-front-admin:latest
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-front-store $DOCKER_REPOSITORY_NAME/pjmicrostore-front-store:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-front-store:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-front-store:latest