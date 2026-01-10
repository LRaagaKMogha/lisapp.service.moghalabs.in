CKEditorProcess = {};

CKEditorProcess.IsActiveElementCkEditor = function ()
{
     // Find active CK Editor-4
    var activeDocumentElement = CommonEditorProcess.GetActiveEditorElement();
    var isCkEditor = false;
    
     if (activeDocumentElement.hasClass("cke_wysiwyg_frame") || activeDocumentElement.hasClass("cke_editable")) {
         // To make sure active element is CK Editor and not other textbox
         isCkEditor = true;
     }
     return isCkEditor;
}

// Callback function to handle insert text 
CKEditorProcess.InsertTextCallback = function (editorInstance, insertText) {
    if(insertText == "\n") {
        editorInstance.commands.enter.exec(); 
    } else {
        editorInstance.insertText(insertText);
    }
}
CKEditorProcess.CkEditorWriter = function (newProcessText) {

    var IsLastFullstop = false;
    var IsLastNewLine = false;
    var cursorInSameLine = false;
    var lastChar = "";    

    var result = CKEditorProcess.FindInstance();
    var CkEditorObject = result.CkEditorObject;
    var currentEditor = result.CurrentEditor;
    currentEditor.focus();

    var selection = currentEditor.getSelection();
    var range = selection.getRanges()[0];
    var rangeClone = range.clone();
    var currentPosition = range.startOffset;
    
    // Reverse back to '\n', client app may not need this. replace flow of new line.
    newProcessText = newProcessText.replace(new RegExp("@newline@", "gi"), "\n");
    var startNode = range.startContainer;
    if (currentPosition > 0) {
        var resultLastChar = CKEditorProcess.FindLastChar(startNode, range, CkEditorObject,currentEditor);
        cursorInSameLine = resultLastChar.CursorInSameLine;
        lastChar = resultLastChar.LastChar;
        IsLastFullstop = resultLastChar.IsLastFullstop;
        IsLastNewLine = resultLastChar.IsLastNewLine;
    }
    currentEditor.getSelection().removeAllRanges();
    currentEditor.getSelection().selectRanges([rangeClone]);
    newProcessText = CommonEditorProcess.ProcessText(newProcessText, IsLastFullstop, IsLastNewLine, currentPosition, cursorInSameLine);    
    CommonEditorProcess.InsertTextAtCursor(currentEditor, newProcessText, CKEditorProcess.InsertTextCallback);    
}

// Unselect previous selection in editor
CKEditorProcess.UnSelectCurrentSelection = function (currentEditor) {

    var oldRange = currentEditor.getSelection().getRanges()[0];
    var newRange = currentEditor.createRange();
    newRange.setStart(oldRange.endContainer, oldRange.endOffset);
    newRange.setEnd(oldRange.endContainer, oldRange.endOffset);
    currentEditor.getSelection().selectRanges([newRange]);
}

