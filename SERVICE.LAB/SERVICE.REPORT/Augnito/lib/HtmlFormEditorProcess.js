HtmlFormEditorProcess = {}; 
HtmlFormEditorProcess.buffer = "";

HtmlFormEditorProcess.GetDelimiterArray=function(recipe)
{
    var delimiterArray;
    if(recipe.Name == AugnitoCMDs.SELECT_WORD || recipe.Name == AugnitoCMDs.SELECT_ACTIVE_WORD) {
        delimiterArray = [" ","\n"];
    } else if(recipe.Name == AugnitoCMDs.SELECT_LINE || recipe.Name == AugnitoCMDs.SELECT_ACTIVE_LINE) {
        delimiterArray = ["\n"];
    } else if(recipe.Name == AugnitoCMDs.SELECT_SENTENCE || recipe.Name == AugnitoCMDs.SELECT_ACTIVE_SENTENCE) {
        delimiterArray = ["."];
    }
    return delimiterArray;
}

HtmlFormEditorProcess.SelectWordLineSentence = function (recipe, text, caretPosition) {
    
    var dir = recipe.NextPrevious;
    if(!dir) {
        dir = "previous"; // Default direction to proceed is previous text from cursor
    }

    var count = recipe.ChooseNumber > 0 ? recipe.ChooseNumber : 1; 
    var cursorAtEnd = ( text.length == caretPosition );
    if(dir == "next") {
        caretPosition = Math.max(caretPosition, $(document.activeElement).prop('selectionEnd')) ;
    } else if(dir == "previous" || dir == "last") {
        caretPosition = Math.min(caretPosition, $(document.activeElement).prop('selectionStart') - 1);
    }
    var startIndex = caretPosition;    
    delimiterArray=HtmlFormEditorProcess.GetDelimiterArray(recipe);

    if (dir == "next") {
        while(true) {
            if(caretPosition > text.length) {
                caretPosition = text.length;
                break;
            } else if(text[caretPosition] == delimiterArray[0]) {
                caretPosition++;
            }
            else if(delimiterArray.length > 1 && text[caretPosition] == delimiterArray[1]){
                caretPosition++;
            } else {
                break;
            }
        }
        startIndex = caretPosition;
        endIndex = caretPosition;
        var delimiterFound = false;
        for( var i = caretPosition; i - 1 >= 0 && delimiterFound == false; i--) {
            for(let delimiter of delimiterArray) {
                if(text[i - 1] == delimiter) {
                    startIndex = i;
                    delimiterFound = true;
                    break;
                }
            }
        }
        if(!delimiterFound) {
            startIndex = 0;
        }

        for (var i = caretPosition; i < text.length && count > 0; i++, endIndex++) {
            for(let delimiter of delimiterArray) {
                if (text[i] == delimiter) {
                    count--;
                }
            }
        }
        if(count > 0) {
            endIndex = text.length;
        }
    } else if (dir == "previous" || dir == "last" ) {
        while(true) {
            if(caretPosition < 0) {
                caretPosition = 0;
                break;
            } else if(text[caretPosition] == delimiterArray[0]) {
                caretPosition--;
            }
            else if(delimiterArray.length > 1 && text[caretPosition] == delimiterArray[1]){
                caretPosition--;
            } else {
                break;
            }
        }
        startIndex = caretPosition;
        endIndex = caretPosition;
        var delimiterFound = false;
        for(var i = caretPosition; i  < text.length && delimiterFound == false; i++) {
            for(let delimiter of delimiterArray) {
                if(text[i] == delimiter) {
                    endIndex = i + 1;
                    delimiterFound = true;
                    break;
                }
            }
        }
        if(!delimiterFound) {
            endIndex = text.length;
        }
        for (var i = caretPosition; i >= 0 && count > 0; i--, startIndex--) {
            for(let delimiter of delimiterArray) {
                if (text[i] == delimiter) {
                    count--;
                }
            }
            if(count == 0) {
                startIndex = i + 1;
                break;
            }
        }
        if(count > 0) {
            startIndex = 0;
        } 

        if(startIndex > 0 && recipe.SelectFor == "delete" && SetSelectObjectType(recipe.SearchText) == AugnitoCMDs.SELECT_WORD && cursorAtEnd) {
            startIndex--;
        }
    }
    $(document.activeElement).prop('selectionStart', startIndex);
    $(document.activeElement).prop('selectionEnd', endIndex);
}

HtmlFormEditorProcess.DeleteSelected = function () {

    var activeDocumentElement = $(document.activeElement);
    caretPosition = activeDocumentElement.prop("selectionStart");
    document.execCommand("delete", false, null);
    HtmlFormEditorProcess.SetCaretPosition(caretPosition);    

}

