FROM node:22-alpine AS build

ARG yarn_version=4.5.3

WORKDIR /app

RUN apk add --no-cache libc6-compat

COPY src/CineControl/package.json ./

RUN corepack prepare yarn

COPY src/CineControl/yarn* src/CineControl/angular.json src/CineControl/.yarnrc.yml ./


COPY src/CineControl .
RUN corepack yarn install --immutable

RUN corepack yarn build

FROM nginx:latest

COPY src/CineControl/nginx.conf /etc/nginx/conf.d/default.conf

COPY --from=build /app/dist/cinecontrol/browser /usr/share/nginx/html

EXPOSE 80