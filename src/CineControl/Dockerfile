FROM node:20-alpine AS build

WORKDIR /app

COPY src/CineControl/package*.json ./
COPY src/CineControl/yarn* ./

RUN yarn install --frozen-lockfile

COPY src/CineControl .

RUN yarn ng build

FROM nginx:latest

COPY src/CineControl/nginx.conf /etc/nginx/conf.d/default.conf

COPY --from=build /app/dist/cinecontrol/browser /usr/share/nginx/html

EXPOSE 80