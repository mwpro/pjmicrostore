FROM node:12.7.0 as ui-builder
RUN mkdir /usr/src/app
WORKDIR /usr/src/app
ENV PATH /usr/src/app/node_modules/.bin:$PATH
COPY package.json /usr/src/app/package.json
COPY package-lock.json /usr/src/app/package-lock.json
RUN npm install
RUN npm install -g @vue/cli
COPY . /usr/src/app
RUN npm run build
 
FROM nginx:1.17.2-alpine
COPY --from=ui-builder /usr/src/app/dist /usr/share/nginx/html
COPY env.sh /scripts/env.sh
COPY nginx.default.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
RUN apk add --no-cache bash
ENV API_URL=/ AUTH_URL=http://localhost:5000 APP_URL=http://localhost:8080
CMD ["/bin/bash", "-c", "/scripts/env.sh && nginx -g \"daemon off;\""]