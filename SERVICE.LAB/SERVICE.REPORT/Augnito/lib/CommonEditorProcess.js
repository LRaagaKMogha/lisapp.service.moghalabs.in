CommonEditorProcess = {};

CommonEditorProcess.PinnedElement = null;
CommonEditorProcess.PinnedElementName = null;

CommonEditorProcess.GetActiveEditorElement = function () {
  if(CommonEditorProcess.PinnedElement){
    return $(CommonEditorProcess.PinnedElement);    
  }
    // Find active Editor
    var activeDocumentElement = $(document.activeElement);
    if (document.activeElement instanceof HTMLIFrameElement) {
        // If Active Element is Iframe than search CK Editor Inside it
        $(document.activeElement).each(function () {
            var element = this.contentWindow.document.activeElement;
            if (element !== this.contentWindow.document.body) {
                activeDocumentElement = $(element);
                return false; // Stop searching
            }
        });
    }
    return activeDocumentElement;    
}

CommonEditorProcess.CapitalFirstLetter = function (newProcessText) {
    // This for start of new line, and new report in empty editor.
    // If start of report, or start of new line than do first character in upper.
    var charArr = newProcessText.split('');
    if (charArr.length > 0) {
        for (var i = 0; i < charArr.length; i++) {
            var _char = charArr[i] + "";
            if (_char.trim() != "") {
                charArr[i] = charArr[i].toUpperCase();
                // break once char found after space, make it capital
                break;
            }
        }
    }
    newProcessText = charArr.join("");
    return newProcessText;
}

// Trim Speech output without new line ('\n')
CommonEditorProcess.TrimWithoutNewLine = function (newProcessText) {
    newProcessText = newProcessText.replace(/\n/gi, "@newline@");
    newProcessText = newProcessText.trim();
    newProcessText = newProcessText.replace(new RegExp("@newline@", "gi"), "\n");
    return newProcessText;
}

CommonEditorProcess.ProcessText = function (newProcessText, IsLastFullstop, IsLastNewLine, currentPosition, cursorInSameLine) {
    if (newProcessText == " ," || newProcessText == " .") {
        // By default speech output come with pre space , remove if not needed for fullstop and comma
        newProcessText = newProcessText.trim();
    }
    if (newProcessText == " \n" || newProcessText == " \n\n" || newProcessText == "\n" || newProcessText == "\n\n") {

        // handle \n for trim, trim() will remove \n also, so replace it and then do trim
        newProcessText = CommonEditorProcess.TrimWithoutNewLine(newProcessText);
    }

    // Empty editor or start position
    if (currentPosition == 0 || !cursorInSameLine) {
        newProcessText = CommonEditorProcess.CapitalFirstLetter(newProcessText);
        newProcessText = CommonEditorProcess.TrimWithoutNewLine(newProcessText);
    }
    if (IsLastFullstop || IsLastNewLine) {
        //Capital first char from speech output
        newProcessText = CommonEditorProcess.CapitalFirstLetter(newProcessText);
        if (!IsLastFullstop) {
            newProcessText = CommonEditorProcess.TrimWithoutNewLine(newProcessText);
        }
    }
    return newProcessText;
}

CommonEditorProcess.InsertTextAtCursor = function (editorInstance, newProcessText, insertCallback) {
     // Type in Editor
     var wordArr = newProcessText.split('\n');
     if (wordArr.length > 0) {
         for (var i = 0; i < wordArr.length; i++) {
             if (wordArr[i] != "") {
                 if (wordArr[i].trim() == "." || wordArr[i].trim() == ":") {
                     insertCallback(editorInstance, wordArr[i].trim());
                     if (i < wordArr.length - 1 && wordArr[i + 1].trimLeft() != "") {
                         wordArr[i + 1] = " " + wordArr[i + 1].trimLeft();
                     }
                 }
                 else {
                     insertCallback(editorInstance, wordArr[i]);
                 }
             }
             var addNewLine = wordArr.length > 1 && !((i == wordArr.length - 2 && wordArr[wordArr.length - 1] == "") || (i == wordArr.length - 1 && wordArr[i] != ""));
             if (addNewLine) {
                 insertCallback(editorInstance, "\n");
             }
         }
     }
}

