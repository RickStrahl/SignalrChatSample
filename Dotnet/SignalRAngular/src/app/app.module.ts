import { BrowserModule } from '@angular/platform-browser';
import {FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';


//import { AppComponent } from './app.component';
import { ChatService } from './chat-client/chatservice';
import { ChatClientComponent } from './chat-client/chat-client.component';
import { AppComponent } from "./app.component";


@NgModule({
  declarations: [
    AppComponent,
    ChatClientComponent
  ],
  imports: [
    BrowserModule,
    FormsModule    
  ],
  providers: [
    ChatService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
