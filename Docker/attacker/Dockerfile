# syntax=docker/dockerfile:1
FROM node:16.13.1
#FredFanPage
ENV API_IP localhost
ENV API_PORT 8081
#LogsAPI
ENV LOGS_IP localhost
ENV LOGS_HTTP_PORT 8080
ENV LOGS_SSH_PORT 8022

WORKDIR /attacker
COPY "package*.json" "./"
RUN npm install
COPY . .
CMD ["node", "index.js"]