//Find last character from cursor position
CKEditorProcess.FindLastChar = function (startNode, range, CkEditorObject, currentEditor) {

    var lastChar = "";
    var cursorInSameLine = false;
    var currentLineText = "";
    // Get last character from cursor position in same or previous line.
    if (startNode.type == CkEditorObject.NODE_TEXT && range.startOffset) {
        var charIndex = range.startOffset;
        // Range at the non-zero position of a text node.
        currentLineText = startNode.getText();
        lastChar = currentLineText[charIndex - 1];
        if (lastChar.trim() == "") {
            for (var i = charIndex - 2; i >= 0; i--) {
                lastChar = startNode.getText()[i];
                //console.log(lastChar);
                if (lastChar.trim() != "") {
                    break;
                }
            }
        }
        cursorInSameLine = true;
    }
    if (lastChar.trim() == "") {

        // Expand the range to the beginning of editable.
        range.collapse(true);
        cursorInSameLine = true;
        range.setStartAt(currentEditor.editable(), CkEditorObject.POSITION_AFTER_START);

        // Let's use the walker to find the closes (previous) text node.
        var walker = new CkEditorObject.dom.walker(range),
            node;

        // Fine all previous node.
        while ((node = walker.previous())) {
            // If found, return the last character of the text node.
            //console.log(node);
            if (node.type == CkEditorObject.NODE_ELEMENT) {
                var nodeHtml = "";

                if (typeof node.getHtml == "function") {
                    nodeHtml = node.getHtml();
                    if (currentLineText === "" || nodeHtml == "<br>") {
                        cursorInSameLine = false;
                        break;
                    }
                }
            }
            else if (node.type == CkEditorObject.NODE_TEXT) {
                currentLineText = node.getText();
                var nodeHtml = "";

                if (typeof node.getHtml == "function") {
                    nodeHtml = node.getHtml();
                }
                lastChar = currentLineText.slice(-1);

                if (currentLineText === "" || nodeHtml == "<br>") {
                    cursorInSameLine = false;
                }
                if (lastChar.trim() == "") {
                    for (var i = currentLineText.length - 2; i >= 0; i--) {
                        lastChar = currentLineText[i];
                        if (lastChar != "") {
                            break;
                        }
                    }
                }
                if (lastChar.trim() != "") {
                    break;
                }
            }
        }
    }

    var resultObject = {};
    resultObject.LastChar = lastChar;
    resultObject.CursorInSameLine = cursorInSameLine;
    resultObject.IsLastFullstop=false;
    resultObject.IsLastNewLine=false;
    if (lastChar != "") {
        // Check cursor has full Stop before it
        if (("." == lastChar) || (":" == lastChar)) {
            resultObject.IsLastFullstop = true;
        }
        else if (("\r\n" == lastChar) || ("\n" == lastChar) || ("\r" == lastChar)) {
            resultObject.IsLastNewLine = true;
        }
    }
    return resultObject;
}


//Move cursor to previous location
CKEditorProcess.SelectEditorRange = function (newRange, oldRange, currentEditor) {
    newRange.setStart(oldRange.endContainer, oldRange.endOffset);
    newRange.setEnd(oldRange.endContainer, oldRange.endOffset);

    currentEditor.getSelection().selectRanges([newRange]);
}

//Select CKEditor elements for selections
CKEditorProcess.ProcessSelectCommand = function (selectedEndIndex, selectSartIndex, selectNodeList, currentEditor, CkEditorObject, selection) {
    var firstNode = selectNodeList[selectNodeList.length - 1];
    var lastNode = selectNodeList[0];
    var lastNodeTextLength = lastNode.getText().length;
    var firstNodeStart = selectSartIndex;
    var lastNodeEnd = (selectedEndIndex == -1 || lastNodeTextLength < selectedEndIndex) ? lastNodeTextLength : selectedEndIndex;

    var tempRange = new CkEditorObject.dom.range(currentEditor.document);
    tempRange.startOffset = firstNodeStart;
    tempRange.endOffset = lastNodeEnd;

    tempRange.startContainer = firstNode;
    tempRange.endContainer = lastNode;

    selection.selectRanges([tempRange]);

    oldRange = currentEditor.getSelection().getRanges()[0];
    // If Element has strong, U and em then selection will not work know need to reselect with parent
    if (oldRange.endOffset == oldRange.startOffset) {
        //Make center selection.
        tempRange = new CkEditorObject.dom.range(currentEditor.document);
        tempRange.startOffset = firstNodeStart;
        tempRange.endOffset = lastNodeEnd;

        tempRange.startContainer = firstNode;
        tempRange.endContainer = lastNode;

        if (firstNode.getParent().$.localName == "strong" || firstNode.getParent().$.localName == "u" || firstNode.getParent().$.localName == "em") {
            tempRange.startContainer = firstNode.getParent();
        }

        if (lastNode.getParent().$.localName == "strong" || lastNode.getParent().$.localName == "u" || lastNode.getParent().$.localName == "em") {
            tempRange.endContainer = lastNode.getParent();
        }

        selection.selectRanges([tempRange]);
    }
}

