# syntax=docker/dockerfile:1
FROM node:16.13.1
#FredFanPage
ENV API_IP fredfanpage
ENV API_PORT 80
#LogsAPI
ENV LOGS_IP logs_api
ENV LOGS_HTTP_PORT 80
ENV LOGS_SSH_PORT 22

WORKDIR /attacker
COPY "package*.json" "./"
COPY "infectedAppSettings.json" "./"
RUN npm install
COPY . .
CMD ["node", "index.js"]