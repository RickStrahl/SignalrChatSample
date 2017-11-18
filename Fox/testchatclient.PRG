LPARAMETERS lnCount
DO ChatClient

PUBLIC goChat

IF VARTYPE(goChat) = "O"
   goChat.Stop()
   WAIT WINDOW "" TIMEOUT 0.2
ENDIF
IF TYPE("gcSignalRUrl") # "C"
   PUBLIC gcSignalUrl 
   gcSignalRUrl = "http://signalrswf.west-wind.com/"         
ENDIF


goChat = CREATEOBJECT("ChatClient")
goChat.oChat.SignalRUrl = gcSignalRUrl

goChat.Start()

lcGroup = "Southwest Fox"
goChat.JoinGroup("Rick (SWFOX)", lcGroup)

IF EMPTY(lnCount)
   goChat.SendMessage("Hello From FoxPro PID " +  TRANSFORM(_vfp.ProcessId) +  "  " + TIME(), lcGroup)
ELSE
	FOR lnX = 1 TO lnCount
	   goChat.SendMessage("Hello From FoxPro PID " +  TRANSFORM(_vfp.ProcessId) +  "  " + TIME(), lcGroup)
	   WAIT WINDOW "" TIMEOUT 0.1
	ENDFOR
ENDIF

WAIT WINDOW "Waiting for chat messages to display"