//Execute CK Editor commands
CKEditorProcess.ExecuteCommand = function (currentEditor, SelectFor, rangeSelection, ranegAlreadySelected, isBoldElement, isItalicElement, isUnderLineElement) {

    if ("bold" == SelectFor.toLowerCase() && (!isBoldElement || ranegAlreadySelected)) {
        currentEditor.commands.bold.exec();
    }
    else if ("unbold" == SelectFor.toLowerCase() && (isBoldElement || ranegAlreadySelected)) {
        currentEditor.commands.bold.exec();
    }
    else if ("italicize" == SelectFor.toLowerCase() && (!isItalicElement || ranegAlreadySelected)) {
        currentEditor.commands.italic.exec();
    }
    else if ("unitalicize" == SelectFor.toLowerCase() && (isItalicElement || ranegAlreadySelected)) {
        currentEditor.commands.italic.exec();
    }
    else if ("underline" == SelectFor.toLowerCase() && (!isUnderLineElement || ranegAlreadySelected)) {
        currentEditor.commands.underline.exec();
    }
    else if ("deunderline" == SelectFor.toLowerCase() && (isUnderLineElement || ranegAlreadySelected)) {
        currentEditor.commands.underline.exec();
    }
    else if ("capitalize" == SelectFor.toLowerCase() || "uncapitalize" == SelectFor.toLowerCase()) {

    }
    else if ("delete" == SelectFor.toLowerCase()) {
        if (rangeSelection.startOffset != undefined && rangeSelection.startOffset != "undefined") {
            rangeSelection.deleteContents();
        }
        else {
            currentEditor.insertText('');
        }
    }
    else if ("copy" == SelectFor.toLowerCase()) {
        currentEditor.commands.copy.exec();
    }
    else if ("cut" == SelectFor.toLowerCase()) {
        currentEditor.commands.cut.exec();
    }
}

//Find all previous node from current position
CKEditorProcess.PreparePreviousNodes = function (selectedPreviousWords, range, CkEditorObject) {
    var walker = new CkEditorObject.dom.walker(range), node;
    var isCursorStartOfLine = null;
    var returnObject = {};
    while ((node = walker.previous())) {

        if(isCursorStartOfLine == null) {
            if( node.type == CKEDITOR.NODE_ELEMENT && node.$.localName == "br") {
                isCursorStartOfLine = true;
            }
            else if( node.type == CKEDITOR.NODE_TEXT) {
                isCursorStartOfLine = false;
            }
        }
        selectedPreviousWords.push(node);
    }
    if(isCursorStartOfLine == null) {
        isCursorStartOfLine = false;
    }
    returnObject["selectedPreviousWords"] = selectedPreviousWords;
    returnObject["isCursorStartOfLine"] = isCursorStartOfLine;
    return returnObject
}

//Find number of words from node and select elements for processing
CKEditorProcess.ProcessNodes = function (node, rangeSelection, processedWords, ItemCount, ChooseNumber) {
    
    var currentLineText = node.getText();
    if (currentLineText.indexOf('\u200B') >= 0) {
        currentLineText = currentLineText.replace(/\u200B/gi, "");
    }
    if (currentLineText.indexOf('\uc2a0') >= 0) {
        currentLineText = currentLineText.replace(/\uc2a0/gi, " ");
    }

    return CommonEditorProcess.ProcessNodes(currentLineText, processedWords, ItemCount, ChooseNumber, rangeSelection);
}

