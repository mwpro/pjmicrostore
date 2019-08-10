sed -i "s|REPLACE_WITH_API_URL|$API_URL|g" /usr/share/nginx/html/js/*
sed -i "s|REPLACE_WITH_AUTH_URL|$AUTH_URL|g" /usr/share/nginx/html/js/*
sed -i "s|REPLACE_WITH_APP_URL|$APP_URL|g" /usr/share/nginx/html/js/*