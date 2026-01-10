GenericEditorProcess = {};
GenericEditorProcess.CONST = {
    NODE_ELEMENT: 1,
    NODE_TEXT: 3
}
// NOTE:
/*
 *Tiny Editor document: tinymce.editors[0].contentAreaContainer.firstElementChild.contentWindow.document 
 * CK Editor 4: CKEDITOR.instances.ckdemo.document.$
 * CK Editor 5: document
 * Tiny Editor : tinymce.activeEditor.contentAreaContainer.firstChild.contentWindow.document
 **/
//TODO :
/*
 * Set focus after insert text when line break using p like CK Editor 5, quill 
 *
 */
GenericEditorProcess.LinkBreakElement = "br";
GenericEditorProcess.DivLinkBreakElement = "div";
GenericEditorProcess.PLinkBreakElement = "p";
GenericEditorProcess.LiLinkBreakElement = "li";
GenericEditorProcess.LineBreakElements = [GenericEditorProcess.LinkBreakElement, GenericEditorProcess.DivLinkBreakElement, GenericEditorProcess.PLinkBreakElement, GenericEditorProcess.LiLinkBreakElement];

GenericEditorProcess.CustomLineBreakFunction;
GenericEditorProcess.EditorDocument;
GenericEditorProcess.CustomCommandProcesser;
GenericEditorProcess.CustomSelectProcesser;

/*
= function (){
    //CKEditor 5
    editors[0].commands.execute("enter");
}    
*/

GenericEditorProcess.FindActiveEditor = function (activeControl) {
    if (activeControl instanceof HTMLIFrameElement || activeControl.localName == "iframe") {
        var iframeActiveControl;
        $(activeControl).each(function () {
            var element = this.contentWindow.document.activeElement;
            // If there is a active element
            if (element instanceof HTMLIFrameElement || element.localName == "iframe") {
                iframeActiveControl = GenericEditorProcess.FindActiveEditor(element);
                return false; // Stop searching
            }
            else {
                iframeActiveControl = element;
                return false; // Stop searching
            }
        });
        if (iframeActiveControl) {
            return iframeActiveControl;
        }
        else {
            return activeControl;
        }
    }
    else {
        return activeControl;
    }
}

GenericEditorProcess.IsActiveGenericEditor = function () {
    var isGenericEditor = false;

    return isGenericEditor;
}

GenericEditorProcess.deleteSelection = function (editorDocument) {
    if (editorDocument == null) {
        AugnitoRange.getSelection(GenericEditorProcess.EditorDocument).deleteFromDocument();
    }
    else {
        editorDocument.execCommand("delete");
    }
}

// Callback function to handle insert text 
GenericEditorProcess.InsertTextCallback = function (editorDocument, insertText) {

    var selection = AugnitoRange.getSelection(editorDocument);
    if (!selection || selection.rangeCount == 0) {
        return;
    }

    var range = selection.getRangeAt(0);

    if (range) {

        if (insertText == "\n") {
            if (GenericEditorProcess.CustomLineBreakFunction) {
                GenericEditorProcess.CustomLineBreakFunction();
            }
            else {
                if (editorDocument != document.getRootNode()) {
                    var newNode = editorDocument.createElement(GenericEditorProcess.LinkBreakElement);
                    var extranewNode = editorDocument.createElement(GenericEditorProcess.LinkBreakElement);
                    var blankNode = editorDocument.createTextNode("");

                    if (range.endContainer.nextElementSibling == null || range.endContainer.nextElementSibling.localName != GenericEditorProcess.LinkBreakElement) {
                        range.insertNode(extranewNode);
                    }
                    range.insertNode(blankNode);
                    range.insertNode(newNode);

                    range.setStartAndEnd(blankNode, 0);
                    AugnitoRange.getSelection(editorDocument).setSingleRange(range);
                }
                else {

                    var cloneNode = range.cloneRange();

                    var enterEvent = new KeyboardEvent('keydown', {
                        bubbles: true, cancelable: true, keyCode: 13
                    });
                    editorDocument.activeElement.dispatchEvent(enterEvent);

                    range = AugnitoRange.getSelection(editorDocument).getRangeAt(0);

                    if (range.endContainer == cloneNode.endContainer &&
                        range.endOffset == cloneNode.endOffset) {

                        var newNode = editorDocument.createElement(GenericEditorProcess.DivLinkBreakElement);
                        var extranewNode = editorDocument.createElement(GenericEditorProcess.DivLinkBreakElement);
                        var blankNode = editorDocument.createTextNode("");

                        if (range.endContainer.nextElementSibling == null ||
                            range.endContainer.nextElementSibling.localName != GenericEditorProcess.DivLinkBreakElement) {
                            range.insertNode(extranewNode);
                        }
                        range.insertNode(blankNode);
                        range.insertNode(newNode);

                        range.setStartAndEnd(blankNode, 0);
                        AugnitoRange.getSelection(editorDocument).setSingleRange(range);
                    }
                }
            }
        }
        else {
            var newNode = editorDocument.createTextNode(insertText);
            range.insertNode(newNode);

            range.setStartAndEnd(newNode, newNode.nodeValue.length);
            AugnitoRange.getSelection(editorDocument).setSingleRange(range);
        }
    }
}

