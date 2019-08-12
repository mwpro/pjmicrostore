$DOCKER_REPOSITORY_NAME = 'mwpro'

# checkout
$DOCKER_IMAGE_VERSION = "1.0.1"
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-cart $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-cart:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-cart:$DOCKER_IMAGE_VERSION 
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-cart:latest
$DOCKER_IMAGE_VERSION = "1.0.2"
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-orders $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-orders:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-orders:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-orders:latest
$DOCKER_IMAGE_VERSION = "1.0.1"
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-payments $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-payments:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-payments:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-payments:latest
$DOCKER_IMAGE_VERSION = "1.0.1"
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-shipping $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-shipping:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-shipping:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-checkout-shipping:latest

# common
$DOCKER_IMAGE_VERSION = "1.0.1"
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-common-emailsender $DOCKER_REPOSITORY_NAME/pjmicrostore-common-emailsender:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-common-emailsender:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-common-emailsender:latest

# frontapigateway
$DOCKER_IMAGE_VERSION = "1.0.1"
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-adminapigateway $DOCKER_REPOSITORY_NAME/pjmicrostore-adminapigateway:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-adminapigateway:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-adminapigateway:latest
$DOCKER_IMAGE_VERSION = "1.0.1"
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-frontapigateway $DOCKER_REPOSITORY_NAME/pjmicrostore-frontapigateway:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-frontapigateway:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-frontapigateway:latest

# identity
$DOCKER_IMAGE_VERSION = "1.0.3"
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-identity-api $DOCKER_REPOSITORY_NAME/pjmicrostore-identity-api:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-identity-api:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-identity-api:latest

# products
$DOCKER_IMAGE_VERSION = "1.0.1"
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-products-catalog $DOCKER_REPOSITORY_NAME/pjmicrostore-products-catalog:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-products-catalog:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-products-catalog:latest
$DOCKER_IMAGE_VERSION = "1.0.6"
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-products-search $DOCKER_REPOSITORY_NAME/pjmicrostore-products-search:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-products-search:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-products-search:latest
$DOCKER_IMAGE_VERSION = "1.0.2"
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-products-photos $DOCKER_REPOSITORY_NAME/pjmicrostore-products-photos:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-products-photos:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-products-photos:latest

# frontend
$DOCKER_IMAGE_VERSION = "1.0.0"
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-front-admin $DOCKER_REPOSITORY_NAME/pjmicrostore-front-admin:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-front-admin:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-front-admin:latest
$DOCKER_IMAGE_VERSION = "1.0.0"
docker tag $DOCKER_REPOSITORY_NAME/pjmicrostore-front-store $DOCKER_REPOSITORY_NAME/pjmicrostore-front-store:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-front-store:$DOCKER_IMAGE_VERSION
docker push $DOCKER_REPOSITORY_NAME/pjmicrostore-front-store:latest