DO wwDotnetBridge
SET PROCEDURE TO ChatClient ADDITIVE

*************************************************************
DEFINE CLASS ChatClient AS Custom
*************************************************************
*: Author: Rick Strahl
*:         (c) West Wind Technologies, 2017
*:Contact: http://www.west-wind.com
*:Created: 09/27/2017
*************************************************************
#IF .F.
*:Help Documentation
*:Topic:
Class ChatClient

*:Description:

*:Example:

*:Remarks:

*:SeeAlso:


*:ENDHELP
#ENDIF

oBridge = null
oChat = null

************************************************************************
*  Init
****************************************
***  Function:
***    Assume:
***      Pass:
***    Return:
************************************************************************
FUNCTION Init()

this.oBridge = GetwwDotnetBridge()
IF !this.oBridge.LoadAssembly( FULLPATH("SignalRClient.dll"))
   ERROR "Couldn't load SignalR client library." + + this.oBridge.cErrorMsg
ENDIF


THIS.oChat = this.oBridge.CreateInstance("SignalRClient.Chat.ChatClient")
IF ISNULL(this.oChat)
   ERROR "Unable to load Chat Client " + this.oBridge.cErrorMsg
ENDIF


ENDFUNC
*   Init


************************************************************************
*  Start
****************************************
***  Function:
***    Assume:
***      Pass:
***    Return:
************************************************************************
FUNCTION Start()

THIS.oChat.Start(this)

ENDFUNC
*   Start

************************************************************************
*  Stop
****************************************
***  Function:
***    Assume:
***      Pass:
***    Return:
************************************************************************
FUNCTION Stop()

this.oChat.Stop()

ENDFUNC
*   Stop


************************************************************************
*  SendMessage
****************************************
***  Function:
***    Assume:
***      Pass:
***    Return:
************************************************************************
FUNCTION SendMessage(lcMsg as String, lcGroup as string, lcName as string)
this.oChat.SendMessage(lcMsg, lcGroup, lcName)
ENDFUNC
*   SendMessage

************************************************************************
*  JoinGroup
****************************************
***  Function:
***    Assume:
***      Pass:
***    Return:
************************************************************************
FUNCTION JoinGroup(lcName, lcGroup)

this.oChat.JoinGroup(lcName, lcGroup)

ENDFUNC
*   JoinGroup



************************************************************************
*  ReceiveMessage
****************************************
***  Function:
***    Assume:
***      Pass:
***    Return:
************************************************************************
FUNCTION OnReceiveMessage(loMessage)

ACTIVATE SCREEN 
? loMessage.Message
? "   - " + loMessage.User.Group + " " +  loMessage.User.Name

ENDFUNC
*   ReceiveMessage

ENDDEFINE
*EOC ChatClient 