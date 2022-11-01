# syntax=docker/dockerfile:1
FROM node:16.13.1
ENV API_IP 192.168.0.1
ENV API_PORT 8080
ENV LOGS_IP 192.168.0.2
WORKDIR /attacker
COPY ["package.json", "package-lock.json*","./"]
RUN npm install --production
COPY . .
CMD ["node", "index.js"]