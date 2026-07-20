# Stage 1: Build Angular application
FROM node:20-alpine AS build

ARG BUILD_VERSION=1.0.0
ARG API_BASE_URL=http://localhost:5000

WORKDIR /app

COPY package.json package-lock.json ./
RUN npm ci --legacy-peer-deps

COPY . .
RUN API_BASE_URL=${API_BASE_URL} ng build --configuration=production

# Stage 2: Serve with Nginx
FROM nginx:alpine AS production

COPY --from=build /app/dist/frontend/browser /usr/share/nginx/html
COPY ../nginx/nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