GenericEditorProcess.EditorWriter = function (newProcessText) {

    var IsLastFullstop = false;
    var IsLastNewLine = false;
    var cursorInSameLine = false;
    var lastChar = "";

    GenericEditorProcess.deleteSelection(null);

    var selection = AugnitoRange.getSelection(GenericEditorProcess.EditorDocument);
    if (!selection || selection.rangeCount == 0) {
        return;
    }
    var range = selection.getRangeAt(0);
    var rangeClone = range.cloneRange();
    var currentPosition = range.startOffset;
    // Reverse back to '\n', client app may not need this. replace flow of new line.
    newProcessText = newProcessText.replace(new RegExp("@newline@", "gi"), "\n");
    var startNode = range.startContainer;
    //if (currentPosition > 0) 
    {
        var resultLastChar = GenericEditorProcess.FindLastChar(startNode, range, GenericEditorProcess.EditorDocument);
        cursorInSameLine = resultLastChar.CursorInSameLine;
        lastChar = resultLastChar.LastChar;
        IsLastFullstop = resultLastChar.IsLastFullstop;
        IsLastNewLine = resultLastChar.IsLastNewLine;
    }
    selection.removeAllRanges();
    selection.addRange(rangeClone);
    newProcessText = CommonEditorProcess.ProcessText(newProcessText, IsLastFullstop, IsLastNewLine, currentPosition, cursorInSameLine);
    CommonEditorProcess.InsertTextAtCursor(GenericEditorProcess.EditorDocument, newProcessText, GenericEditorProcess.InsertTextCallback);

    try {
        var range = selection.getRangeAt(0);

        var endElement = range.endContainer;
        var parentElement = endElement.parentElement ? endElement.parentElement : endElement.parentNode;
        var documentElement = GenericEditorProcess.EditorDocument.scrollingElement ? GenericEditorProcess.EditorDocument.scrollingElement : GenericEditorProcess.EditorDocument.documentElement;

        documentElement.scrollTop = $(parentElement).offset().top;
    } catch (e) {

    }
}

GenericEditorProcess.FindLineBreakAtCursor = function (currentNode) {
    if (currentNode != null) {
        if (currentNode.previousSibling != null &&
            currentNode.previousSibling.nodeType == GenericEditorProcess.CONST.NODE_ELEMENT) {
            if (GenericEditorProcess.LineBreakElements.includes(currentNode.previousSibling.localName)) {
                return true;
            }
        }
        else if (currentNode.previousSibling != null &&
            currentNode.previousSibling.nodeType == GenericEditorProcess.CONST.NODE_TEXT &&
            currentNode.previousSibling.length > 0) {
            return false;
        }
        else if (currentNode.previousElementSibling != null &&
            currentNode.previousElementSibling.nodeType == GenericEditorProcess.CONST.NODE_ELEMENT) {
            if (GenericEditorProcess.LineBreakElements.includes(currentNode.previousElementSibling.localName)) {
                return true;
            }
        }
        else if (currentNode.previousSibling == null &&
            currentNode.previousElementSibling == null &&
            (currentNode.parentElement != null || currentNode.parentNode != null)) {

            var parentElement = currentNode.parentElement ? currentNode.parentElement : currentNode.parentNode;
            if (GenericEditorProcess.LineBreakElements.includes(parentElement.localName)) {
                return true;
            }

            if (parentElement.previousSibling != null) {
                if (parentElement.previousSibling.nodeType == GenericEditorProcess.CONST.NODE_TEXT &&
                    parentElement.previousSibling.length > 0) {
                    return false;
                }
                else if (parentElement.previousSibling.nodeType == GenericEditorProcess.CONST.NODE_ELEMENT
                    && (GenericEditorProcess.LineBreakElements.includes(parentElement.previousSibling.localName))) {
                    return true;
                }
            }

            if (parentElement.previousElementSibling != null &&
                (GenericEditorProcess.LineBreakElements.includes(parentElement.previousElementSibling.localName))) {
                return true;
            }

            else if (parentElement.previousElementSibling != null) {
                return false;
            }

            if (parentElement != null) {
                return GenericEditorProcess.FindLineBreakAtCursor(parentElement);
            }
        }

    }
    return false;
}

