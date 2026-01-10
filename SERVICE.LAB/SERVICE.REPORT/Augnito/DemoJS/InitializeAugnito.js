var GetAugnitoClient = function (appLogic) {

    if (typeof URLSearchParams != "undefined") {
        var urlParams = new URLSearchParams(window.location.search);
        if (urlParams.has('UserTag')) {
            appLogic.UserTag = urlParams.get('UserTag');
        }
    }

    /*
     UAE - ae-apis.augnito.ai
    Australia - au-apis.augnito.ai
    India - apis.augnito.ai
    USA - us.apis.augnito.ai
    UK - uk.apis.augnito.ai
    KSA - sa-apis.augnito.ai
   */

    appLogic.DomainName = "apis.augnito.ai";

    appLogic.PushNotification = "wss://" + appLogic.DomainName + "/speechapi/notification/";
    appLogic.SpeechMicURL = "wss://" + appLogic.DomainName + "/v2/speechapi/mobile/client/";

    appLogic.MacroServiceURL = "https://" + appLogic.DomainName + "/manage/v2/";
    appLogic.PersonalizationURL = "https://" + appLogic.DomainName + "/db/speechapi/";

    appLogic.Server = "wss://" + appLogic.DomainName + "/v2/speechapi";
    appLogic.AccountCode = "944200e5-faf9-4193-bb8d-0e66f8c36885";
    appLogic.AccessKey = "7b1a2b82760e4b329656eb063d1db4d1";
    appLogic.Version = 1;
    appLogic.ContentType = "audio/x-raw,+layout=(string)interleaved,+rate=(int)16000,+format=(string)S16LE,+channels=(int)1";
    appLogic.NoiseCt = "1";
    appLogic.LmId = 211801206;  

    if (typeof appLogic.UserTag == "undefined") {
        appLogic.UserTag = "UserTag"; /*user name or user unique information*/
    }
    appLogic.LoginToken = "userLoginToken"; /*Login token or unique login id*/
    appLogic.OtherInfo = "otherinfo";
    appLogic.SourceApp = "SDKdemo";
    return new AugnitoSDK(appLogic);

}
this.GetAugnitoClient = GetAugnitoClient;
 