var pushNotificationSocket;
var DeviceId = GetDeviceId();
var debugLogRequired = true;
var currentTabId = create_UUID();
var requiredMicRestart = false;
var mobileConnected = false;

function createMobileWebSocket() {

    if (closeMobileWebSocket()) {
        return;
    }
    var activeTabId = localStorage.getItem('ActiveBrowserTab');
    if (activeTabId != currentTabId) {
        return;
    }

    if (pushNotificationSocket != null && pushNotificationSocket.readyState == 0) {
        return;
    }
    else if (pushNotificationSocket == null || pushNotificationSocket.readyState == 3 || pushNotificationSocket.readyState == 2) {
        if (pushNotificationSocket != null) {
            pushNotificationSocket.close();
            pushNotificationSocket = null;
        }        
        if (debugLogRequired) {
            console.log("Push Notification Connecting " + currentTabId);
        }
        var deviceCodeUrl = "?devicecode=" + DeviceId;
        var currentTabURL = "&currentTabId=" + currentTabId;
        var accountURL = "&accountcode=" + augnitoClient.getConfig().AccountCode;
        var accesskeyURL = "&accesskey=" + augnitoClient.getConfig().AccessKey;
        
        var url = augnitoClient.getConfig().PushNotification + deviceCodeUrl + currentTabURL + accountURL + accesskeyURL;
        url = url.replace(new RegExp("amp;", "gi"), "");
        pushNotificationSocket = new WebSocket(url);
    }    

    pushNotificationSocket.onmessage = function (e) {
        if (e.data) {
            var jsonResponse = JSON.parse(e.data);

            if (DeviceId && (DeviceId != jsonResponse.ToDeviceId)) {
                pushNotificationSocket.send(GetReplyMessage(JSONResponse, 'WRONG_DESTINATION'))
                return;
            }

            if ("Type" in jsonResponse) {
                if (jsonResponse.Type == 'MOBILE_SCAN_SUCCESSFUL') {
                    $("#QRCodeDialog").dialog("close");
                    mobileConnected = true;
                }
                else if (jsonResponse.Type == 'MIC_CONNECTION_REQUEST') {
                    createWebSocketForMobileMic(jsonResponse);
                    mobileConnected = true;
                }
                else if (jsonResponse.Type=='GENERAL_NOTIFICATION') {
                    if(jsonResponse.Data.AppNotification && jsonResponse.Data.AppNotification.SubType=='Vocabulary') {
                        
                        localStorage.setItem('VocabAdded','true');
                        $('.vocabNotif').show();
                        $('body').append("<div class='toast'>New word(s) added to vocabulary. <a id='restartMic'>Restart mic</a> to start using them</div>");
                        setTimeout(function () {
                            localStorage.removeItem('VocabAdded');
                            $('.toast').remove();
                        }, 3000);
                    }
                }
            }
        }
    };

    pushNotificationSocket.onopen = function (e) {        
        console.log("Socket Connection Opened.");
    };

    pushNotificationSocket.onclose = function (e) {        
        console.log("Socket Connection Closed.");
        pushNotificationSocket = null;
    };

    pushNotificationSocket.onerror = function (e) {        
        console.log("Socket Connection Error.");        
        pushNotificationSocket = null;
    };
}

function closeMobileWebSocket() {
    var currentTabURL = "currentTabId=" + currentTabId;
    if (pushNotificationSocket != null && currentTabId != undefined) {
        var isCurrentTabSocket = pushNotificationSocket.url.indexOf(currentTabURL) > 0;
        var activeTabId = localStorage.getItem('ActiveBrowserTab');
        if (activeTabId != currentTabId && pushNotificationSocket != null) {
            pushNotificationSocket.close();
            pushNotificationSocket = null;
            return true;
        }
    }
    return false;
}

var watcher;
function PushNotificationWatcher() {
    watcher = setInterval(CheckAndCreateWS, 20000);
}

function CheckAndCreateWS() {
    if (pushNotificationSocket == null || pushNotificationSocket.readyState != 1) {
        createMobileWebSocket();
    }
}

