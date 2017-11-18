DO wwDotnetBridge
SET PROCEDURE TO WebStoreNotificationsClient ADDITIVE

*************************************************************
DEFINE CLASS WebStoreNotificationsClient AS Custom
*************************************************************
*: Author: Rick Strahl
*:         (c) West Wind Technologies, 2017
*:Contact: http://www.west-wind.com
*:Created: 09/27/2017
*************************************************************
#IF .F.
*:Help Documentation
*:Topic:
Class WebStoreNotificationsClient

*:Description:

*:Example:

*:Remarks:

*:SeeAlso:


*:ENDHELP
#ENDIF

oBridge = null
oNotifications = null

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


THIS.oNotifications = this.oBridge.CreateInstance("SignalRClient.WebStoreNotifications.WebStoreNotificationsClient")
IF ISNULL(this.oNotifications)
   ERROR "Unable to load Notifications Client " + this.oBridge.cErrorMsg
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

THIS.oNotifications.Start(this)

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

this.oNotifications.Stop()

ENDFUNC
*   Stop


************************************************************************
*  OnNotifyOrder
****************************************
***  Function:
***    Assume:
***      Pass:
***    Return:
************************************************************************
FUNCTION OnNotifyOrder(loNotification)

ACTIVATE SCREEN
? loNotification.OrderNumber + " " + TRANSFORM(loNotification.OrderAmount) + " " + NVL(loNotification.CustomerName,"")

ENDFUNC
*   OnNotifyOrder


************************************************************************
*  OnNotifyItemAdded
****************************************
***  Function:
***    Assume:
***      Pass:
***    Return:
************************************************************************
FUNCTION OnNotifyItemAdded(loNotification)

ACTIVATE SCREEN
? loNotification.Sku + " " + loNotification.Description + " " + TRANSFORM(loNotification.Qty)

ENDFUNC
*   OnNotifyItemAdded


ENDDEFINE
*EOC ChatClient 