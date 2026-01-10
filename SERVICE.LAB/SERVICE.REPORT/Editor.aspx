<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Editor.aspx.cs" Inherits="DEV.ReportService.Editor.Editor" %>

<%@ Register Assembly="DevExpress.Web.ASPxSpellChecker.v22.2, Version=22.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxSpellChecker" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxRichEdit.v22.2, Version=22.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxRichEdit" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v22.2, Version=22.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v22.2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="./Augnito/lib/Augnito/AugnitoCMDs.js?v=3"></script>
    <script src="./Augnito/lib/Augnito/AugnitoCMDStatic.js?v=3"></script>
    <script src="./Augnito/lib/Augnito/AugnitoCMDRegex.js?v=3"></script>
    <script src="./Augnito/lib/Augnito/TextWordToInteger.js?v=3"></script>
    <script src="./Augnito/lib/Augnito/RecordRTC.js"></script>
    <script src="./Augnito/lib/Augnito/AugnitoSDK.js?v=3"></script>
    <script src="./Augnito/lib/jquery.js"></script>
    <script src="./Augnito/lib/MobileMic/GetSetCookie.js"></script>
    <script src="./Augnito/lib/MobileMic/ProcessNotification.js"></script>
    <script src="./Augnito/lib/MobileMic/qrcode.min.js"></script>
    <script src="./Augnito/lib/PhilipsMicHandler.js?v=3"></script>
    <script src="./Augnito/lib/jquery-ui/jquery-ui.js?v=1"></script>
    <script src="./Augnito/DemoJS/InitializeAugnito.js?v=3"></script>
    <script src="./Augnito/lib/HtmlFormEditorProcess.js?v=3"></script>
    <script src="./Augnito/DemoJS/HtmlFormLogic.js?v=3"></script>
    <link href="./Augnito/lib/jquery-ui/jquery-ui.css?v=1" type="text/css" rel="stylesheet" />
    <link href="./Augnito/DemoCSS/AugnitoStyle.css?v=3" type="text/css" rel="stylesheet" />
    <link href="./Augnito/DemoCSS/menu.css?v=1" type="text/css" rel="stylesheet" />

    <link rel="stylesheet" href="../Augnito/DemoCSS/bootstrap.min.css?v=1">


    <!-- Bootstrap -->
    <script type="text/javascript" src='./Augnito/lib/jquery-ui/jquery-1.8.3.min.js'></script>
    <script type="text/javascript" src='./Augnito/lib/jquery-ui/bootstrap.min.js'></script>
    <script type="text/javascript" src="./Augnito/lib/jquery-ui/moment.min.js"></script>
    <link rel="stylesheet" href='./Augnito/lib/jquery-ui/bootstrap.min.css' media="screen" />
    <!-- Bootstrap -->

    <style>
        html,
        body {
            height: auto;
        }
    </style>
    <script type="text/javascript">
        function onUploadControlFileUploadComplete(s, e) {
            if (e.isValid)
                RichEdit.commands.fileOpen.execute(e.callbackData);
        }

    </script>
    <script>
        $(document).ready(function () {
            //debugger;
            $('#augnitoMicBar').hide();
        });

        $(document).keydown(function (event) {
            if (event.ctrlKey && event.which === 83) {
                alert("Ctrl+S");
                event.preventDefault();
            }
        });

        function onCustomCommandExecuted(s, e) {
            if (e.commandName == 'showSpellingDialog')
                s.commands.openSpellingDialog.execute();

            if (e.commandName == 'voiceToText') {

                $('#augnitoMicBar').show();
                $("#btnAugnitoMic").click()

                voiceToText.Show();
            }
        }

    </script>

    <%-- Augnito --%>

    <script type="text/javascript">

        function openMenu() {
            var popup = document.getElementById("menuOptions");
            popup.classList.toggle("show");
        }

        function openMacro() {
            var userTag = '';
            if (typeof URLSearchParams != "undefined") {
                var urlParams = new URLSearchParams(window.location.search);
                if (urlParams.has('UserTag')) {
                    userTag = urlParams.get('UserTag');
                }
            }

            if (userTag == '') {
                $("#UserTagDialog").dialog("open");
                return;
            }
            var macroWindows = window.open("ManageMacro.html?UserTag=" + userTag, "", "scrollbars=no,resizable=no,status=no,location=no,toolbar=no,menubar=no,width = 867, height = 500");
        }

        function openVocabulary() {
            var userTag = '';
            if (typeof URLSearchParams != "undefined") {
                var urlParams = new URLSearchParams(window.location.search);
                if (urlParams.has('UserTag')) {
                    userTag = urlParams.get('UserTag');
                }
            }

            if (userTag == '') {
                $("#UserTagDialog").dialog("open");
                return;
            }
            var vocabularyWindows = window.open("ManageVocabulary.html?UserTag=" + userTag, "", "scrollbars=no,resizable=no,status=no,location=no,toolbar=no,menubar=no,width = 867, height = 700");
        }

        function openFormatting() {
            var userTag = '';
            if (typeof URLSearchParams != "undefined") {
                var urlParams = new URLSearchParams(window.location.search);
                if (urlParams.has('UserTag')) {
                    userTag = urlParams.get('UserTag');
                }
            }
            if (userTag == '') {
                $("#UserTagDialog").dialog("open");
                return;
            }
            var formattingWindows = window.open("ManageFormatting.html?UserTag=" + userTag, "", "scrollbars=no,resizable=no,status=no,location=no,toolbar=no,menubar=no,width = 867, height = 700");
        }

        function openPairMic() {
            $("#QRCodeDialog").dialog("open");
        }

        function openCommandHelp() {
            $("#commandHelp").dialog("open");
        }

        /*
        $(document).ready(function () {
            $("#mnhtmlform").addClass("active");
            $("#MessageDialog").dialog({
                autoOpen: false,
                modal: true
            });

            $("#UserTagDialog").dialog({
                autoOpen: false,
                modal: true,
            });

            $("#QRCodeDialog").dialog({
                autoOpen: false,
                modal: true,
                width: "307px"
            });

            $("#commandHelp").dialog({
                autoOpen: false,
                modal: true,
                width: "600px"
            });

            $("#MicOffDialog").dialog({
                autoOpen: false,
                modal: true,
                buttons: {
                    "Keep Mic On": function () {
                        clearInterval(stopInterval);
                        MicCloseInSec = 10;
                        $(this).dialog("close");
                    }
                }
            });
            $("#augnitoMicBar").draggable();
        });
        */
        var MicCloseInSec = 10;
        var stopInterval;
        function HandleMicOff() {
            $("#MicOffDialog").dialog("open");
            $("#lblTimer").html(MicCloseInSec);
            stopInterval = setInterval(() => {
                if (MicCloseInSec > 0) {
                    MicCloseInSec--;
                    $("#lblTimer").html(MicCloseInSec);
                } else {
                    clearInterval(stopInterval);
                    MicCloseInSec = 10;
                    $("#btnAugnitoMic").click();
                    $("#MicOffDialog").dialog("close");
                }
            }, 1000)
        }
    </script>

    <%-- Augnito End --%>
