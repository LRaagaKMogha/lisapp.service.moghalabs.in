FroalaEditorProcess = {};

FroalaEditorProcess.IsActiveElementFroalaEditor = function () {
    // Find active Froala Editor
    var activeDocumentElement = CommonEditorProcess.GetActiveEditorElement();
    var isFroalaEditor = false;
    
    if (activeDocumentElement.hasClass("fr-element") && activeDocumentElement.hasClass("fr-view")) {
        // To make sure active element is CK Editor and not other textbox
        isFroalaEditor = true;
    }
    return isFroalaEditor;
}
// Callback function to handle insert text 
FroalaEditorProcess.InsertTextCallback = function (editorInstance, insertText) {
    if (insertText != "\n") {
        editorInstance.html.insert(insertText);
    }
    else {
        editorInstance.cursor.enter();
    }
}

FroalaEditorProcess.FindInstance = function () {
    return FroalaEditor.INSTANCES[0];
}

FroalaEditorProcess.FindLastChar = function (selectedNode, currentPossition, currentEditor) {
    var lastChar = "";
    var cursorInSameLine = false;
    var currentNode = selectedNode;
    
    if (selectedNode != null && currentPossition > 0) {
        var charIndex = currentPossition;
        while (charIndex > 0) {
            lastChar = selectedNode.nodeValue.charAt(charIndex - 1);
            charIndex = charIndex - 1;
            if (lastChar.trim() != "") {
                cursorInSameLine = true;
                break;
            }
        }
    }
    if (lastChar.trim() == "") {
        while (selectedNode.previousSibling != null) {
            var charIndex = 0;
            var nodeContent = "";
            if (selectedNode.previousSibling.nodeType == 3) {
                nodeContent = selectedNode.previousSibling.nodeValue;
            }
            if (selectedNode.previousSibling.nodeType == 1) {
                nodeContent = selectedNode.previousSibling.textContent;
            }
            charIndex = nodeContent.length;
            while (charIndex > 0) {
                lastChar = nodeContent.charAt(charIndex - 1);
                charIndex = charIndex - 1;
                if (lastChar != "") {
                    break;
                }
            }
            if (lastChar != "") {
                cursorInSameLine = true;
                break;
            }
            selectedNode = selectedNode.previousSibling;
        }
    }
    if (currentNode.tagName == "P") {
        cursorInSameLine = false;
    }
    var resultObject = {};
    resultObject.LastChar = lastChar;
    resultObject.CursorInSameLine = cursorInSameLine;
    resultObject.IsLastFullstop = false;
    resultObject.IsLastNewLine = false;

    if (lastChar != "") {
        if (("." == lastChar) || (":" == lastChar)) {
            resultObject.IsLastFullstop = true;
        }
        else if (("\r\n" == lastChar) || ("\n" == lastChar) || ("\r" == lastChar)) {
            resultObject.IsLastNewLine = true;
        }
    }
    return resultObject;
}

FroalaEditorProcess.FroalaEditorWriter = function (newProcessText) {
    var froalaEdtitorInstance = FroalaEditorProcess.FindInstance();

    var currentNode = froalaEdtitorInstance.selection.get().focusNode;
    var previousNode = froalaEdtitorInstance.selection.get().focusNode.previousSibling;
    var currentPosition = froalaEdtitorInstance.selection.get().focusOffset;
    var lastChar = "";
    var cursorInSameLine = false;
    var currentLineText = "";

    newProcessText = newProcessText.replace(new RegExp("@newline@", "gi"), "\n");
    
    var resultLastChar = FroalaEditorProcess.FindLastChar(currentNode, currentPosition, froalaEdtitorInstance);
    cursorInSameLine = resultLastChar.CursorInSameLine;
    lastChar = resultLastChar.LastChar;
    
    newProcessText = CommonEditorProcess.ProcessText(newProcessText, resultLastChar.IsLastFullstop, resultLastChar.IsLastNewLine, currentPosition, cursorInSameLine);
    CommonEditorProcess.InsertTextAtCursor(froalaEdtitorInstance, newProcessText, FroalaEditorProcess.InsertTextCallback);
}