function createWebSocketForMobileMic(JSONResponse) {
   
    var currentMicStatus = localStorage.getItem('WebSocketConnectionStatus');
    if (currentMicStatus == "On") {    
        pushNotificationSocket.send(GetReplyMessage(JSONResponse, 'DEVICE_ALREADY_IN_USE'))
        return;
    }

    if (augnitoClient.getConfig().UserTag && (augnitoClient.getConfig().UserTag != JSONResponse.Data.UserTag)) {
        pushNotificationSocket.send(GetReplyMessage(JSONResponse, 'DIFFERENT_LOGIN_USER'))
        return;
    }
    
    if (augnitoClient.getConfig().UserTag) {

        var SocketUrl = augnitoClient.getConfig().SpeechMicURL;

        var accountURL = "accountcode=" + augnitoClient.getConfig().AccountCode;
        var usertagURL = "&usertag=" + augnitoClient.getConfig().UserTag;
        var sequenceIdURL = "&seqid=" + JSONResponse.SeqId;
        var mobileDeviceCodeURL = "&mobiledevicecode=" + JSONResponse.FromDeviceId;
        var clientDeviceCodeURL = "&clientdevicecode=" + DeviceId;

        SocketUrl = SocketUrl + "?" + accountURL + usertagURL + sequenceIdURL + mobileDeviceCodeURL + clientDeviceCodeURL;
        SocketUrl = SocketUrl.replace(new RegExp("amp;", "gi"), "");
        augnitoClient.startListening(SocketUrl);
    }
}

function GetReplyMessage(JSONResponse,messageType) {
    var NotificationMessage = new Object();
    NotificationMessage.FromDeviceId = DeviceId;
    NotificationMessage.ToDeviceId = JSONResponse.FromDeviceId;
    NotificationMessage.SeqId = create_UUID();
    NotificationMessage.AckOf = JSONResponse.SeqId;
    NotificationMessage.Type = messageType;
    NotificationMessage.TimeStamp = GetCurrentTimestamp();
    return JSON.stringify(NotificationMessage)
}

function create_UUID() {
    var dt = new Date().getTime();
    var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = (dt + Math.random() * 16) % 16 | 0;
        dt = Math.floor(dt / 16);
        return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
    });
    return uuid;
}

function GetCurrentTimestamp() {
    var today = new Date();
    var date = today.getFullYear() + '' + ("0" + (today.getMonth() + 1)).slice(-2) + '' + ("0" + (today.getDate())).slice(-2);
    var time = ("0" + (today.getHours())).slice(-2) + '' + ("0" + (today.getMinutes())).slice(-2) + '' + ("0" + (today.getSeconds())).slice(-2) + '' + ("000" + (today.getMilliseconds())).slice(-4);
    var dateTime = date + '' + time;
    return dateTime;
}

function GetDeviceId() {
    var DeviceId = getCookie("DeviceId");
    if (DeviceId == "undefined") {
        DeviceId = create_UUID();
        setCookie("DeviceId", DeviceId, 365);
    }
    return DeviceId;
}

function HandleFocusEvent() {
    localStorage.setItem("ActiveBrowserTab", currentTabId);
    if (debugLogRequired) {
        console.log("Active Tab Change to " + currentTabId);
    }
    if (augnitoClient) {
        createMobileWebSocket();
    }
}

function RequestMicOff() {
    var currentMicStatus = localStorage.getItem('WebSocketConnectionStatus');
    if (currentMicStatus == "On") {
        //Display Message and Stop Mic
        localStorage.setItem('WebSocketConnectionStatus', 'RequestOff');
        //MicOffDialog();
        //TODO: Show Message to active mic here
    }
}

function RestartMic(){
    if(augnitoClient.isListening()){
        // The mic is already listening, so we need to stop it first
        // however the change event won't be fired because it's the same page
        augnitoClient.stopListeningPromise().then(
            function(){
                if($('.vocabNotif').length){
                    localStorage.removeItem('VocabAdded')
                    $('.vocabNotif').hide();
                }
                // Now we can start listening again
                if(mobileConnected){
                    mobileConnected = false;
                }
                else{
                    augnitoClient.startListening();
                }
            },
            function(errorReason){
                console.log(errorReason)
                // There was an error stopping the mic
            }
        )
    }
    else{
        augnitoClient.startListening();
    }
}


function HandleStorageChangeEvent(event) {
    
    if (event.key == "WebSocketConnectionStatus") {
        var currentMicStatus = localStorage.getItem('WebSocketConnectionStatus');
        if (currentMicStatus == "RequestOff") {
            //Stop Mic when page load and when mic starting from another tab
            augnitoClient.stopListening();
        }
        else if (currentMicStatus == "Off" && requiredMicRestart) {
            requiredMicRestart = false;
            augnitoClient.startListening();
        }
        else if (currentMicStatus== "RequestRestart") {
            RestartMic();
        }
          
    }
    else if (event.key == "ActiveBrowserTab") {
        var activeTabId = localStorage.getItem('ActiveBrowserTab');
        closeMobileWebSocket();
        if (debugLogRequired) {
            console.log("Received Tab Change to " + activeTabId);
        }
    }

}

$(window).on('load', function (event) {
    HandleFocusEvent();
});

$(window).on('focus', function (event) {
    HandleFocusEvent();
});

window.addEventListener('storage', function (event) {
    HandleStorageChangeEvent(event);
});

$(document).on('click','#restartMic', function(ev){
    ev.preventDefault();
    RestartMic();
})    