GenericEditorProcess.FindLineBreakAtNextCursor = function (currentNode) {
    if (currentNode != null) {
        if (currentNode.nextSibling != null &&
            currentNode.nextSibling.nodeType == GenericEditorProcess.CONST.NODE_ELEMENT) {
            if (GenericEditorProcess.LineBreakElements.includes(currentNode.nextSibling.localName)) {
                return true;
            }
        }
        else if (currentNode.nextSibling != null &&
            currentNode.nextSibling.nodeType == GenericEditorProcess.CONST.NODE_TEXT &&
            currentNode.nextSibling.length > 0) {
            return false;
        }
        else if (currentNode.nextElementSibling != null &&
            currentNode.nextElementSibling.nodeType == GenericEditorProcess.CONST.NODE_ELEMENT) {
            if (GenericEditorProcess.LineBreakElements.includes(currentNode.nextElementSibling.localName)) {
                return true;
            }
        }
        else if (currentNode.nextSibling == null &&
            currentNode.nextElementSibling == null &&
            (currentNode.parentElement != null || currentNode.parentNode != null)) {

            var parentElement = currentNode.parentElement ? currentNode.parentElement : currentNode.parentNode;
            if (GenericEditorProcess.LineBreakElements.includes(parentElement.localName)) {
                return true;
            }

            if (parentElement.nextSibling != null) {
                if (parentElement.nextSibling.nodeType == GenericEditorProcess.CONST.NODE_TEXT &&
                    parentElement.nextSibling.length > 0) {
                    return false;
                }
                else if (parentElement.nextSibling.nodeType == GenericEditorProcess.CONST.NODE_ELEMENT
                    && (GenericEditorProcess.LineBreakElements.includes(parentElement.nextSibling.localName))) {
                    return true;
                }
            }

            if (parentElement.nextElementSibling != null &&
                (GenericEditorProcess.LineBreakElements.includes(parentElement.nextElementSibling.localName))) {
                return true;
            }

            else if (parentElement.nextElementSibling != null) {
                return false;
            }

            if (parentElement != null) {
                return GenericEditorProcess.FindLineBreakAtNextCursor(parentElement);
            }
        }

    }
    return false;
}

GenericEditorProcess.FindLastChar = function (startNode, range, editorDocument) {

    var result = GenericEditorProcess.TraverseTextNodes("character", -1, [" ", "\n"], true, editorDocument);

    AugnitoRange.getSelection(editorDocument).removeAllRanges();
    AugnitoRange.getSelection(editorDocument).addRange(result.RangeBeforeProcess);

    var resultObject = {};
    resultObject.LastChar = result.LastChar;
    resultObject.CursorInSameLine = result.IsCursorInSameLine;
    resultObject.IsLastFullstop = false;
    resultObject.IsLastNewLine = false;
    if (result.LastChar != "") {
        // Check cursor has full Stop before it
        if (("." == result.LastChar) || (":" == result.LastChar)) {
            resultObject.IsLastFullstop = true;
        }
        else if (("\r\n" == result.LastChar) || ("\n" == result.LastChar) || ("\r" == result.LastChar)) {
            resultObject.IsLastNewLine = true;
        }
    }
    //console.log(resultObject);
    return resultObject;
}