FroalaEditorProcess.DomFroalaSelection = function (startElement, startIndex, endElement, endIndex) {
    var range = document.createRange();
    var selection = window.getSelection();

    // Sets selection position to the end of the element.
    range.setStart(startElement, startIndex);
    range.setEnd(endElement, endIndex);
    // Removes other selection ranges.
    selection.removeAllRanges();
    // Adds the range to the selection.
    selection.addRange(range);
}

FroalaEditorProcess.UnSelectCurrentSelection = function (currentEditor) {
    var currentElement = froalaEdtitorInstance.selection.ranges()[0].endContainer;
    var currentElementIndex = froalaEdtitorInstance.selection.ranges()[0].endOffset;
    FroalaEditorProcess.DomFroalaSelection(currentElement, currentElementIndex, currentElement, currentElementIndex);
}

FroalaEditorProcess.isRangeAlreadySelected = function () {
    return window.getSelection().type == "Range";
}

FroalaEditorProcess.FindPreviousSibling = function(currentNode)
{
    var maxDepth = 10;
    if (currentNode.previousSibling == null) {
        var parentNode = currentNode.parentNode;
        while (parentNode != null && (parentNode.tagName.toLowerCase() == "strong" || parentNode.tagName.toLowerCase() == "em" || parentNode.tagName.toLowerCase() == "u")) {
            if (parentNode.previousSibling != null) {
                return parentNode.previousSibling;
            }
            parentNode = parentNode.parentNode;
            maxDepth = maxDepth - 1;
            if (maxDepth == 0) {
                break;
            }
        }
        return null;
    }
    else {
        return currentNode.previousSibling;
    }
}

FroalaEditorProcess.PreparePreviousNodes = function (selectedPreviousWords, currentEditor, focusNode)
{
    selectedPreviousWords.push(focusNode);
    focusNode = FroalaEditorProcess.FindPreviousSibling(focusNode);
    while (focusNode != null) {
        selectedPreviousWords.push(focusNode);
        focusNode = FroalaEditorProcess.FindPreviousSibling(focusNode);
    }
    return selectedPreviousWords;
}

