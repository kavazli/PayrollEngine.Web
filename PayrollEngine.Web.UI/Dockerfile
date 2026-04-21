# Runtime stage - nginx to serve pre-built static files from host
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

# Copy published files from host machine
COPY ["PayrollEngine.Web.UI/bin/Release/net9.0/publish/wwwroot/", "."]

# Copy custom nginx config
COPY PayrollEngine.Web.UI/nginx.conf /etc/nginx/nginx.conf

EXPOSE 80