GenericEditorProcess.TraverseTextNodes = function (moveby, jumpRange, breakCondition, isInvert, editorDocument, keepToLastStep) {

    var orgProcessedCursor = AugnitoRange.getSelection(editorDocument).getRangeAt(0);
    var preProcessedCursor = orgProcessedCursor.cloneRange();

    var IsCursorInSameLine = true;
    var currentClonedNewCursor;
    var lastChar = "";
    var requiredNext = -1;

    while (requiredNext != 0) {
        var isLineBreakFound = false;

        var currentCursor = orgProcessedCursor;
        var currentClonedCursor = currentCursor.cloneRange();

        if (currentCursor.endOffset == 0 && jumpRange < 0) {
            isLineBreakFound = GenericEditorProcess.FindLineBreakAtCursor(currentCursor.endContainer);
            if (isLineBreakFound) {
                isLineBreakFound = true;
                IsCursorInSameLine = false;
            }
        }
        else if (jumpRange > 0 && currentCursor.endContainer.nodeType == GenericEditorProcess.CONST.NODE_TEXT &&
            currentCursor.endOffset == currentCursor.endContainer.length) {
            isLineBreakFound = GenericEditorProcess.FindLineBreakAtNextCursor(currentCursor.endContainer);
            if (isLineBreakFound) {
                isLineBreakFound = true;
                IsCursorInSameLine = false;
            }
        }
        if (typeof breakCondition == "object") {
            if (isLineBreakFound && breakCondition.indexOf("\n") >= 0) {
                currentClonedNewCursor = currentCursor;
                break;
            }
        }

        requiredNext = currentCursor.move(moveby, jumpRange);
        //assign new position to current
        currentCursor = orgProcessedCursor;
        currentClonedNewCursor = currentClonedCursor.cloneRange();

        if (currentCursor.endOffset == currentClonedCursor.endOffset &&
            currentCursor.endContainer == currentClonedCursor.endContainer) {
            break;
        }

        var range = AugnitoRange.createRange(editorDocument);


        if (jumpRange < 0) {
            range.setEnd(currentClonedCursor.endContainer, currentClonedCursor.endOffset);
            range.setStart(currentCursor.endContainer, currentCursor.endOffset);
        }
        else if (jumpRange > 0) {
            range.setEnd(currentCursor.endContainer, currentCursor.endOffset);
            range.setStart(currentClonedCursor.endContainer, currentClonedCursor.endOffset);
        }
        else {
            break;
        }
        AugnitoRange.getSelection(editorDocument).removeAllRanges();
        AugnitoRange.getSelection(editorDocument).addRange(range);


        var selection = AugnitoRange.getSelection(editorDocument);
        if (selection.rangeCount == 0) {
            continue;
        }

        var rangeSelection = selection.getRangeAt(0);
        lastChar = rangeSelection.toString();


        if (rangeSelection.endOffset == 0 && jumpRange < 0) {
            isLineBreakFound = GenericEditorProcess.FindLineBreakAtCursor(rangeSelection.endContainer);
            if (isLineBreakFound) {
                isLineBreakFound = true;
                IsCursorInSameLine = false;
            }
        }
        else if (jumpRange > 0 && rangeSelection.endContainer.nodeType == GenericEditorProcess.CONST.NODE_TEXT &&
            rangeSelection.endOffset == rangeSelection.endContainer.length) {
            isLineBreakFound = GenericEditorProcess.FindLineBreakAtNextCursor(rangeSelection.endContainer);
            if (isLineBreakFound) {
                isLineBreakFound = true;
                IsCursorInSameLine = false;
            }
        }

        if (keepToLastStep) {
            if (jumpRange > 0) {
                currentClonedNewCursor.setStartAndEnd(currentClonedCursor.endContainer, currentClonedCursor.endOffset);
            }
            else if (jumpRange < 0) {
                currentClonedNewCursor.setStartAndEnd(currentClonedCursor.startContainer, currentClonedCursor.startOffset);
            }
        }
        else {
            if (jumpRange > 0) {
                currentClonedNewCursor.setStartAndEnd(rangeSelection.endContainer, rangeSelection.endOffset);
            }
            else if (jumpRange < 0) {
                currentClonedNewCursor.setStartAndEnd(rangeSelection.startContainer, rangeSelection.startOffset);
            }
        }
        /*
        if (jumpRange > 0) {
            currentClonedNewCursor.setStartAndEnd(rangeSelection.endContainer, rangeSelection.endOffset);
        }
        else if (jumpRange < 0) {
            currentClonedNewCursor.setStartAndEnd(rangeSelection.startContainer, rangeSelection.startOffset);
        }
        */

        if (typeof breakCondition == "number") {
            var wordArray = [];
            if (isInvert) {
                wordArray = lastChar.split(" ");
            }
            else {
                wordArray = lastChar.split(" ").filter(function (word) { return word != "" && word != ":" && word != ";" && word != "." });
            }
            breakCondition = breakCondition - wordArray.length;
            if (breakCondition <= 0) {
                break;
            }
        }
        else if (typeof breakCondition == "object") {
            var allWhiteSpaceRegex = /^[\u0085\u00A0\u1680\u180E\u2000-\u200B\u2028\u2029\u202F\u205F\u3000]+$/;

            if (!allWhiteSpaceRegex.test(lastChar) && lastChar != "") {
                if (!isInvert && breakCondition.indexOf(lastChar) >= 0) {
                    break;
                }
                else if (isInvert && breakCondition.indexOf(lastChar) < 0) {
                    break;
                }
            }
            if (isLineBreakFound && breakCondition.indexOf("\n") >= 0) {
                break;
            }
        }
        else {
            break;
        }
        AugnitoRange.getSelection(editorDocument).removeAllRanges();
        AugnitoRange.getSelection(editorDocument).addRange(currentClonedNewCursor);
        if (requiredNext == 0) {
            IsCursorInSameLine = false;
        }
    }

    AugnitoRange.getSelection(editorDocument).removeAllRanges();
    AugnitoRange.getSelection(editorDocument).addRange(preProcessedCursor);

    var returnObject = {};
    returnObject.RangeBeforeProcess = preProcessedCursor;
    returnObject.RangeAfterProcess = currentClonedNewCursor;
    returnObject.IsCursorInSameLine = IsCursorInSameLine;
    returnObject.LastChar = lastChar;

    return returnObject;
}

