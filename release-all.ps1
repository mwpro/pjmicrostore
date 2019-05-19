$DOCKER_REPOSITORY_NAME = 'mwpro'
$DOCKER_IMAGE_VERSION = Get-Date -UFormat %Y%m%d-%H%M

# checkout
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-cart $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-cart:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-cart:$DOCKER_IMAGE_VERSION
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-orders $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-orders:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-orders:$DOCKER_IMAGE_VERSION
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-payments $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-payments:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-payments:$DOCKER_IMAGE_VERSION

# frontapigateway
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-frontapigateway $DOCKER_REPOSITORY_NAME/pjmicrostore-frontapigateway:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-frontapigateway:$DOCKER_IMAGE_VERSION

# identity
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-identity-api $DOCKER_REPOSITORY_NAME/pjmicrostore-identity-api:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-identity-api:$DOCKER_IMAGE_VERSION

# products
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-products-catalog $DOCKER_REPOSITORY_NAME/pjmicrostore-products-catalog:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-products-catalog:$DOCKER_IMAGE_VERSION
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-products-search $DOCKER_REPOSITORY_NAME/pjmicrostore-products-search:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-products-search:$DOCKER_IMAGE_VERSION
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-products-photos $DOCKER_REPOSITORY_NAME/pjmicrostore-products-photos:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-products-photos:$DOCKER_IMAGE_VERSION