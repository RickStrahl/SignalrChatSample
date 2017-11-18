# Sample code for Southwest Fox 2017 SignalR Session

This repository contains the source code for the SignalR session that demonstrates how to use real time messaging to create peer to peer applications and push server notifications into FoxPro applications.

This sample also includes Web Clients using VueJs and Angular Web clients accessing the SignalR hub server.

### Online Hub Server
There's a sample hub server available online at:

http://signalrswf.west-wind.com/     

which you can use if you don't want to run the SignalR server locally. For the Web samples, change the signalR URL as part of the setup to point the above Url to use the online SignalR hub.

No guarantees this will continue to run, but for a while this will be available.


### Runnable FoxPro Sample
You can find more information on the FoxPro bits here:

* [White Paper](https://bitbucket.org/RickStrahl/swfox16_signalr/raw/master/Documents/Strahl_SignalR.docx)
* [Slides](https://bitbucket.org/RickStrahl/swfox16_signalr/raw/master/Documents/Strahl_SignalR.pptx)

You can download a fully self contained FoxPro sample of the Chat application from here:

* [SignalR Runnable Sample App](https://bitbucket.org/RickStrahl/swfox16_signalr/raw/master/Build/SignalRSamples.zip)

You can download this sample and run it against our test Signal Chat service.

> #### Security 
> In order to run this application you may have to Unblock the various DLL dependencies that ship with this example, as they are considered downloaded from the Internet. If you run the EXE and get a `Cannot load CLR Instance` error do the following:
>
> * Right click the **each of .dll files** individually
> * Open Properties
> * Check the [x] Unblock option (or button pre-Win10)


### VueJs Sample
The VueJs sample is part of the **SignalRHub** Web project that also contains the SignalR hub server,  and can be run by accessing the `chatclient.html` page:

* http://signalrswf.west-wind.com/chatclient.html (online IIS)
* http://localhost/signalrhub/chatclient.html (IIS must be configured)
* http://localhost:2662/chatclient.html (IIS Express start VS session)

### Angular (5) Sample
The Angular sample is contained the **SignalRAngular** folder and is a self-contained Angular CLI sample application. To run this example:

To run locally:

* cd `./SignalRAngular` Folder
* Make sure Angular CLI is installed (`npm install @angular/cli -g`)
* Run `npm install` to restore package
* Run `ng serve`
* Navigate to `http://localhost:4200`

By default the online SignalR hub at http://signalrswf.west-wind.com is used. If you want to switch to the local hub, change:

* Open `./SignalRAngular/src/app/chat-client.ts` 
* Change `signalrUrl = "http://localhost/signalrhub/signalr"` or `http://localhost:2662/signalr`;
* Start the SignalR hub Web application.

You can also run the sample online:

* http://signalrswf.west-wind.com/AngularClient

and you should be able to share connections with any others connected through the same hub.