// Find CK Editor instance and properties.
CKEditorProcess.FindInstance = function()
{
    var returnObject = {};
    var iframeName = "WebWizRTE";
    var iframeEditorName = "currentEditor";
    var ckEditorName = 'CKEDITOR';
    returnObject.CkEditorObject = undefined;

    if (typeof CKEDITOR != "undefined") {
        returnObject.CkEditorObject = CKEDITOR;
    }

    returnObject.CurrentEditor = undefined;
    // this is only for EK Editor implemeted inside Iframe , customer may not required but this is for demo, how we can access object from Iframe 
    var iFrameObject = document.getElementById(iframeName);
    if (iFrameObject != undefined) {

        if (returnObject.CkEditorObject == undefined) {
            returnObject.CkEditorObject = iFrameObject.contentWindow[ckEditorName];
        }
        returnObject.CurrentEditor = returnObject.CkEditorObject.instances.TemplateSubPage
        if (returnObject.CurrentEditor == undefined) {
            returnObject.CurrentEditor = iFrameObject.contentWindow[iframeEditorName];
        }
    }
    if (returnObject.CurrentEditor == undefined) {
        returnObject.CurrentEditor = returnObject.CkEditorObject.instances[Object.keys(returnObject.CkEditorObject.instances)[0]];
    }
    return returnObject;
}
//Process Command
CKEditorProcess.ProcessCommand = function (ActionRecipe) {
    
    //Defined recipe field
   var SelectFor=ActionRecipe.SelectFor;
   var NextPrevious=ActionRecipe.NextPrevious;
    var ChooseNumber = ActionRecipe.ChooseNumber == undefined ? 1 : ActionRecipe.ChooseNumber;
   var SearchText= ActionRecipe.SearchText;
   var CommandName=ActionRecipe.Name;
   
    if (CommandName.toLowerCase() == "selectword" && SelectFor.trim() == "") {
        SelectFor = "select";
    }
    if (NextPrevious == undefined || NextPrevious == '' || NextPrevious == "undefined") {
        NextPrevious = 'previous';
    }

    var result = CKEditorProcess.FindInstance();
    var CkEditorObject = result.CkEditorObject;
    var currentEditor = result.CurrentEditor;
    currentEditor.focus();

    // Check processing required
    if(CKEditorProcess.PreProcessCommand(ActionRecipe, currentEditor)) {
        return;
    }

    if(ActionRecipe.Action == AugnitoCMDs.PRESS_DELETE)
    {
        SelectFor="delete";
    }

    if (typeof SelectFor == "undefined") {
        SelectFor = CommandName;
    }

    if (SelectFor!=undefined && "select" == SelectFor.toLowerCase()) {
        CKEditorProcess.UnSelectCurrentSelection(currentEditor);
    }

    var selectedPreviousWords = [];
    var selectedNextWords = [];
    var selectNodeList = [];
    var selection = currentEditor.getSelection();
    var range = selection.getRanges()[0];
    var rangeClone = range.clone();
    var currentCursorIndex = range.startOffset;
    var isCursorStartOfLine;
    cursorInSameLine = true;

    var rangeAlreadySelected = true;
    if (range.startContainer.equals(range.endContainer) && range.startOffset == range.endOffset) {
        rangeAlreadySelected = false;
    }
    else if (CommonEditorProcess.RemoveSelection(SelectFor)) {
            rangeAlreadySelected = false;
    }

    if (!rangeAlreadySelected) {
        CKEditorProcess.UnSelectCurrentSelection(currentEditor);
    }

    SelectFor = CommonEditorProcess.SetSelectFor(CommandName, SelectFor);

    var dir = NextPrevious;
    var ItemCount = ChooseNumber > 0 ? ChooseNumber : 1;
    var processNodeList = selectedPreviousWords;

    if (!rangeAlreadySelected) {
        range.setStartAt(currentEditor.editable(), CkEditorObject.POSITION_AFTER_START);
        if ("next" == dir) {
            //TODO:
            processNodeList = selectedNextWords;
        }
        else if ("previous" == dir || "last" == dir) {
            var returnObject = CKEditorProcess.PreparePreviousNodes(selectedPreviousWords, range, CkEditorObject); 
            processNodeList = returnObject["selectedPreviousWords"];
            isCursorStartOfLine = returnObject["isCursorStartOfLine"];
        }
    }
    else {
        processNodeList.push(null);
    }
    var selectSartIndex = 0;
    var selectedEndIndex = -1;
    {
        for (var i = 0; i < processNodeList.length; i++) {

            var node = processNodeList[i];

            var rangeSelection = new CkEditorObject.dom.range(currentEditor.document);
            if (!rangeAlreadySelected) {
                rangeSelection.selectNodeContents(node);
            }
            // Shift cusrsor to end of previous line
            if(isCursorStartOfLine && i == 0) {
                rangeSelection.collapse(true);
                continue;   
            }
            var processedWords = 0;

            if (!rangeAlreadySelected && node.type == CkEditorObject.NODE_TEXT) {
                var returnObject = CKEditorProcess.ProcessNodes(node, rangeSelection, processedWords, ItemCount, ChooseNumber);
                processedWords = returnObject.processedWords;
                if (!returnObject.IsProcessed) {
                    continue;
                }
                
                if (returnObject.selectedStartIndex != -1) {
                    selectSartIndex = returnObject.selectedStartIndex;
                }
                if (returnObject.selectedEndIndex != -1) {
                    selectedEndIndex = returnObject.selectedEndIndex;
                }
            }

            if (!rangeAlreadySelected) {
                currentEditor.getSelection().selectRanges([rangeSelection]);
            }

            var parentNodeElement = '';
            var parentNodeL1Element = '';
            var parentNodeL2Element = '';

            if (node != null && node.$.parentNode != null) {
                parentNodeElement = node.$.parentNode.localName;
                if (node.$.parentNode.parentNode != null) {
                    parentNodeL1Element = node.$.parentNode.parentNode.localName;
                    if (node.$.parentNode.parentNode.parentNode != null) {
                        parentNodeL2Element = node.$.parentNode.parentNode.parentNode.localName;
                    }
                }
            }

            var isBoldElement = (parentNodeElement == "strong" || parentNodeL1Element == "strong" || parentNodeL2Element == "strong");
            var isItalicElement = (parentNodeElement == "em" || parentNodeL1Element == "em" || parentNodeL2Element == "em");
            var isUnderLineElement = (parentNodeElement == "u" || parentNodeL1Element == "u" || parentNodeL2Element == "u");

            if (node != null && "select" == SelectFor.toLowerCase() && !rangeAlreadySelected) {
                selectNodeList.push(node);
            }
            else {
                CKEditorProcess.ExecuteCommand(currentEditor, SelectFor, rangeSelection, rangeAlreadySelected, isBoldElement, isItalicElement, isUnderLineElement);
            }

            if (node != null && node.type == CkEditorObject.NODE_TEXT) {
                ItemCount = ItemCount - processedWords;
            }
            if (ItemCount <= 0 || rangeAlreadySelected) {
                break;
            }
        }

    }

    if (!isCursorStartOfLine && processNodeList[0] != null && processNodeList.length > 0) {
        var oldRange = new CkEditorObject.dom.range(currentEditor.document);
        oldRange.selectNodeContents(processNodeList[0]);
        var newRange = currentEditor.createRange();

        CKEditorProcess.SelectEditorRange(newRange, oldRange, currentEditor);
    }
    else {
        var oldRange = currentEditor.getSelection().getRanges()[0];
        var newRange = currentEditor.createRange();

        CKEditorProcess.SelectEditorRange(newRange, oldRange, currentEditor);        
    }


    if (!rangeAlreadySelected) {
        if ("select" == SelectFor.toLowerCase() && selectNodeList.length > 0) {
            CKEditorProcess.ProcessSelectCommand(selectedEndIndex, selectSartIndex, selectNodeList, currentEditor, CkEditorObject, selection);           
        }
    }
}