HtmlFormEditorProcess.CopySelected = function() {
    HtmlFormEditorProcess.buffer = HtmlFormEditorProcess.selectedText();
    document.execCommand("copy");
}

HtmlFormEditorProcess.cut = function() {
    HtmlFormEditorProcess.buffer = HtmlFormEditorProcess.selectedText();
    document.execCommand("cut");
}

HtmlFormEditorProcess.paste =  function() {
    HtmlFormEditorProcess.copyToClipBoard(HtmlFormEditorProcess.buffer);
    document.execCommand("paste");
}

HtmlFormEditorProcess.undo = function() {
    document.execCommand("undo");
}

HtmlFormEditorProcess.redo =  function() {
    document.execCommand("redo");
}

HtmlFormEditorProcess.print = function() {
    window.print();
}

HtmlFormEditorProcess.MovetoPreviousFields=function(ApplicationControls)
{
    var activeControlId = document.activeElement.id;
    var findIndexFunction = (element) => element.ControlId == activeControlId
    var controlIndex = ApplicationControls.findIndex(findIndexFunction);

    for (var index = controlIndex - 1; index >= 0; index --) {
        var previousControl = ApplicationControls[index];
        if (previousControl != undefined && $("#" + previousControl.ControlId).length > 0) {
            $("#" + previousControl.ControlId).focus();
            break;
        }
    }
}

HtmlFormEditorProcess.MovetoNextFields=function(ApplicationControls)
{
    var activeControlId = document.activeElement.id;
    var findIndexFunction = (element) => element.ControlId == activeControlId
    var controlIndex = ApplicationControls.findIndex(findIndexFunction);
    for (var index = controlIndex + 1; index < ApplicationControls.length; index++) {
        var nextControl = ApplicationControls[index];
        if (nextControl != undefined && $("#" + nextControl.ControlId).length > 0) {
            $("#" + nextControl.ControlId).focus();
            break;
        }
    }
}

HtmlFormEditorProcess.GoStartOfLine = function(text, caretPosition) {
    for(var i = caretPosition; i >= 0 ; i-- ) {
        if( i == caretPosition && text[i] == "\n") {
            i--;
        }
        if(text[i] == "\n") {
            return i + 1;
        }
    }
    return 0;
}

HtmlFormEditorProcess.GoEndOfLine =  function(text, caretPosition) {
    if( i == caretPosition && text[i] == "\n") {
        i++;
    }
    for(var i = caretPosition; i < text.length ; i++ ) {
        if(text[i] == "\n") {
            return i;
        }
    }
    return text.length;
} 

HtmlFormEditorProcess.SetCaretPosition = function(position) {
    $(document.activeElement).prop('selectionStart', position);
    $(document.activeElement).prop('selectionEnd', position);
}


HtmlFormEditorProcess.StringWriteAtCaret =  function(currentText, hypText, caretPosition) {
    var leftText = currentText.substring(0,caretPosition);
    var rightText = currentText.substring(caretPosition);
    currentText = leftText +  hypText + rightText;
    document.activeElement.value = currentText;
    HtmlFormEditorProcess.SetCaretPosition(leftText.length + hypText.length);
}

HtmlFormEditorProcess.PasteWriteAtCaret = function(hypText) {
    navigator.clipboard.writeText(hypText).then(function() {
        document.execCommand("paste");
    }, function() {
        console.error("Unable To Write To Clipboard");
    });
}

HtmlFormEditorProcess.GetSelectedText = function() {
    var start = $(document.activeElement).prop("selectionStart");
    var end = $(document.activeElement).prop("selectionEnd");
    var currentText =  $(document.activeElement).val();
    return currentText.substring(start,end);
}

HtmlFormEditorProcess.IsTextSelected = function() {
    var start = $(document.activeElement).prop("selectionStart");
    var end = $(document.activeElement).prop("selectionEnd");
    return (start!=end);
}