GenericEditorProcess.GetPrevWordInterval = function (ItemCount, editorDocument) {

    var PreCharResult = GenericEditorProcess.TraverseTextNodes("character", -1, 1, true, editorDocument);

    var result;

    if (PreCharResult.LastChar.trim() != "") {
        result = GenericEditorProcess.TraverseTextNodes("character", 1, [" ", "\n", ",", ":", ";", "."], false, editorDocument, false);
        GenericEditorProcess.SetSelectionRange(editorDocument, result.RangeAfterProcess);
    }

    result = GenericEditorProcess.TraverseTextNodes("word", -1, ItemCount, false, editorDocument, false);

    var range = AugnitoRange.createRange(editorDocument);
    range.setEnd(result.RangeBeforeProcess.endContainer, result.RangeBeforeProcess.endOffset);
    range.setStart(result.RangeAfterProcess.startContainer, result.RangeAfterProcess.startOffset);

    GenericEditorProcess.SetSelectionRange(editorDocument, range);

    return range;
}

GenericEditorProcess.GetPrevInterval = function (Item, ItemCount, editorDocument) {
    if ("word" == Item) {
        return GenericEditorProcess.GetPrevWordInterval(ItemCount, editorDocument);
    }
    else if(Item === "line"){
        return GenericEditorProcess.GetLastLines(ItemCount, editorDocument);
    }
    return null;
}

GenericEditorProcess.GetNextWordInterval = function (ItemCount, editorDocument) {
    var NextCharResult = GenericEditorProcess.TraverseTextNodes("character", 1, 1, true, editorDocument);

    var result;
    if (NextCharResult.LastChar.trim() != "") {
        result = GenericEditorProcess.TraverseTextNodes("character", -1, [" ", "\n", ",", ":", ";", "."], false, editorDocument, false);
        GenericEditorProcess.SetSelectionRange(editorDocument, result.RangeAfterProcess);
    }

    result = GenericEditorProcess.TraverseTextNodes("word", 1, ItemCount, false, editorDocument, false);

    var range = AugnitoRange.createRange(editorDocument);
    range.setEnd(result.RangeAfterProcess.endContainer, result.RangeAfterProcess.endOffset);
    range.setStart(result.RangeBeforeProcess.startContainer, result.RangeBeforeProcess.startOffset);

    GenericEditorProcess.SetSelectionRange(editorDocument, range);

    return range;
}

GenericEditorProcess.GetNextInterval = function (Item, ItemCount, editorDocument) {
    if ("word" == Item) {
        return GenericEditorProcess.GetNextWordInterval(ItemCount, editorDocument);
    }

    return null;
}

