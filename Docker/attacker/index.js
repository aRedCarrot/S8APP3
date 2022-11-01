const fs = require('fs')
const path = require('path')
const {NodeSSH} = require('node-ssh');
const { resolve } = require('path');

const ssh = new NodeSSH()
const API_IP = process.env.API_IP || "localhost";
const API_PORT = process.env.API_PORT || 8081;
const LOGS_IP = process.env.LOGS_IP || "localhost";
const LOGS_SSH_PORT = process.env.LOGS_SSH_PORT || 8022;

const sleep = (time) => {
  return new Promise(resolve => setTimeout(resolve,time));
}

const AttackAPI = async () => {
  const SQL_COMMAND = ""

  // Exported functions
  const isCompleted = () => {

  }
}

const SSHIntoLogs = async () => {
  ssh.connect({
    host: LOGS_IP,
    port: LOGS_SSH_PORT,
    username : 'admin',
    password : 'admin',
  }).then(() =>{
    console.log("SSH CONNECTION SUCCESSFULL");
  }).catch((error) =>{
    console.error("Something went wrong : ", error);
  })

  // Exported functions
  const isCompleted = () => {
    
  }
}

console.log(LOGS_IP);
SSHIntoLogs();
