# build stage
FROM node:14 as build
WORKDIR /src
COPY package.json ./

RUN npm install
COPY . .
RUN npm run static

# production stage
FROM steebchen/nginx-spa:stable as production
EXPOSE 80
COPY --from=build /src/dist /app
COPY ./entrypoint.sh /entrypoint.sh

RUN ["chmod", "+x", "/entrypoint.sh"]

ENTRYPOINT ["/entrypoint.sh"]