CommonEditorProcess.RemoveSelection = function (SelectFor) {
    if ("bold" == SelectFor.toLowerCase() ||
        "unbold" == SelectFor.toLowerCase() ||
        "italicize" == SelectFor.toLowerCase() ||
        "unitalicize" == SelectFor.toLowerCase() ||
        "italicise" == SelectFor.toLowerCase() ||
        "unitalicise" == SelectFor.toLowerCase() ||
        "underline" == SelectFor.toLowerCase() ||
        "deunderline" == SelectFor.toLowerCase()) {
        return true;
    }
    else {
        return false;
    }
}
//Prepare command based on Command Name and Selection 
CommonEditorProcess.SetSelectFor = function (CommandName, SelectFor) {

    if ("boldit" == CommandName.toLowerCase() || "boldit" == SelectFor.toLowerCase()) {
        SelectFor = "bold";
    }
    else if ("unboldit" == CommandName.toLowerCase() || "unboldit" == SelectFor.toLowerCase()) {
        SelectFor = "unbold";
    }
    else if ("italicizeit" == CommandName.toLowerCase() || "italicizeit" == SelectFor.toLowerCase() || "italiciseit" == SelectFor.toLowerCase() || "italiciseit" == CommandName.toLowerCase()) {
        SelectFor = "italicize";
    }
    else if ("unitalicizeit" == CommandName.toLowerCase() || "unitalicizeit" == SelectFor.toLowerCase()) {
        SelectFor = "unitalicize";
    }
    else if ("underlineit" == CommandName.toLowerCase() || "underlineit" == SelectFor.toLowerCase()) {
        SelectFor = "underline";
    }
    else if ("deunderlineit" == CommandName.toLowerCase() || "deunderlineit" == SelectFor.toLowerCase()) {
        SelectFor = "deunderline";
    }
    else if ("deleteit" == CommandName.toLowerCase() || "deleteit" == SelectFor.toLowerCase()) {
        SelectFor = "delete";
    }
    else if ("copyit" == CommandName.toLowerCase() || "copyit" == SelectFor.toLowerCase()) {
        SelectFor = "copy";
    }
    else if ("cutit" == CommandName.toLowerCase() || "cutit" == SelectFor.toLowerCase()) {
        SelectFor = "cut";
    }
    return SelectFor;
}

CommonEditorProcess.ProcessNodes = function (currentLineText, processedWords, ItemCount, ChooseNumber, rangeSelection) {

    var returnObject = {};
    returnObject.processedWords = processedWords;
    returnObject.selectedStartIndex = -1;
    returnObject.selectedEndIndex = -1;
    returnObject.IsProcessed = true;

    if (currentLineText.trim() !== '') {
        var numberOfWords = currentLineText.split(' ');

        var numberOfSpace = 0;
        var selectedText = '';
        while (true) {

            var selectedWord = numberOfWords[numberOfWords.length - returnObject.processedWords - numberOfSpace - 1];

            if (selectedWord != undefined) {
                if ((selectedWord != '' && selectedWord.trim() == "") || selectedWord == '') {
                    numberOfSpace = numberOfSpace + 1;
                    selectedWord = ' ';
                }
                selectedText = selectedWord + (selectedText == '' ? '' : ' ') + selectedText;
                if (selectedWord.trim() != "") {
                    returnObject.processedWords = returnObject.processedWords + 1;
                }
                if (returnObject.processedWords >= ItemCount || returnObject.processedWords > numberOfWords.length) {
                    break;
                }
            }
            else {
                break;
            }
        }

        if (selectedText.trim() == '') {
            returnObject.IsProcessed = false;
            return returnObject;
        }

        selectedText = CommonEditorProcess.TrimWithoutNewLine(selectedText);

        var startIndex = currentLineText.lastIndexOf(selectedText);
        if (startIndex >= 0) {
            var endIndex = startIndex + selectedText.length;
            


    

            if (rangeSelection != undefined) {
                rangeSelection.startOffset = startIndex;
                rangeSelection.endOffset = endIndex;
            }
            if (ChooseNumber == ItemCount) {
                returnObject.selectedEndIndex = endIndex;
            }
            returnObject.selectedStartIndex = startIndex;
        }
        if (selectedText.trim() == "." || selectedText.trim() == ":" || selectedText.trim() == ";") {
            returnObject.processedWords = 0;
        }
    }
    else {
        returnObject.processedWords = 0;
    }
    return returnObject;
}

CommonEditorProcess.SetPinnedElement = function (pinnedElement, pinnedElementName) {
  CommonEditorProcess.PinnedElement = pinnedElement;
  CommonEditorProcess.PinnedElementName = pinnedElementName;
};

CommonEditorProcess.RemovePinnedElement = function () {
  CommonEditorProcess.PinnedElement = null;
  CommonEditorProcess.PinnedElementName = null;
};