CKEditorProcess.PanelButtonEnable = function(buttonName) {
    var buttonClass = "cke_button__"+ buttonName;
    var buttonEnable = false
    $("." + buttonClass).filter(function() {
        for(var i = 0; i < this.classList.length; i++) {
            if(this.classList[i] == "cke_button_on") {
                buttonEnable = true;
            }
        }
    });
    return buttonEnable;
}

CKEditorProcess.PreProcessCommand = function (ActionRecipe, currentEditor) {
    var commandFound = false;
    if( "bulletliststart" == ActionRecipe.Action && !CKEditorProcess.PanelButtonEnable("bulletedlist")){
        currentEditor.commands.bulletedlist.exec();
        commandFound = true;
    } 
    else if( "numberliststart" == ActionRecipe.Action  && !CKEditorProcess.PanelButtonEnable("numberedlist")){
        currentEditor.commands.numberedlist.exec();
        commandFound = true;
    } 
    else if( "bulletliststop" == ActionRecipe.Action && CKEditorProcess.PanelButtonEnable("bulletedlist")){
        currentEditor.commands.bulletedlist.exec();
        commandFound = true;
    } 
    else if( "numberliststop" == ActionRecipe.Action  && CKEditorProcess.PanelButtonEnable("numberedlist")){
        currentEditor.commands.numberedlist.exec();
        commandFound = true;
    } 
    return commandFound;
}