var augnitoClient;

$(document).ready(function (e) {
    var appLogic = {};
    appLogic.HyperTextControl = $("#hyperText")
    appLogic.EnableLogs = false;

    appLogic.OnChangeMicState = function (isConnected) {

        //This will be called when mic recording start OR stop to update UI interface mic button.
        if (isConnected) {
            appLogic.HyperTextControl.html("");
            appLogic.HyperTextControl.hide();
            $("#btnAugnitoMic").addClass("AugnitoStartButtonStyle");
        }
        else {
            $("#btnAugnitoMic").removeClass("AugnitoStartButtonStyle");
        }
    }
    appLogic.InterfaceCommand = function (ActionRecipe) {
        // Any UI Command to be process here, before it reaches the editor
        if (AugnitoCMDs.STOP_MIC == ActionRecipe.Name) {
            augnitoClient.stopListening();
            return true;
        }
        return false;
    }
    appLogic.TrimLeft = function (ReceivedText) {
        //Received trim left , Trim left also remove new line, so preserver it
        ReceivedText = ReceivedText.replace(/\n/gi, "@newline@");
        ReceivedText = ReceivedText.trimLeft();
        ReceivedText = ReceivedText.replace(new RegExp("@newline@", "gi"), "\n");
        return ReceivedText;
    }
    appLogic.HandleEditorCursorContext = function (receivedText, caretPosition, currentText) {
        // We called this part of code, client side beatification. 
        // In client side beatification text need to be processed for better formatting,
        // The Processing done here is :- 
        //      Make first Charecter capital, if after fullstop.
        //      Remove prefix spaces as and when needd.                

        var editorReady = receivedText;
        if (currentText == "") {
            // Is Empty than remove pre space and make first char capital
            editorReady = appLogic.TrimLeft(editorReady);
            editorReady = editorReady[0].toUpperCase() + editorReady.substring(1);
        }
        // Remove prfix spaces for newline and new paragraph
        if (editorReady == " \n" || editorReady == " \n\n") {
            // Avoid tail space on above line.
            editorReady = appLogic.TrimLeft(editorReady);
        }

        if (currentText.length > 1 && currentText[caretPosition - 1] == " " && currentText[caretPosition - 2] == ".") {
            // Capital after full stop
            editorReady = appLogic.TrimLeft(editorReady);
            editorReady = editorReady[0].toUpperCase() + editorReady.substring(1);
        }

        if (currentText.length > 0 && currentText[caretPosition - 1] == " ") {
            // If already segment has space then remove server given space, as the user has moved the cursor. 
            editorReady = appLogic.TrimLeft(editorReady);
        }

        if (currentText.length > 0 && currentText[caretPosition - 1] == "\n") {
            // Start of new line
            editorReady = appLogic.TrimLeft(editorReady);
            editorReady = editorReady[0].toUpperCase() + editorReady.substring(1);
        }

        if (currentText.length > 0 && (currentText[caretPosition - 1] == "." || currentText[caretPosition - 1] == ":")) {
            // Capital after full stop
            var trimleftText = appLogic.TrimLeft(editorReady);
            if (trimleftText.length > 0) {
                editorReady = " " + trimleftText[0].toUpperCase();
            }
            if (trimleftText.length > 1) {
                editorReady = editorReady + trimleftText.substring(1);
            }
        }
        if (editorReady.trim() == "." || editorReady.trim() == ":") {
            editorReady = appLogic.TrimLeft(editorReady);
        }

        return editorReady;
    }
    appLogic.CommandAndControlCallback = function (ActionRecipe) {

        if (appLogic.InterfaceCommand(ActionRecipe)) {
            return;
        }
        HtmlFormEditorProcess.ProcessCommand(ActionRecipe, []);
    };

    appLogic.FinalResultCallback = function (ActionRecipe) {

        var activeDocumentElement = $(document.activeElement);
        var caretPosition = activeDocumentElement.prop("selectionStart");
        var currentText = activeDocumentElement.val();
        var editorReady = appLogic.HandleEditorCursorContext(ActionRecipe.ReceivedText, caretPosition, currentText);
        HtmlFormEditorProcess.StringWriteAtCaret(currentText, editorReady, caretPosition);

        // Handle auto scroll if needed.
        if (document.activeElement.type == "textarea") {
            var lineHeight = parseInt(activeDocumentElement.css('line-height'));
            var lineNumber = document.activeElement.value.substr(0, document.activeElement.selectionStart).split("\n").length;
            activeDocumentElement.scrollTop(lineNumber * lineHeight);
        }
        else if (document.activeElement.type == "text") {
            var currentIndex = activeDocumentElement.prop("selectionStart");
            activeDocumentElement.blur();
            activeDocumentElement.focus();
            activeDocumentElement.prop("selectionStart", currentIndex);
            activeDocumentElement.prop("selectionEnd", currentIndex);
        }
    };
    appLogic.OnSessionEvent = function (meta) {

        // Session event will be called during speech session(between mic start and stop) 
        // whenever server has to send information to client app which is not relaed to ASR output.
        var sessionMetaData = meta;
        if (appLogic.EnableLogs) {
            console.log(sessionMetaData);
        }
        var event = sessionMetaData.Event;
        if (event == 'None') {
            // Error case
            return;
        }

        var eventType = sessionMetaData.Event.Type;
        var eventValue = sessionMetaData.Event.Value;
        if (eventType == "SESSION_CREATED") {
            var Sessiontoken = eventValue;
            console.log("session Token " + Sessiontoken);
            // After successful authenticate, server creates an unique ID for each speech session and sends it back to client app for reference.
            // Client app can store this is it requires.            
        }
        else if (eventType == "SERVICE_DOWN") {
            // Very rare, But This event will come when Speech server's any internal component down.
            console.log(eventType);
        }
        else if (eventType == "NO_DICTATION_STOP_MIC") {
            // Some time user start mic and forgot after it. start doing discussion with colleague or on phone.
            // In this case mic is on and user is not dictating any valid speech for trascription. Server can detect such situations and send an event to confirm from user.
            HandleMicOff();
        }
        else if (eventType == "INVALID_AUTH_CREDENTIALS") {
            // This event happens when one of following is invalid.
            // AccountCode, AccessKey, Active subscription for trial or paid. lmid.
            $("#txtMessageInformation").html(eventValue);
            $("#MessageDialog").dialog("open");
        }
        else if ("LOW_BANDWIDTH" == eventType) {
            // Speech API need continues upload speed of 32KBps if it raw audio data with 16k sampling rate.
            // If fluctuation in internet than speech output may be delayed. It's good to notify that speech may delayed due to poor network connection.
            // Client app can use this event to show un attendant popup to indicate network status. 
            console.log(eventType);
        }
    }
    appLogic.onPartialResults = function (response) {

        // Partial output against audio stream started to server. This is not final and keep changing.
        // Use this to show user that system started listing, and Processing your voice.
        var partialText = response.Result.Transcript;
        if (partialText && partialText.length > 82) {
            partialText = '..' + partialText.substring(partialText.length - 80);
        }
        appLogic.HyperTextControl.html(partialText);
        appLogic.HyperTextControl.show();
    }
    appLogic.onFinalResults = function (response) {

        // Prepare Action Recipe from speech output to pass in editor handle.
        // When System make output final this even will be call.
        // It can be either static command, or normal transcription. 
        appLogic.HyperTextControl.html('');
        appLogic.HyperTextControl.hide();
        var ActionRecipe = new Object();
        var text = response.Result.Transcript;
        var Action = response.Result.Action;

        ActionRecipe.Name = text.replace(/\s+/g, '');
        ActionRecipe.SessionCode = response.SessionCode;
        ActionRecipe.Final = response.Result.Final;
        ActionRecipe.IsCommand = response.Result.IsCommand;
        if (ActionRecipe.IsCommand) {
            ActionRecipe.Action = Action.replace(/\s+/g, '');
        }
        ActionRecipe.ReceivedText = text;
        webWorkerRichEdit.postMessage(ActionRecipe);
    }
    appLogic.onReadyForSpeech = function () {
        // On socket connection established 
        this.OnChangeMicState(true);
    };

    appLogic.onEndOfSession = function () {
        // On mic off and connection close
        this.OnChangeMicState(false);
    };
    appLogic.onError = function (code, data) {
        if (appLogic.EnableLogs) {
            console.log("ERR: " + code + ": " + (data || ''));
        }
    };
    appLogic.onEvent = function (eventCode, data) {

        // All these events are from client side SDK only for debugging purpose.
        // Speech server will not be calling them
        switch (eventCode) {

            case AugnitoSDKEvent.WS_CONNECTING:
                console.log("WS_CONNECTING");
                appLogic.HyperTextControl.html(data);
                appLogic.HyperTextControl.show();
                break;
            case AugnitoSDKEvent.MSG_MEDIA_STREAM_CREATED:
                //console.log("MSG_MEDIA_STREAM_CREATED");
                // code block
                break;
            case AugnitoSDKEvent.MSG_INIT_RECORDER:
                // console.log("MSG_INIT_RECORDER");              
                break;
            case AugnitoSDKEvent.MSG_RECORDING:
                //console.log("MSG_RECORDING");              
                break;
            case AugnitoSDKEvent.MSG_SEND_EMPTY:
                //console.log("MSG_SEND_EMPTY");              
                break;
            case AugnitoSDKEvent.MSG_WEB_SOCKET_OPEN:
                //  console.log("MSG_WEB_SOCKET_OPEN");              
                break;
            case AugnitoSDKEvent.MSG_WEB_SOCKET_CLOSE:
                //console.log("MSG_WEB_SOCKET_CLOSE");
                appLogic.HyperTextControl.html("");
                appLogic.HyperTextControl.hide();
                break;
            case AugnitoSDKEvent.MSG_STOP:
                //console.log("MSG_STOP");              
                break;
            default:
            // console.log("default");              
        }
    }


    function processRichEditInWebWorker(_function) {

        // Create worker for background processing.  Client app may not need this if they not procesing final text much.
        // But better to keep this, to de-couple UI thread and background thread.
        var workerURL = URL.createObjectURL(new Blob([_function.toString(),
        ';this.onmessage =  function (eee) {' + _function.name + '(eee.data);}'
        ], {
            type: 'application/javascript'
        }));

        var worker = new Worker(workerURL);
        worker.workerURL = workerURL;
        return worker;
    }


    function ProcessResponseData(ActionRecipe) {

        // Data post method of worker.
        self.postMessage(ActionRecipe);
    }

    var webWorkerRichEdit = processRichEditInWebWorker(ProcessResponseData);
    webWorkerRichEdit.onmessage = function (event) {

        var ActionRecipe = event.data
        if (ActionRecipe.IsCommand) {
            // Static commands , server will give action name
            ActionRecipe = AugnitoCMDStatic.PrepareRecipe(ActionRecipe);
            appLogic.CommandAndControlCallback(ActionRecipe);
        }
        else {

            // Look for dynamic commands
            ActionRecipe = AugnitoCMDRegex.PrepareRecipe(ActionRecipe);
            if (ActionRecipe.IsCommand) {
                appLogic.CommandAndControlCallback(ActionRecipe);
            }
            else {
                appLogic.FinalResultCallback(ActionRecipe);
            }
        }


    };

    augnitoClient = GetAugnitoClient(appLogic);
    $("#btnAugnitoMic").click(function () {
        augnitoClient.toggleListening();
    });
});