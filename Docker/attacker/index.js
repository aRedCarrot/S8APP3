const fs = require('fs')
const path = require('path')
const {NodeSSH} = require('node-ssh');
const { resolve } = require('path');

const ssh = new NodeSSH()
const API_IP = process.env.API_IP;
const API_PORT = process.env.API_PORT;
const LOGS_IP = process.env.LOGS_IP;
const LOGS_SSH_PORT = 22;

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
    username : 'username',
    password : 'password',
  }).then(() =>{

  }).catch((error) =>{
    console.error("Something went wrong : ", error);
  })

  // Exported functions
  const isCompleted = () => {
    
  }
}

