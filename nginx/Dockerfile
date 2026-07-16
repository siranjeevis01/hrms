# ============================================
# Stage 1: Build Angular Application
# ============================================
FROM node:18-alpine AS build

ARG BUILD_VERSION=1.0.0
ARG API_BASE_URL=http://localhost:5000

WORKDIR /app

# Copy package files
COPY package*.json ./

# Install dependencies
RUN npm ci --legacy-peer-deps

# Copy source code
COPY . .

# Set environment variables
ENV NODE_ENV=production
ENV API_BASE_URL=$API_BASE_URL
ENV NG_APP_VERSION=$BUILD_VERSION

# Build Angular application
RUN npm run build -- --configuration=production

# ============================================
# Stage 2: Production Nginx Server
# ============================================
FROM nginx:1.25-alpine AS production

# Add labels
LABEL maintainer="HRMS Pro Team"
LABEL version=$BUILD_VERSION
LABEL description="HRMS Pro Angular Frontend"

# Remove default nginx config
RUN rm /etc/nginx/conf.d/default.conf

# Copy custom nginx config
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Copy built Angular application
COPY --from=build /app/dist/hrms-frontend/browser /usr/share/nginx/html

# Create nginx cache directories
RUN mkdir -p /var/cache/nginx/client_temp \
    && mkdir -p /var/cache/nginx/proxy_temp \
    && mkdir -p /var/cache/nginx/fastcgi_temp \
    && mkdir -p /var/cache/nginx/uwsgi_temp \
    && mkdir -p /var/cache/nginx/scgi_temp

# Set proper permissions
RUN chown -R nginx:nginx /usr/share/nginx/html \
    && chown -R nginx:nginx /var/cache/nginx \
    && chown -R nginx:nginx /var/log/nginx \
    && chown -R nginx:nginx /etc/nginx/conf.d \
    && touch /var/run/nginx.pid \
    && chown -R nginx:nginx /var/run/nginx.pid

# Health check
HEALTHCHECK --interval=30s --timeout=10s --start-period=30s --retries=3 \
    CMD wget --no-verbose --tries=1 --spider http://localhost:80/health || exit 1

# Expose port
EXPOSE 80

# Start nginx
CMD ["nginx", "-g", "daemon off;"]

# ============================================
# Stage 3: Development Server (Optional)
# ============================================
FROM node:18-alpine AS development

WORKDIR /app

# Copy package files
COPY package*.json ./

# Install dependencies
RUN npm ci --legacy-peer-deps

# Copy source code
COPY . .

# Expose ports
EXPOSE 4200
EXPOSE 49153

# Start Angular development server
CMD ["ng", "serve", "--host", "0.0.0.0", "--port", "4200", "--disable-host-check", "--poll", "2000"]