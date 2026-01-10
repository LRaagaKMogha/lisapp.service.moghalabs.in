var augnitoClient;
$(document).ready(function () {

    var appLogic = {};
    appLogic.HyperTextControl = $("#FloatingHyperText")
    appLogic.OnChangeMicState = function (isConnected) {
        //This will be called when mic recording start OR stop to update UI interface mic button.
        if (isConnected) {
            appLogic.HyperTextControl.html("");
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

    appLogic.CommandAndControlCallback = function (ActionRecipe) {

        if (appLogic.InterfaceCommand(ActionRecipe)) {
            return;
        }
        var isFroalaEditor = FroalaEditorProcess.IsActiveElementFroalaEditor();
        if (isFroalaEditor) {
            FroalaEditorProcess.ProcessCommand(ActionRecipe);
        }
    };
    appLogic.FinalResultCallback = function (ActionRecipe) {

        // This final result that need to typed in Editor as dictated speech output.
        var isCkEditor = FroalaEditorProcess.IsActiveElementFroalaEditor();
        if (isCkEditor) {
            // Handled new line character
            var newProcessText = ActionRecipe.ReceivedText;
            newProcessText = newProcessText.replace(/\n/gi, "@newline@");
            FroalaEditorProcess.FroalaEditorWriter(newProcessText);
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
            localStorage.setItem("WebSocketConnectionStatus", "On");
            // After successful authenticate, server creates an unique ID for each speech session and sends it back to client app for reference.
            // Client app can store this is it requires.              
        }
        else if (eventType == "SERVICE_DOWN") {
            // Very rare, But This event will come when Speech server's any internal component down.
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
        }
    }
    appLogic.onPartialResults = function (response) {
        // Partial output against audio stream started to server. This is not final and keep changing.
        // Use this to show user that system started listing, and Processing your voice.

        var partialText = response.Result.Transcript;
        if (partialText && partialText.length > 52) {
            partialText = '..' + partialText.substring(partialText.length - 50);
        }
        appLogic.HyperTextControl.html(partialText);
    }
    appLogic.onFinalResults = function (response) {
        // Prepare Action Recipe from speech output to pass in editor handle.
        // When System make output final this even will be call.
        // It can be either static command, or normal transcription.

        appLogic.HyperTextControl.html('');
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
    },
        appLogic.onEndOfSession = function () {
            // On mic off and connection close
            this.OnChangeMicState(false);
        },
        appLogic.onError = function (code, data) {
            if (appLogic.EnableLogs) {
                console.log("ERR: " + code + ": " + (data || ''));
            }
        }
    appLogic.onEvent = function (eventCode, data) {

        // All these events are from client side SDK only for debugging purpose.
        // Speech server will not called this 

        switch (eventCode) {

            case AugnitoSDKEvent.WS_CONNECTING:
                appLogic.HyperTextControl.html(data);
                break;
            case AugnitoSDKEvent.MSG_MEDIA_STREAM_CREATED:
                // code block
                break;
            case AugnitoSDKEvent.MSG_INIT_RECORDER:
                // code block
                break;
            case AugnitoSDKEvent.MSG_RECORDING:
                // code block
                break;
            case AugnitoSDKEvent.MSG_SEND_EMPTY:
                // code block
                break;
            case AugnitoSDKEvent.MSG_WEB_SOCKET_OPEN:
                // code block
                break;
            case AugnitoSDKEvent.MSG_WEB_SOCKET_CLOSE:
                appLogic.HyperTextControl.html("");
                // code block
                break;
            case AugnitoSDKEvent.MSG_STOP:
                // code block
                break;
            default:
            // code block
        }

    },
    appLogic.performMicOnAction = function () {
        try {
            $("#DuplicateMicWarningBox-dialog").dialog("close");
            $('#DuplicateMicWarningBox-dialog').remove();

            $('<div id="DuplicateMicWarningBox-dialog">' +
                '<p id="timeout-message1">Augnito mic is ON in another tab. Click "Use Here" to dictate in this tab.</p>' +
                '</div>')
                .appendTo('body')
                .dialog({
                    modal: true,
                    width: '351px',
                    minHeight: 'auto',
                    zIndex: 1000000000,
                    closeOnEscape: true,
                    draggable: false,
                    resizable: false,
                    closeText: '',
                    close: function () {
                        $('#DuplicateMicWarningBox-dialog').remove();
                    },
                    dialogClass: 'messageBox-dialog',
                    title: 'Augnito Mic',
                    buttons: {
                        'Confirm-button': {
                            text: 'Use Here',
                            id: "contunue-template-btn",
                            class: 'btnMacroButton btnMacroButtonAdd',
                            click: function () {
                                $(this).dialog("close");
                                $('#DuplicateMicWarningBox-dialog').remove();

                                var currentMicStatus = localStorage.getItem('WebSocketConnectionStatus');
                                if (currentMicStatus == "Off") {
                                    augnitoClient.startListening();
                                }
                                else {
                                    requiredMicRestart = true;
                                    localStorage.setItem('WebSocketConnectionStatus', 'RequestOff');
                                }

                            }
                        },
                        'cancel-button': {
                            text: 'Close',
                            class: 'btnMacroButton btnMacroCancel',
                            id: "cancel-template-button",
                            click: function () {
                                $(this).dialog("close");
                                $('#DuplicateMicWarningBox-dialog').remove();
                                augnitoClient.getConfig().onEvent(AugnitoSDKEvent.MSG_WEB_SOCKET_CLOSE, '');
                            }
                        }
                    }
                });
        } catch (e) {

        }
    }

    augnitoClient = GetAugnitoClient(appLogic);
    $("#btnAugnitoMic").click(function () {
        augnitoClient.toggleListening();
    });

    var QRCodeData = appLogic.AccountCode + "|" + appLogic.AccessKey + "|" + appLogic.UserTag + "|" + DeviceId + "|" + appLogic.LmId + "|" + appLogic.SourceApp + "|" + appLogic.DomainName + "|" + appLogic.Version;
    new QRCode(document.getElementById("qrcode"), QRCodeData);

    createMobileWebSocket();

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
    window.addEventListener("beforeunload", function (e) {
        augnitoClient.stopListening();
    });
});