GenericEditorProcess.ProcessSelect = function (recipe, editorDocument) {

    if (!recipe.SearchText) {
        recipe.SearchText = "";
    }


    var command = recipe.SearchText.replace(/ /gi, "").toLowerCase().trim();
    var dir = recipe.NextPrevious;
    var ItemCount = recipe.ChooseNumber > 0 ? recipe.ChooseNumber : 1;
    if ("nextword" == command || "next" == dir) {
        var nextInterval = GenericEditorProcess.GetNextInterval(recipe.Name, ItemCount, editorDocument);
        if (nextInterval && recipe.SelectFor) {
            GenericEditorProcess.ProcessSelectForAction(recipe.SelectFor, editorDocument);
        }
        return true;
    }
    else if ("previousword" == command || "it" == command || "that" == command || "previous" == dir || "last" == dir) {
        var prevInterval = GenericEditorProcess.GetPrevInterval(recipe.Name, ItemCount, editorDocument);
        if (prevInterval && recipe.SelectFor) {
            GenericEditorProcess.ProcessSelectForAction(recipe.SelectFor, editorDocument);
        }
        return true;
    }
    return false;
}

GenericEditorProcess.ProcessSelectForAction = function (SelectFor, editorDocument) {

    var selection = AugnitoRange.getSelection(editorDocument);
    var range = selection.getRangeAt(0);
    var clonedRange = range.cloneRange();

    if (range.startOffset == range.endOffset && range.endContainer == range.startContainer) {
        GenericEditorProcess.GetPrevInterval("word", 1, editorDocument);
    }

    var returnValue = false;
    if ("bold" == SelectFor.toLowerCase() || AugnitoCMDs.BOLD_IT == SelectFor.toLowerCase()) {
        AugnitoRange.execSelectionCommand("Bold", editorDocument);
        returnValue = true;
    }
    else if ("italicize" == SelectFor.toLowerCase() || AugnitoCMDs.ITALICIZE_IT == SelectFor.toLowerCase()) {
        AugnitoRange.execSelectionCommand("Italic", editorDocument);
        returnValue = true;
    }
    else if ("underline" == SelectFor.toLowerCase() || AugnitoCMDs.UNDERLINE_IT == SelectFor.toLowerCase()) {
        AugnitoRange.execSelectionCommand("Underline", editorDocument);
        returnValue = true;
    }
    else if ("capitalize" == SelectFor.toLowerCase() || AugnitoCMDs.CAPITALIZED_IT == SelectFor.toLowerCase()) {
        AugnitoRange.execSelectionCommand("Uppercase", editorDocument);
        returnValue = true;
    }
    else if (AugnitoCMDs.DELETE == SelectFor.toLowerCase() || "deleteit" == SelectFor.toLowerCase() ||
        AugnitoCMDs.PRESS_DELETE == SelectFor.toLowerCase()) {
        GenericEditorProcess.deleteSelection(editorDocument);
        returnValue = true;
    }

    if (!returnValue) {
        GenericEditorProcess.SetSelectionRange(editorDocument, clonedRange);
    }
    return returnValue;
}

GenericEditorProcess.DeSelectSelection = function (editorDocument, atStart) {
    var selection = AugnitoRange.getSelection(editorDocument);
    var range = selection.getRangeAt(0);
    if (atStart) {
        range.setEnd(range.startContainer, range.startOffset);
    }
    else {
        range.setStart(range.endContainer, range.endOffset);
    }

    AugnitoRange.getSelection(editorDocument).removeAllRanges();
    AugnitoRange.getSelection(editorDocument).addRange(range);
}

GenericEditorProcess.SetSelectionRange = function (editorDocument, range) {
    AugnitoRange.getSelection(editorDocument).removeAllRanges();
    AugnitoRange.getSelection(editorDocument).addRange(range);
}

GenericEditorProcess.SearchDynamicFiled = function (searchOption) {
    var range = AugnitoRange.createRange(GenericEditorProcess.EditorDocument);
    range.selectNodeContents(GenericEditorProcess.EditorDocument.body);

    var currentRange = AugnitoRange.getSelection(GenericEditorProcess.EditorDocument).getRangeAt(0);
    searchOption.withinRange = currentRange;

    var searchTerm = /\[(.*?)\]/gi;
    
    // Iterate over matches
    while (range.findText(searchTerm, searchOption)) {
        // range now encompasses the first text match

        // Collapse the range to the position immediately after the match
        //range.collapse(false);
        range.setStart(range.startContainer, range.startOffset);
        range.setEnd(range.endContainer, range.endOffset);
        GenericEditorProcess.SetSelectionRange(GenericEditorProcess.EditorDocument, range);

        var endElement = range.endContainer;
        var parentElement = endElement.parentElement ? endElement.parentElement : endElement.parentNode;
        var documentElement = GenericEditorProcess.EditorDocument.scrollingElement ? GenericEditorProcess.EditorDocument.scrollingElement : GenericEditorProcess.EditorDocument.documentElement;

        documentElement.scrollTop = $(parentElement).offset().top;
        break;
    }

}

