DO WebStoreNotificationsClient

IF VARTYPE(loNotifications) = "O"
   loNotifications.Stop()
   WAIT WINDOW "" TIMEOUT 0.2 
ENDIF
IF TYPE("gcSignalRUrl") # "C"
   PUBLIC gcSignalUrl 
   gcSignalRUrl = "http://signalrswf.west-wind.com/"         
ENDIF

PUBLIC loNotifications
loNotifications = CREATEOBJECT("WebStoreNotificationsClient")
loNotifications.oNotifications.SignalRUrl = gcSignalRUrl
loNotifications.Start()

WAIT WINDOW "Waiting for orders to be placed..."

RETURN