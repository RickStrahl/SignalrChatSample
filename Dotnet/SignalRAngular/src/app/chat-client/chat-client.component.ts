import { Component, OnInit } from '@angular/core';
import { ChatMessage, ChatService, ChatUser } from '../chat-client/chatservice';

declare var $:any;

@Component({
  selector: 'app-chat-client',
  templateUrl: './chat-client.component.html',
  styleUrls: ['./chat-client.component.css']
})
export class ChatClientComponent implements OnInit {

  messages:ChatMessage[] = [];
  groups = [
    "Southwest Fox",
    "Web Connection",
    "Markdown Monster"
  ];
  group = "Southwest Fox";
  user:ChatUser = new ChatUser();
  message = "Say something witty";

  constructor(public service:ChatService) { 
    this.messages = service.messages;
    this.user = service.user;    
  }

  ngOnInit() {    
  }

  clearMessages(){
    this.messages = [];
  }

  joinGroup(){
    this.service.joinGroup(this.user.Name,this.user.Group)
  }

  sendMessage(message:string, group?: string, name?:string)
  {
    if (!message)
      return;
    if (!group)
      group = this.group;
    if (!name)
      name = this.user.Name;

    this.service.sendMessage(message,group,name);

    this.message = "";

  }

  
}