HtmlFormEditorProcess.ProcessCommand = function (ActionRecipe, ApplicationControls) {

    var activeDocumentElement = $(document.activeElement);
    var caretPosition = activeDocumentElement.prop("selectionStart");
    var currentText = activeDocumentElement.val();
    var isCommandProcessed = false;

    if (ActionRecipe.Action == AugnitoCMDs.PRESS_DELETE) {    

        if(HtmlFormEditorProcess.IsTextSelected())
        {
           HtmlFormEditorProcess.DeleteSelected();
        }
        else
        {
            ActionRecipe.Name= AugnitoCMDs.SELECT_WORD;
            HtmlFormEditorProcess.SelectWordLineSentence(ActionRecipe, currentText, caretPosition)    
            HtmlFormEditorProcess.DeleteSelected();
        }
        isCommandProcessed = true;

    } else if (ActionRecipe.Name == AugnitoCMDs.SELECT_WORD) {    
        
        HtmlFormEditorProcess.SelectWordLineSentence(ActionRecipe, currentText, caretPosition)
        if(ActionRecipe.SelectFor == AugnitoCMDs.DELETE)
        {
         HtmlFormEditorProcess.DeleteSelected();
        }
        isCommandProcessed = true;
    }
    else if (ActionRecipe.Name == AugnitoCMDs.SELECT_LINE) {
      
        HtmlFormEditorProcess.SelectWordLineSentence(ActionRecipe, currentText, caretPosition)
        if(ActionRecipe.SelectFor == AugnitoCMDs.DELETE)
        {
         HtmlFormEditorProcess.DeleteSelected();
        }
        isCommandProcessed = true;
    } 
    else if (ActionRecipe.Name == AugnitoCMDs.SELECT_SENTENCE) {
      
        HtmlFormEditorProcess.SelectWordLineSentence(ActionRecipe, currentText, caretPosition)
        if(ActionRecipe.SelectFor == AugnitoCMDs.DELETE)
        {
         HtmlFormEditorProcess.DeleteSelected();
        }
        isCommandProcessed = true;
    } 
    else if (ActionRecipe.Action == AugnitoCMDs.GO_TO_LINE_START) {
        var newCaretPosition = HtmlFormEditorProcess.GoStartOfLine(currentText, caretPosition);
        HtmlFormEditorProcess.SetCaretPosition(newCaretPosition);
        isCommandProcessed = true;
    } else if (ActionRecipe.Action == AugnitoCMDs.GO_TO_DOCUMENT_START) {
        activeDocumentElement.prop("selectionStart", 0);
        activeDocumentElement.prop("selectionEnd", 0);
        isCommandProcessed = true;
    } else if (ActionRecipe.Action == AugnitoCMDs.GO_TO_DOCUMENT_END) {
        activeDocumentElement.prop("selectionStart", currentText.length);
        activeDocumentElement.prop("selectionEnd", currentText.length);
        isCommandProcessed = true;
    } else if (ActionRecipe.Action == AugnitoCMDs.GO_TO_LINE_END) {
        var newCaretPosition = HtmlFormEditorProcess.GoEndOfLine(currentText, caretPosition);
        HtmlFormEditorProcess.SetCaretPosition(newCaretPosition);
        isCommandProcessed = true;
    } else if (ActionRecipe.Action == AugnitoCMDs.COPY_IT) {
        HtmlFormEditorProcess.copy();
        isCommandProcessed = true;
    } else if (ActionRecipe.Action == AugnitoCMDs.UNDO_IT) {
        HtmlFormEditorProcess.undo();
        var endCaretPosition = activeDocumentElement.prop("selectionEnd");
        activeDocumentElement.prop("selectionStart", endCaretPosition);
        isCommandProcessed = true;
    } else if (ActionRecipe.Action == AugnitoCMDs.REDO_IT) {
        HtmlFormEditorProcess.redo();
        isCommandProcessed = true;
    } else if (ActionRecipe.Action == AugnitoCMDs.DOCUMENT_PRINT) {
        HtmlFormEditorProcess.print();
        isCommandProcessed = true;
    } else if (ActionRecipe.Action == AugnitoCMDs.SPACE_ADD) {
        HtmlFormEditorProcess.StringWriteAtCaret(currentText, " ", caretPosition);
        isCommandProcessed = true;
    }
    else  if (ActionRecipe.Action == AugnitoCMDs.DYNAMIC_NEXT_FIELD) {
        HtmlFormEditorProcess.MovetoNextFields(ApplicationControls);
    }
    else if (ActionRecipe.Action == AugnitoCMDs.DYNAMIC_PREVIOUS_FIELD) {
        HtmlFormEditorProcess.MovetoPreviousFields(ApplicationControls)
    }else if (ActionRecipe.Name == AugnitoCMDs.GOTO && ApplicationControls.length > 0) {

        // UI Navigation
        if(ActionRecipe.SearchText!=undefined)
        {
            var findName= ActionRecipe.SearchText.replace(/\s+/g, '');
            var findIndexFunction = (element) => element.ControlName == findName
            var controlIndex = ApplicationControls.findIndex(findIndexFunction);
            if(controlIndex>-1)
            {
                var targetControl = ApplicationControls[controlIndex];
                if (targetControl != undefined) {
                    $("#" + targetControl.ControlId).focus();
                }
            }
            if(findName =="nextfield" || findName=="nextfields")
            {
                HtmlFormEditorProcess.MovetoNextFields(ApplicationControls);
            }
            if(findName=="previousfield" || findName=="previousfields")
            {
                HtmlFormEditorProcess.MovetoPreviousFields(ApplicationControls);
            }
        }
        isCommandProcessed = true;
    }
    return isCommandProcessed;
}