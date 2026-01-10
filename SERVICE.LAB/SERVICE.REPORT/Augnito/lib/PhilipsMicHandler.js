var gpButtonMapping = ["FRWD", "PLAY", "FFWD", "EOL", "INSOVR", "REC", "CMD", "BB2", "STOP", "INSTR", "F1", "F2", "F3", "F4", "EOL"];
var isRecodingIsPressed = false;

function gamePadCallback() {

    var gamepads = navigator.getGamepads();

    for (var index = 0; index < gamepads.length; index++) {
        var pad = gamepads[index];
        if (pad) {
            var connectedPhilipsDeviceStr = GetConnectedPhilipsDeviceStr(pad.id);

            if (connectedPhilipsDeviceStr == "") {
                continue;
            }

            var message = '';
            for (var i = 0; i < pad.buttons.length; i++) {
                if (gpButtonMapping[i] === "REC") {
                    if (pad.buttons[i].pressed) {
                        isRecodingIsPressed = true;
                        message = gpButtonMapping[i] + " pressed on " + connectedPhilipsDeviceStr;
                    }
                    else if (isRecodingIsPressed) {
                        isRecodingIsPressed = false;
                        message = gpButtonMapping[i] + " released on " + connectedPhilipsDeviceStr;
                        $("#btnAugnitoMic").trigger("click");
                    }
                    //Message only for debug purpose, will remove for in final package
                    if (message !== '') {
                        console.log(message);
                    }
                }
            }
        }
    }
    window.requestAnimationFrame(gamePadCallback);
}

function GetConnectedPhilipsDeviceStr(idStr) {
    if (idStr.toLowerCase().indexOf("911") !== -1 && idStr.indexOf("fa0") !== -1) {
        return "SpeechMike";
    }

    if (idStr.toLowerCase().indexOf("911") !== -1 && idStr.indexOf("c1e") !== -1) {
        return "SpeechOne";
    }

    if (idStr.toLowerCase().indexOf("911") !== -1 && (idStr.indexOf("1844") !== -1 || idStr.indexOf("91a") !== -1)) {
        return "Footswitch";
    }

    return "";
}

function HandleEditorKeyDown(s, e)
{
    if (e.htmlEvent.altKey && e.htmlEvent.shiftKey && e.htmlEvent.which === 82)
    {
        console.log(e);
        e.handled = true;
        $('#btnAugnitoMic').trigger('click');
    }
}

$(document).ready(function () {
    if (navigator.getGamepads !== undefined) {
        window.requestAnimationFrame(gamePadCallback);
    }
    document.onkeydown = function (e) {
        if (e.altKey && e.shiftKey && e.which === 82) {
            $("#btnAugnitoMic").trigger("click");
            e.preventDefault();
        }
    };
});