FroalaEditorProcess.ProcessNodes = function(node, processedWords, ItemCount, ChooseNumber)
{
    var currentLineText = "";
    if (node.nodeType == 3) {
        currentLineText = node.nodeValue;
    }
    if (node.nodeType == 1) {
        currentLineText = node.textContent;
    }

    return CommonEditorProcess.ProcessNodes(currentLineText, processedWords, ItemCount, ChooseNumber);
}
FroalaEditorProcess.ProcessCommand = function (ActionRecipe) {
    //Line or word
    var SelectFor = ActionRecipe.SelectFor;
    var NextPrevious = ActionRecipe.NextPrevious;
    var ChooseNumber = ActionRecipe.ChooseNumber == undefined ? 1 : ActionRecipe.ChooseNumber;
    var SearchText = ActionRecipe.SearchText;
    var CommandName = ActionRecipe.Name;

    if (CommandName.toLowerCase() == "selectword" && SelectFor.trim() == "") {
        SelectFor = "select";
    }
    if (NextPrevious == undefined || NextPrevious == '' || NextPrevious == "undefined") {
        NextPrevious = 'previous';
    }

    var currentEditor = FroalaEditorProcess.FindInstance();
     
    if (ActionRecipe.Action == AugnitoCMDs.PRESS_DELETE) {
        SelectFor = "delete";
    }
    if (typeof SelectFor == "undefined") {
        SelectFor = CommandName;
    }
    
    if (SelectFor != undefined && "select" == SelectFor.toLowerCase()) {
        FroalaEditorProcess.UnSelectCurrentSelection(currentEditor);
    }
    
    var selectedPreviousWords = [];
    var selectedNextWords = [];
    var selectNodeList = [];
    var selection = currentEditor.selection;
    var range = selection.ranges()[0];
    var currentCursorIndex = range.startOffset;
    var currentNode = froalaEdtitorInstance.selection.get().focusNode;
    cursorInSameLine = true;

    var rangeAlreadySelected = FroalaEditorProcess.isRangeAlreadySelected();
    if (CommonEditorProcess.RemoveSelection(SelectFor)) {
            rangeAlreadySelected = false;
            FroalaEditorProcess.UnSelectCurrentSelection(currentEditor);
    }

    SelectFor = CommonEditorProcess.SetSelectFor(CommandName, SelectFor);

    var dir = NextPrevious;
    var ItemCount = ChooseNumber > 0 ? ChooseNumber : 1;
    var processNodeList = selectedPreviousWords;

    if (!rangeAlreadySelected) {
        if ("next" == dir) {
            //TODO:
            processNodeList = selectedNextWords;
        }
        else if ("previous" == dir || "last" == dir) {
            processNodeList = FroalaEditorProcess.PreparePreviousNodes(selectedPreviousWords, currentEditor, currentNode);
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
            var processedWords = 0;

            if (!rangeAlreadySelected) {
                var returnObject = FroalaEditorProcess.ProcessNodes(node, processedWords, ItemCount, ChooseNumber);
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
            //Keep outside for editor commands like bold, underline
            selectNodeList.push(node);
            if (node != null) {
                ItemCount = ItemCount - processedWords;
            }
            if (ItemCount <= 0 || rangeAlreadySelected) {
                break;
            }
        }

    }
    if (selectNodeList.length > 0) {
        if (!rangeAlreadySelected) {
            FroalaEditorProcess.ProcessSelectCommand(selectSartIndex, selectedEndIndex, selectNodeList);
        }
        FroalaEditorProcess.ExecuteCommand(SelectFor, currentEditor, selectNodeList[selectNodeList.length -1]);
    }
    if (SelectFor != undefined && SelectFor.toLowerCase().indexOf("select") < 0) {
        FroalaEditorProcess.UnSelectCurrentSelection(currentEditor);
    }
}

FroalaEditorProcess.ProcessSelectCommand = function (selectedStartIndex, selectedEndIndex, selectNodeList) {
    FroalaEditorProcess.DomFroalaSelection(selectNodeList[selectNodeList.length - 1], selectedStartIndex, selectNodeList[0], selectedEndIndex);
}
FroalaEditorProcess.ExecuteCommand = function (SelectFor, currentEditor, firstElementNode) {

    var parentNodeElement = '';
    var parentNodeL1Element = '';
    var parentNodeL2Element = '';
    var firstNode = currentEditor.selection.get().focusNode;
    if (firstElementNode != null) {
        firstNode = firstElementNode;
    }
    if (firstNode != null && firstNode.parentNode != null) {
        parentNodeElement = firstNode.parentNode.localName;
        if (firstNode.parentNode.parentNode != null) {
            parentNodeL1Element = firstNode.parentNode.parentNode.localName;
            if (firstNode.parentNode.parentNode.parentNode != null) {
                parentNodeL2Element = firstNode.parentNode.parentNode.parentNode.localName;
            }
        }
    }

    var isBoldElement = (parentNodeElement == "strong" || parentNodeL1Element == "strong" || parentNodeL2Element == "strong");
    var isItalicElement = (parentNodeElement == "em" || parentNodeL1Element == "em" || parentNodeL2Element == "em");
    var isUnderLineElement = (parentNodeElement == "u" || parentNodeL1Element == "u" || parentNodeL2Element == "u");

    
    if("bold" == SelectFor.toLowerCase() && !isBoldElement) {
        currentEditor.format.toggle('strong');
    }
    else if ("unbold" == SelectFor.toLowerCase() && isBoldElement) {
        currentEditor.format.toggle('strong');
    }
    else if (("italicize" == SelectFor.toLowerCase() || "italicise" == SelectFor.toLowerCase()) && !isItalicElement) {
        currentEditor.format.toggle('em');
    }
    else if (("unitalicize" == SelectFor.toLowerCase() || "unitalicise" == SelectFor.toLowerCase()) && isItalicElement) {
        currentEditor.format.toggle('em');
    }
    else if ("underline" == SelectFor.toLowerCase() && !isUnderLineElement) {
        currentEditor.format.toggle('u');
    }
    else if ("deunderline" == SelectFor.toLowerCase() && isUnderLineElement) {
        currentEditor.format.toggle('u');
    }
    else if ("capitalize" == SelectFor.toLowerCase() || "uncapitalize" == SelectFor.toLowerCase()) {

    }
    else if ("delete" == SelectFor.toLowerCase()) {
        froalaEdtitorInstance.selection.remove();   
    }
}