GenericEditorProcess.ProcessCommand = function (ActionRecipe) {

    //Keep this logs for debugging perpose only
    console.log(ActionRecipe);

    if (GenericEditorProcess.CustomCommandProcesser && ActionRecipe.Action
        && GenericEditorProcess.CustomCommandProcesser(ActionRecipe.Action)) {
        return;
    }
    else if (ActionRecipe.Action == AugnitoCMDs.SPACE_ADD) {
        GenericEditorProcess.InsertTextCallback(GenericEditorProcess.EditorDocument, " ");
    }
    else if (ActionRecipe.Action == AugnitoCMDs.SELECT_ALL) {

        var currentRange = AugnitoRange.getSelection(GenericEditorProcess.EditorDocument).getRangeAt(0);

        currentRange.setStart(GenericEditorProcess.EditorDocument.activeElement.firstElementChild.firstChild, 0);

        var lastChild = GenericEditorProcess.EditorDocument.activeElement.lastElementChild.lastChild;
        currentRange.setEnd(lastChild, lastChild.textContent.length);


        AugnitoRange.getSelection(GenericEditorProcess.EditorDocument).removeAllRanges();
        AugnitoRange.getSelection(GenericEditorProcess.EditorDocument).addRange(currentRange);

        return;
    }
    else if(!ActionRecipe.Action && ActionRecipe.Name == AugnitoCMDs.SELECT
         && GenericEditorProcess.CustomSelectProcesser
         && GenericEditorProcess.CustomSelectProcesser(ActionRecipe)){
        return;
    }
    else if ((ActionRecipe.Action && ActionRecipe.Action.indexOf(AugnitoCMDs.SELECT) == 0)
        || ActionRecipe.Name.indexOf(AugnitoCMDs.SELECT) == 0) {

        GenericEditorProcess.DeSelectSelection(GenericEditorProcess.EditorDocument);

        ActionRecipe.Name = ActionRecipe.Name.replace(AugnitoCMDs.SELECT, "");
        GenericEditorProcess.ProcessSelect(ActionRecipe, GenericEditorProcess.EditorDocument);

        if (ActionRecipe.SelectFor && ActionRecipe.SelectFor != AugnitoCMDs.SELECT) {
            GenericEditorProcess.DeSelectSelection(GenericEditorProcess.EditorDocument);
        }
        return;
    }
    else if (ActionRecipe.Action && AugnitoCMDs.NEXT_FIELD == ActionRecipe.Action.toLowerCase()) {
        var options = {
            caseSensitive: false,
            wholeWordsOnly: false,
            direction: "forward"
        };
        GenericEditorProcess.DeSelectSelection(GenericEditorProcess.EditorDocument);
        GenericEditorProcess.SearchDynamicFiled(options);
    }
    else if (ActionRecipe.Action && AugnitoCMDs.PREVIOUS_FIELD == ActionRecipe.Action.toLowerCase()) {
        var options = {
            caseSensitive: false,
            wholeWordsOnly: false,
            direction: "backward"
        };
        GenericEditorProcess.DeSelectSelection(GenericEditorProcess.EditorDocument, true);
        GenericEditorProcess.SearchDynamicFiled(options);
    }
    else if (ActionRecipe.Action && AugnitoCMDs.DESELECT_IT == ActionRecipe.Action.toLowerCase()) {
        GenericEditorProcess.DeSelectSelection(GenericEditorProcess.EditorDocument);
    }
    else if (ActionRecipe.Action && GenericEditorProcess.ProcessSelectForAction(ActionRecipe.Action, GenericEditorProcess.EditorDocument)) {
        //Handle Bold it, delete it, cut it etc.
        //GenericEditorProcess.DeSelectSelection(GenericEditorProcess.EditorDocument);
        return;
    }
    else if (ActionRecipe.Action == AugnitoCMDs.GO_TO_LINE_START) {
        var result = GenericEditorProcess.TraverseTextNodes("character", -1, ["\n"], false, GenericEditorProcess.EditorDocument, true);
        GenericEditorProcess.SetSelectionRange(GenericEditorProcess.EditorDocument, result.RangeAfterProcess);
        return;
    }
    else if (ActionRecipe.Action == AugnitoCMDs.GO_TO_LINE_END) {
        var result = GenericEditorProcess.TraverseTextNodes("character", 1, ["\n"], false, GenericEditorProcess.EditorDocument, true);

        if (result.RangeAfterProcess.endContainer.nodeValue != null &&
            result.RangeAfterProcess.endContainer.nodeValue.length != result.RangeAfterProcess.endOffset) {
            result.RangeAfterProcess.setStartAndEnd(result.RangeAfterProcess.endContainer, result.RangeAfterProcess.endContainer.nodeValue.length);
        }
        //console.log(result.RangeAfterProcess.cloneRange());
        GenericEditorProcess.SetSelectionRange(GenericEditorProcess.EditorDocument, result.RangeAfterProcess);
        return;
    }
    else if (ActionRecipe.Name == AugnitoCMDs.GOTO) {
        var range = AugnitoRange.createRange(GenericEditorProcess.EditorDocument);
        range.selectNodeContents(GenericEditorProcess.EditorDocument.body);

        if (ActionRecipe.SearchText !== "") {
            var searchTerm = ActionRecipe.SearchText + ":";
            var options = {
                caseSensitive: false,
                wholeWordsOnly: false,
            };
            // Iterate over matches
            while (range.findText(searchTerm, options)) {
                // range now encompasses the first text match

                // Collapse the range to the position immediately after the match
                range.collapse(false);


                range.setStartAndEnd(range.endContainer, range.endOffset);
                GenericEditorProcess.SetSelectionRange(GenericEditorProcess.EditorDocument, range);

                var endElement = range.endContainer;
                var parentElement = endElement.parentElement ? endElement.parentElement : endElement.parentNode;
                var documentElement = GenericEditorProcess.EditorDocument.scrollingElement ? GenericEditorProcess.EditorDocument.scrollingElement : GenericEditorProcess.EditorDocument.documentElement;

                documentElement.scrollTop = $(parentElement).offset().top;
                break;
            }
        }
    }
}