</head>
<body>

    <%-- Augnito  --%>

    <div style="display: none">

        <div id="MessageDialog" title="Warning">
            <p id="txtMessageInformation">Invalid authentication, Please check your Account Status, Lm Id and Access Key.</p>
        </div>
        <div id="UserTagDialog" title="Warning">
            <p>User Tag is not defined into URL.</p>
        </div>

        <div id="MicOffDialog" title="Switch Off Augnito Mic">
            <p>It seems you are not using Augnito. Do you want to keep the Mic on?</p>
            <p>
                Mic switching off in
            <label id="lblTimer">10</label>
                sec..
            </p>
        </div>

        <div id="QRCodeDialog" title="Pair Augnito Mic">
            <div class="QRCodeParent">
                <div class="ScanQRCodeWithAugnitoMic" style="margin-bottom: 5px;">Scan QR code with Augnito Mic</div>
                <div style="text-align: center; padding: 8px; background-color: white; max-width: 286px; width: 286px;">
                    <div id="qrcode" class="" style=""></div>
                </div>
            </div>
        </div>
        <div id="commandHelp" title="COMMANDS">
            <div id="command-list-dialog">
                <div style="font-size: 12px;">Augnito is voice driven with naturally spoken commands. Here are some examples to get you started.</div>
                <div class="ControlListParent">
                    <div class="ControlListTitle" style="margin-top: 15px;">DICTATE</div>
                    <div class="ControlListCommand">Full Stop / Comma / Colon</div>
                    <div class="ControlListCommand">New line</div>
                    <div class="ControlListTitle">EDIT &amp; FORMAT</div>
                    <div class="ControlListCommand">Add Space</div>
                    <div class="ControlListCommand">Delete last &lt;n&gt; words</div>
                    <div class="ControlListCommand">Delete last &lt;n&gt; lines</div>
                    <div class="ControlListTitle">SELECTION</div>
                    <div class="ControlListCommand">Select last word</div>
                    <div class="ControlListCommand">Select last &lt;n&gt; words</div>
                    <div class="ControlListCommand">Select last &lt;n&gt; lines</div>
                    <div class="ControlListTitle">NAVIGATION</div>
                    <div class="ControlListCommand">Go to line start / Go to line end</div>
                    <div class="ControlListCommand">Go to document start / Go to document end</div>
                    <div class="ControlListCommand">Go to next filed</div>
                    <div class="ControlListCommand">Go to previous filed</div>
                    <div class="ControlListTitle">CONTROL NAVIGATION</div>
                    <div class="ControlListCommand">Go to patientId</div>
                    <div class="ControlListCommand">Go to Date of birth</div>
                    <div class="ControlListCommand">Go to Diagnosis</div>
                    <div class="ControlListCommand">Go to Medication</div>
                    <div class="ControlListCommand">Go to History</div>
                    <div class="ControlListTitle">CONTROL</div>
                    <div class="ControlListCommand">Stop Mic</div>
                </div>
            </div>

        </div>

    </div>
    <div id="augnitoMicBar" class="floatingWindow">
        <div class="augnitoFloatingParentElementStyle">
            <div class="FloatingHyperText" id="FloatingHyperText"></div>
            <button id="btnAugnitoMic" class="AugnitoStopButtonStyle"></button>
            <div class="MenuWrapper popup" onclick="openMenu()">
                <%-- <div class="popuptext" id="menuOptions">
                        <ul class="popupSubMenu">
                            <li onclick="openMacro()">Macros</li>
                            <li onclick="openVocabulary()">Vocabulary<img class="vocabNotif" src="./images/oval_orange.svg"></img></li>
                            <li onclick="openFormatting()">Formatting Preferences</li>
                            <li onclick="openPairMic()">Pair Mic</li>
                            <li onclick="openCommandHelp()">Command Help</li>

                        </ul>
                    </div>--%>
                <span class="MenuIcon">...</span>
            </div>
        </div>

    </div>

    <%-- Augnito End --%>


    <div>
        <form id="form1" runat="server" style="display: block">
            <asp:Button ID="Button1" runat="server" CssClass="hide" Text="Button" OnClick="Button1_Click" Height="0" Width="0" />
            <asp:Button ID="btnCannedtext" runat="server" CssClass="hide" OnClientClick="return DevExpressCannedtext();" UseSubmitBehavior="false" Height="0" Width="0" />
            <%-- Voice to Text modal dialog --%>
            <dx:ASPxPopupControl ID="voiceToText" runat="server" Width="650px" CloseAction="CloseButton" CloseOnEscape="True" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="voiceToText"
                HeaderText="Voice to text" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" AutoUpdatePosition="True">
                <ClientSideEvents
                    Closing="function(s, e) {  
                        $('#btnAugnitoMic').click();  
                        $('#augnitoMicBar').hide();
                    }"
                    PopUp="function(s, e) { }"
                    Shown="function(s, e) { $('#txtDiag')[0].focus(); }" />
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">

                        <dx:ASPxPanel ID="Panel1" runat="server" DefaultButton="btOK">
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    <div>
                                        <textarea id="txtDiag" class="form-control textareaCss report-input" rows="4"></textarea>
                                    </div>
                                    <dx:ASPxFormLayout runat="server" ID="ASPxFormLayout1" Width="100%" Height="100%">
                                        <Items>

                                            <dx:LayoutItem ShowCaption="False" Paddings-PaddingTop="19">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButton ID="btOK" runat="server" Text="OK" Width="80px" AutoPostBack="False" Style="float: left; margin-right: 8px">
                                                            <ClientSideEvents Click="function(s, e) { 
                                                                if(ASPxClientEdit.ValidateGroup('entryGroup')) voiceToText.Hide();
                                                                RichEdit.commands.insertText.execute($('#txtDiag')[0].value);
                                                                $('#txtDiag')[0].value ='';
                                                                }" />
                                                        </dx:ASPxButton>
                                                        <dx:ASPxButton ID="btCancel" runat="server" Text="Cancel" Width="80px" AutoPostBack="False" Style="float: left; margin-right: 8px">
                                                            <ClientSideEvents Click="function(s, e) { voiceToText.Hide(); }" />
                                                        </dx:ASPxButton>
                                                        <dx:ASPxButton ID="ASPxButton_Clear" runat="server" Text="Clear" Width="80px" AutoPostBack="False" Style="float: left; margin-right: 8px">
                                                            <ClientSideEvents Click="function(s, e) { 
                                                                    $('#txtDiag')[0].value ='';
                                                                    $('#txtDiag')[0].focus();
                                                                }" />
                                                        </dx:ASPxButton>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                                <Paddings PaddingTop="19px"></Paddings>
                                            </dx:LayoutItem>

                                        </Items>
                                    </dx:ASPxFormLayout>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxPanel>

                    </dx:PopupControlContentControl>
                </ContentCollection>
                <ContentStyle>
                    <Paddings PaddingBottom="5px" />
                </ContentStyle>
            </dx:ASPxPopupControl>

            <%-- Voice to Text modal dialog END --%>
            <style>
                .dxreControlSys {
                    height:650px !important;
                }
                .hide{
                    display:none;
                }
            </style>

            <div>
                <dx:ASPxRichEdit ID="ASPxRichEdit1" runat="server" OnSaving="RichEdit_Saving" Width="100%"
                    OnSpellCheckerWordAdded="ASPxRichEdit1_SpellCheckerWordAdded" ClientInstanceName="RichEdit">
                    <ClientSideEvents CustomCommandExecuted="onCustomCommandExecuted" />
                    <SettingsDialogs>
                        <SaveFileDialog DisplaySectionMode="ShowServerSection" />
                    </SettingsDialogs>

                    <Settings>
                        <Behavior SaveAs="Hidden" />
                        <AutoCorrect DetectUrls="True" ReplaceTextAsYouType="True"></AutoCorrect>
                        <Fonts AddDefaultFonts="False"></Fonts>
                        <RangePermissions Visibility="Auto"></RangePermissions>
                        <SpellChecker Enabled="true" SuggestionCount="4">
                            <Dictionaries>
                                <dx:ASPxSpellCheckerISpellDictionary
                                    GrammarPath="~/App_Data/Dictionaries/english.aff"
                                    DictionaryPath="~/App_Data/Dictionaries/american.xlg"
                                    Culture="English (United States)"
                                    CacheKey="enDic"></dx:ASPxSpellCheckerISpellDictionary>
                                <dx:ASPxSpellCheckerCustomDictionary DictionaryPath="~/App_Data/Dictionaries/custom.txt" />
                            </Dictionaries>
                        </SpellChecker>
                    </Settings>
                </dx:ASPxRichEdit>
                <%--    
                <dx:ASPxUploadControl ID="UploadControl" runat="server" UploadMode="Auto" AutoStartUpload="True" ClientVisible="false"
                    OnFileUploadComplete="UploadControl_FileUploadComplete1">
                    <AdvancedModeSettings EnableDragAndDrop="True" EnableFileList="False" EnableMultiSelect="False" ExternalDropZoneID="ASPxRichEdit1" />
                    <ClientSideEvents FileUploadComplete="onUploadControlFileUploadComplete" />
                    <ValidationSettings AllowedFileExtensions=".doc,.docx,.rtf,.txt" MaxFileSize="4194304" />
                </dx:ASPxUploadControl>--%>
            </div>

            <input id="hdntemplateno" runat="server" type="hidden" value="0" />
            <input id="hdnIsSave" runat="server" type="hidden" value="0" />
            <input id="hdnCommands" runat="server" type="hidden" value="" />
            <input id="hdnOrderlistNo" runat="server" type="hidden" value="0" />
            <input id="hdnTempType" runat="server" type="hidden" value="" />
            <input id="hdnDocumentId" runat="server" type="hidden" value="0" />
        </form>
    </div>
    <script type="text/javascript">
        function onKeyUp(s, e) {
            //debugger;
            if (e.htmlEvent.altKey === true && e.htmlEvent.shiftKey === true && e.htmlEvent.keyCode === 70) {
                //RichEdit.commands.insertText.execute('hai')
                ShortCutApiCall('altKey+shiftKey+f')
            }
        }
        function DevExpressCannedtext() {
            //debugger;
            let val = document.getElementById("hdnCommands").value;
            RichEdit.commands.insertText.execute(val);
            return false;
        }
        function DevExpressCannedtext() {
            //debugger;
            let val = document.getElementById("hdnCommands").value;
            RichEdit.commands.insertText.execute(val);
            return false;
        }
    </script>
</body>
</html>
