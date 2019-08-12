# DB
kubectl create secret generic db-user-pass --from-file=./db_username --from-file=./db_password --from-file=./db_server -n checkout
kubectl create secret generic db-user-pass --from-file=./db_username --from-file=./db_password --from-file=./db_server -n common
kubectl create secret generic db-user-pass --from-file=./db_username --from-file=./db_password --from-file=./db_server -n external
kubectl create secret generic db-user-pass --from-file=./db_username --from-file=./db_password --from-file=./db_server -n identity
kubectl create secret generic db-user-pass --from-file=./db_username --from-file=./db_password --from-file=./db_server -n products
kubectl create secret generic db-user-pass --from-file=./db_username --from-file=./db_password --from-file=./db_server -n infrastructure

# photo storage
kubectl create secret generic photo-storage --from-file=./photo_storage -n products