GenericEditorProcess.GetLastLines = function (ItemCount, editorDocument){
    var currentRange = AugnitoRange.getSelection(GenericEditorProcess.EditorDocument).getRangeAt(0);
    var element = $(currentRange.nativeRange.startContainer).closest("p")[0];
    
    if(!element){
        element = $(currentRange.nativeRange.startContainer).closest("li")[0];
    }

    let range = AugnitoRange.createRange(GenericEditorProcess.EditorDocument);

    if(ItemCount === 1){
        if(currentRange.startOffset === 0){
            const startElement = GenericEditorProcess.GetLastLinesRecursive(0, ItemCount, element);
            range.setStart(startElement, 0);

            const endElement = element.previousSibling ? element.previousSibling : element;
            const endIndex =  element.previousSibling ? element.previousSibling.childNodes.length : 0;
            range.setEnd(endElement, endIndex);
        }
        else{
            range.selectNodeContents(element);
        }
    }
    else{
        const countStart = currentRange.startOffset === 0 ? 0 : 1;
        const startElement = GenericEditorProcess.GetLastLinesRecursive(countStart, ItemCount, element);
        range.setStart(startElement, 0);
        if(currentRange.startOffset === 0){
            const endElement = element.previousSibling ? element.previousSibling : element;
            const endIndex =  element.previousSibling ? element.previousSibling.childNodes.length : 0;
            range.setEnd(endElement, endIndex);
        }
        else{
            range.setEnd(element, element.childNodes.length);
        }
    }    

    AugnitoRange.getSelection(editorDocument).removeAllRanges();
    AugnitoRange.getSelection(editorDocument).addRange(range);

    return range;
}

GenericEditorProcess.GetLastLinesRecursive = function (count, ItemCount, element){
    if(count === ItemCount){
        return element;
    }

    if(element.previousSibling){
        const nextElement = isListElement(element.previousSibling) ? element.previousSibling.lastChild : element.previousSibling;
        return GenericEditorProcess.GetLastLinesRecursive(count + 1, ItemCount, nextElement);
    }
    else if(isListElement(element.parentElement)){
        const parentSibling = element.parentElement.previousSibling;
        if(!parentSibling){
            return element.parentElement;
        }
        const nextElement = isListElement(parentSibling) ? parentSibling.lastChild : parentSibling;
        return GenericEditorProcess.GetLastLinesRecursive(count + 1, ItemCount, nextElement);

    }

    return element;
}

function isListElement(element){
    return element.tagName === "UL" || element.tagName === "OL";
}

