import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import marked from 'marked';

declare var hljs:any;
declare var toastr: any;
declare var $: any;


@Injectable()
export class ChatService {
  chatHub: any;
  hubConnection: null;

  user = new ChatUser();
  messages:ChatMessage[] = [];
  message:ChatMessage = new ChatMessage();

  signalrConnectionStatus = "Disconnected";

  signalrUrl = "http://signalrswf.west-wind.com/signalr";
  //signalrUrl= "http://localhost/signalrhub/signalr";
  //signalrUrl= "http://localhost:2662/signalr";
  
  constructor() {    
    this.user.Group = "Southwest Fox";    
    this.loadChatHubScript();
    this.resizeWindowHandling();
        
    setTimeout(() => this.startHub(), 700);
    this.setToastrOptions();
  }
  
  onReceiveMessage(message:ChatMessage) 
  {        
    message.IsCurrentUser = message.User.Name == this.user.Name;
    message.Message = marked(message.Message);

    console.log("Receiving message:", message, this);
    this.messages.push(message);    

    this.highlightCode();
  }

  sendMessage(message:string, group?:string, name?:string) {
    if (!message)
      return;
    if (!group)
      group = this.user.Group;
    if (!name)
      name = this.user.Name;

    console.log("Send Message: ", group, message);
    try {
      this.chatHub.server.sendMessage(message, group, name);
    } catch (ex) {
      this.reconnectToHub();
      toastr.error("Can't send message - no connection.");      
    }
  }

  clearMessages(){
    this.messages = [];
  }

  joinGroup(name:string, group:string) {
    console.log("join group: ",name, group);
    this.chatHub.server.joinGroup(name, group);

    if(name)
      this.user.Name = name;
    if (group)
      this.user.Group = group;
    
    this.sendMessage(name + " joined " + this.user.Group,this.user.Group);
  }  

  highlightCode()  {
    setTimeout(function() {
        $("pre code")
        .each(function (i, block) {
            hljs.highlightBlock(block);
        });
    },20);
  }


  startHub() {
    var me = this;

    // alternate syntax that doesn't require the hub proxy url
    // this.connection = $.hubConnection(this.signalrUrl);
    // this.connection.logging = true;
    // this.hub = this.connection.createHubProxy('chatHub');

    // this.hub.on('onReceiveMessage', function (msg) {
    //   alert("received");
    //     me.messages.push(msg)   ;        
    // }); 

    // this.connection.start().done(function() {
    //   console.log("Hub started...");
    //   me.hub.invoke("JoinGroup",me.name, me.group);
    //   //me.hub.invoke("SendMessage","HELLO WORLD","Southwest Fox");
    // });


    $.connection.hub.url = this.signalrUrl ;
    this.chatHub = $.connection.chatHub;
    this.hubConnection = $.connection.hub;

    if (this.chatHub == null) {
      this.signalrConnectionStatus = "Disconnected";
      if (this.signalrConnectionStatus != "Disconnected")
        toastr.error("Couldn't connect to server. Please refresh the page.");
      return;
    }

    this.chatHub.logging = true;


    // important to explicitly forward the call so the THIS pointer
    // stays in context - if just assigning the function jquery context
    // becomes this and that's a problem!
    this.chatHub.client.OnReceiveMessage = (msg) => this.onReceiveMessage(msg);

    // Connection Events
    this.chatHub.connection.error((error) => {
      this.signalrConnectionStatus = "Disconnected";
      if (error && this.signalrConnectionStatus != "Disconnected")
        toastr.error("Connection error: " + error.message);

      this.chatHub = $.connection.chatHub;
      //this.hub.logging = true;
      this.reconnectToHub();
    });
    this.chatHub.connection.disconnected((error) => {
      this.signalrConnectionStatus = "Disconnected";
      if (error && this.signalrConnectionStatus != "Disconnected")
        toastr.error("Connection lost: " + error);
      this.reconnectToHub();
    });

    this.connectToHub();

    // check status
    setInterval(() => {
        var oldStatus = this.signalrConnectionStatus;
        if ($.connection.hub.state == 1) {
          this.signalrConnectionStatus = "Connected";
          if (oldStatus != this.signalrConnectionStatus)
            toastr.success("Re-connected.")
        } else {
          this.signalrConnectionStatus = "Disconnected";
          if (oldStatus != this.signalrConnectionStatus) 
            toastr.error("Connection lost...");          
        }
      },
      10000);

    // message content links
    $(document.body).on("click", ".message-item a", function () {
      window.open(this.href);
      return false;
    });

  }

  reconnectToHub() {
    setTimeout(this.connectToHub, 2000);
  }

  connectToHub() {

    var p = $.connection.hub.start().then(() => {
      console.log("Hub Started: ", this.user, "Connection State:", $.connection.hub.state,this.chatHub.state);
      if ($.connection.hub.state == 1) {
        this.signalrConnectionStatus = "Connected";
        this.joinGroup(this.user.Name, this.user.Group);
        toastr.info("Connected to " + this.user.Group);
      } else {
        toastr.info("Error connecting to " + this.user.Group);
        setTimeout(this.connectToHub, 2000);
      }
    });

  }
  
  loadChatHubScript() {
    // Add Hubs configuration script
    var s = document.createElement('script');
    s.src = this.signalrUrl + "/hubs";
    s.async = false;
    try {
      document.body.appendChild(s);
    } catch (e) {
      document.body.appendChild(s);
    }
  }

  setToastrOptions() {
    toastr.options.closeButton = true;
    toastr.options.positionClass = "toast-bottom-right";
  }

  resizeWindowHandling() {

    function resize() {
      var height = $(window).height();
      console.log(height);
      $("#ChatMessages").height(height - 330);
    }

    function debounce(func, wait, immediate) {
      var timeout;
      return function () {
        var context = this, args = arguments;
        var later = function () {
          timeout = null;
          if (!immediate) func.apply(context, args);
        };
        var callNow = immediate && !timeout;
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
        console.log("Call now: " + callNow);
        if (callNow)
          func.apply(context, args);
      };
    };
    $(window).on("resize", debounce(resize, 5, false));
    setTimeout(resize, 100);
  }

}

export class ChatMessage {
  
  User = new ChatUser();
  Message = "";
  Time = new Date();
  IsCurrentUser = false;

  
}

export class ChatUser {
  Name = "Anonymous" + new Date().getTime();
  Group = "";
}
