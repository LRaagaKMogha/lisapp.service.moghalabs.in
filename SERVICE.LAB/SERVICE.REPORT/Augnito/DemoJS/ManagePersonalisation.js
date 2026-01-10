let appLogic = {};
let vocabDetails = {};
let audioStream = null;
let audioRecorder = null;
let audioBlob = null;
let audioRecorderTimer = null;
let audioRecorderStartTime = null;
let oTable = {};
let timeElapsed = 0;

$(document).ready(function () {

    //Create a UUID if one is not already set, this should never be called here
    function create_UUID() {
        var dt = new Date().getTime();
        var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = (dt + Math.random() * 16) % 16 | 0;
            dt = Math.floor(dt / 16);
            return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
        });
        return uuid;
    }

    
    //Get the device id from the cookie
    function getDeviceId() {
        var DeviceId = getCookie("DeviceId");
        if (DeviceId == "undefined") {
            DeviceId = create_UUID();
            setCookie("DeviceId", DeviceId, 365);
        }
        return DeviceId;
    }

    //Event to watch for new vocab from the main page
    window.addEventListener('storage', function (e) {
        if(e.key === 'VocabAdded' && e.newValue === 'true'){
            $('.VocabularyFormMessages span').hide();
            $('#RestartMessage').show();
            localStorage.removeItem('VocabAdded');
        }
    })

    //Add validator for audio length
    $.validator.addMethod("audiolength", function (value, element, param) {
        return timeElapsed >= 1 && timeElapsed <= 9;
    }, "Recorded audio must be between 1 and 8 seconds long");


    //Form validation setup
    $('#VocabularyForm').validate({
        ignore:":hidden:not(#audioBlob)",
        rules:{
            writtenForm: {
                required: true
            },
            isAbbreviation:{
                required: false
            },
            pronunciation: {
                required: false
            },
            audioBlob: {
                required: true,
                audiolength: true
            }
        },
        messages: {
            writtenForm: "Please enter the written form",
            pronunciation: "Please enter the pronunciation",
            audioBlob: {
                required: "Please record the word",
                audiolength: "Recorded audio must be between 1 and 8 seconds long"
            }
        },
        submitHandler: function (form) {
            form.submit();
        }
    })


    // Definition for the vocabulary list table
    oTable = $('#VocabularyList').DataTable({
        "ordering": false,
        "stateSave": false, // We can't resume from the data tables state and paginate properly
        "searching": false,
        "paging": true,
        "info":false,
        "pagingType": "simple",
        "serverSide": true,
        "columnDefs": [
            { "targets": [0], "searchable": true }
        ],
        columns:[
            {data: "Word"},
            {data: "SpokenWord"},
            {data: "CreatedAt"},
            {
                data: 'Status',
                className: 'string',
                render: function (data, type, row, meta) {
                    if (type === 'display' && data=='Added') {
                        return '<div class="form-group"> <button type="button" class="btn btn-sm btn-primary btn-edit" id="' + row['Word'] + '" title="Edit Vocabulary"><i class="fa fa-pencil-square-o" aria-hidden="true"></i></button>&nbsp;&nbsp;<button type="button" class="btn btn-sm btn-danger btn-delete" id="' + row['Word'] + '" title="Delete Vocabulary" data-toggle="modal" data-target="#confirmModal"><i class="fa fa-trash-o" aria-hidden="true"></i></button> </div>';
                    }
                    else if(type==='display' && data=='Failed'){
                        return '<div class="form-group"> <button type="button" class="btn btn-sm btn-primary btn-edit" id="' + row['Word'] + '" title="Retry Vocabulary"><i class="fa fa-pencil-square-o" aria-hidden="true"></i></button>&nbsp;&nbsp;<button type="button" class="btn btn-sm btn-danger btn-delete" id="' + row['Word'] + '" title="Delete Vocabulary" data-toggle="modal" data-target="#confirmModal"><i class="fa fa-trash-o" aria-hidden="true"></i></button> </div>';
                    }
                    return '';
                }
            },
            {
                data: 'Status',
                className: 'string',
                render: function (data, type, row, meta) {
                    if(type==='display' && data=='Processing'){
                        return '<img src="Images/clock.svg" title="Processing" />';
                    }
                    else if(type==='display' && data=='Failed'){
                        return '<img src="Images/failed.svg" title="Failed" />';
                    }
                    return '';
                }

            }
        ],
        "ajax": async function(data, callback, settings){
            LoadVocabulary(data).then(function(vocab){
                vocabDetails.draw = data.draw;
                vocabDetails.aaData = vocab.Items;
                vocabDetails.start = data.start;
                // Store the page id for the next and previous pages
                vocabDetails.pageIdNext = vocab.PageIdNext;
                vocabDetails.pageIdPrevious = vocab.PageIdPrevious;
                // Set the total number of displayable records to the start position + the number of records returned + 1 if there is a next page (to enabled the next page button)
                vocabDetails.iTotalDisplayRecords = data.start+vocab.Items.length +  (vocab.PageIdNext?1:0);
                callback(vocabDetails);
            },
            function(data){
                $('.warning-modal-message').html("<strong> Warning!</strong> " + data);
                $('.warning-modal-message').removeClass("alert-success");
                $('.warning-modal-message').addClass("alert-danger");
                $('#warningModal').modal('show');
            
            });
           
        },
        language: {
            zeroRecords: "No word added yet",
        },
        createdRow: function (row, data, index) {
            $(row).addClass(data.Status);
        }
    });


    /**
     *  Function to load existing vocabulary words for a given subscription, user and model.
     *  onSuccess loads the data into the table.
     */
    function LoadVocabulary(data) {
        const { AccountCode, UserTag, AccessKey, PersonalizationURL, LmId } = augnitoClient.getConfig();
        requestData = {"Request":{SubscriptionCode: AccountCode, AccessKey,UserCode: UserTag,SmId:LmId.toString(),Limit:data.length}};

        //If next page is requested, use the page id from the previous request
        if(data.start>vocabDetails.start && vocabDetails.pageIdNext){
            requestData.Request.PageId = vocabDetails.pageIdNext;
        }
        //If previous page is requested, use the page id from the previous request
        else if(data.start<vocabDetails.start && vocabDetails.pageIdPrevious){
            requestData.Request.PageId = vocabDetails.pageIdPrevious;
        }
        return new Promise((resolve, reject)=>{ 
            $.ajax({
                url: PersonalizationURL + "get_user_prs_word",
                type: "POST",
                dataType: "json",
                contentType: "application/json",
                data: JSON.stringify(requestData),
                success: function (data) {
                    if(data.Status == 200 && data.Data && data.Data.WordItems && data.Data.WordItems){
                        resolve(data.Data.WordItems);
                    }
                    else if(data.ErrorMessage)
                    {
                        reject(data.ErrorMessage);
                    }
                    else
                    {
                        reject("Error loading vocabulary words");    
                    }
                },
                error: function (err) {
                    reject("Get Vocabulary Status Code : " + err.status);
                }
            });
        });
    }

    /**
     * Function to reset the recorder/playback controls
     */
    function Reset(){
        timeElapsed = 0;
        audioBlob = null;
        audioRecorder = null;
        $('#AddWordRecord').removeClass('recorded');
        $('.VocabularyFormMessages span').hide();
        $('#SubmissionMessage').show();
            $('#writtenForm').val('').prop('disabled',false);;
            $('#pronunciation').val('');
            $('#isAbbreviation').prop('checked', false);
            $('audioBlob').val('');
        
    }


    /**
     * Event handler to start recording when the user clicks on the record button.
     * 1. When the user clicks on the record button, the browser will ask for permission to use the microphone.
     * 2. If the user allows, the stream is stored in a variable that is accessible to the whole script.
     * 3. The RecordRTC library is used to record the audio in the stream.
     * 4. The time the user has been recording is displayed.
     */
    $('#AddWordRecord').on('mousedown', function () {

        if(audioRecorder){
            $('#AddWordRecord').trigger('mouseup');
            return;
        }

        navigator.mediaDevices.getUserMedia({ audio: true })
        .then(async function(stream) {
            audioStream = stream;
            audioRecorder = RecordRTC(stream, {
                type: 'audio/wav',
                disableLogs: true,
                checkForInactiveTracks: true,
                noWorker: true,
                desiredSampRate: 16000,
                numberOfAudioChannels:1,
                recorderType: StereoAudioRecorder,
            })

            $('#AddWordRecord').addClass('recording');

            audioRecorder.startRecording();
            audioRecorderStartTime = new Date().getTime();
            $('.RecordingTime').text("00:00");
            audioRecorderTimer = setInterval(function () {
                timeElapsed = Math.floor((new Date().getTime() - audioRecorderStartTime) / 1000);
                if(timeElapsed < 9){
                    $('.RecordingTime').text("00:0" + timeElapsed);
                }
                else {
                    $('#AddWordRecord').trigger('mouseup');
                }
            },1000);
        });
    });


    /**
     * Event handler to stop recording when the user releases the record button.
     */
    $('#AddWordRecord').on('mouseup', function () {
        clearInterval(audioRecorderTimer);
        if(audioStream) audioStream.stop();
        if(audioRecorder){
            audioRecorder.stopRecording(function () {
                audioBlob = audioRecorder.getBlob();
                $('#AddWordRecord').removeClass('recording').addClass('recorded');
                const url = URL.createObjectURL(audioBlob);
                const audio = $('#AddWordAudio')[0];
                audio.onloadedmetadata  = function(e) {
                    // Update the time elapsed to the duration of the audio file
                    timeElapsed = audio.duration;
                    if( timeElapsed < 1 || timeElapsed >= 9 ){
                        $('#AddWord_Delete').trigger('click');
                    }
                };
                audio.src = url;
                $('#audioBlob').val(url);
            });
        }
    });


    $('#AddWord_Delete').on('click', function () {
        $('#AddWordRecord').removeClass('recorded');

         $('#audioBlob').val(null);

        timeElapsed = 0;
        audioBlob = null;
        audioRecorder = null;
    });

    /**
     * Event handler to add a new word to the vocabulary list. 
     */
    $('.btn-submit').on('click', function () {
        if(!$('#VocabularyForm').valid()) return;
        $('.VocabularyFormMessages span').hide();
        const { AccountCode, UserTag, AccessKey, PersonalizationURL, LmId, SourceApp } = augnitoClient.getConfig();
        let word = $('#writtenForm').val();
        let spokenWord = $('#pronunciation').val();
        let formData = new FormData();
        let isAbbreviation = $('#isAbbreviation').is(':checked');
        formData.append('file', audioBlob, 'audio.wav');
        formData.append('Word',word);
        formData.append('IsAbbreviation',isAbbreviation);
        formData.append('SubscriptionCode',AccountCode);
        formData.append('AccessKey',AccessKey);
        formData.append('UserCode',UserTag);
        formData.append('SmId',LmId.toString());
        formData.append('DeviceCode', getDeviceId());
        formData.append('SpokenWord',spokenWord);
        formData.append('SourceApp',SourceApp);
        $.ajax({
            url: PersonalizationURL + "add_personal_word",
            type: "POST",
            dataType: "json",
            processData: false,
            contentType: false,
            data: formData,
            success: function (data) {
                if(data.Status == 200 && data.Data){
                    Reset();
                    $('.VocabularyFormMessages span').hide();
                    $('#SubmittedMessage').text('Word "' + word + '" has been submitted').show();
                    oTable.ajax.reload();
                }
                else if(data.Status == 400 && data.ErrorMessage)
                {
                    $('.VocabularyFormMessages span').hide();
                    $('#ErrorMessage').text(data.ErrorMessage).show();
                }
            },
            error: function (err) {
                if(err.readyState == 0){
                    $('#ErrorMessage').text('Not able to connect please check your internet connection.').show();
                }
                else{
                    $('#ErrorMessage').text(err.statusText).show();
                }
            }
        });
    });

    /**
     * Event handler to reset the form when the user clicks on the reset button.
     */
    $('.btn-reset').on('click', function () {
        Reset();
    });

    /**
     * Event handler to create a confirm delete dialog for a given word.
     * Uses .on as elements are dynamically created.
     */
    $(document).on('click', '.btn-delete', function (ev) {
        ev.preventDefault();
        var id = $(this).attr('id');
        $(".btn-confirm-delete").attr("id", id);

        var filteredData = vocabDetails.aaData.filter(function (e) { return e.Word == id });
        if (filteredData.length > 0) {
            $("#txtDeleteConfirmMessage").html("Are you sure you want to delete word \"" + filteredData[0].Word + "\" from vocabulary?");
        }
    });

    /**
     * Event handler to close the confirm delete dialog and remove the id attribute from the confirm button.
     * Uses .on as elements are dynamically created.
     */
    $(document).on('click', '.btn-confirm-close', function (ev) {
        ev.preventDefault();
        $(".btn-confirm-delete").attr("id", "");
    });

    /**
     * Event handler to delete a word from the vocabulary list.
     * Uses .on as elements are dynamically created.
     */
    $(document).on('click', '.btn-confirm-delete', function (ev) {
        ev.preventDefault();
        var word = $(this).attr('id');
        const { AccountCode, UserTag, AccessKey, PersonalizationURL, LmId } = augnitoClient.getConfig();
        $('#confirmModal').modal('hide');
        $.ajax({
            url: PersonalizationURL + "remove_personal_word",
            type: "POST",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify({"Request":{SubscriptionCode: AccountCode, AccessKey,UserCode: UserTag, Word: word, SmId:LmId.toString()}}),
            success: function (data) {
                if(data.Status == 200){
                    $(".alert-danger").hide();
                    $(".alert-success").fadeIn(800);
                    $(".alert-success").html("<i class='fa fa-fw fa-check-circle'></i><strong> Success!</strong> Word "+word+" deleted successfully.");
                    oTable.ajax.reload();
                    setTimeout(function () {
                        $(".alert-success").fadeOut(800);
                    }, 10000);
                }
            },
            error: function (err) {
                $('#ErrorMessage').text(err).show();
            }
        });
    });

    /**
     * Event handler to update a word in the vocabulary list.
     * Uses .on as elements are dynamically created.
     */
    $(document).on('click','.btn-edit', function (ev) {
        ev.preventDefault();
        const word = $(this).attr('id');
        const filteredData = vocabDetails.aaData.filter(function (e) { return e.Word == word });
        $('#writtenForm').val(filteredData[0].Word).prop('disabled',true);
        $('#pronunciation').val(filteredData[0].SpokenWord);
        $('#isAbbreviation').prop('checked', filteredData[0].IsAbbreviation === "true");
    });

    $(document).on('click','#restartMic', function(ev){
        ev.preventDefault();
        localStorage.setItem('WebSocketConnectionStatus', 'RequestRestart');
        oTable.ajax.reload();
    })    

    //Ensure ui is in it's initial state when the page is loaded
    Reset();

});