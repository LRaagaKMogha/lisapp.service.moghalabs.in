let appLogic = {};
let formattingDetails = {};
let oTable = {};

$(document).ready(function () {
    Reset();

    $.validator.addMethod("uniqueFormatting", function (value, element, param) {
        const existingFormatting = oTable.data().filter(function (val,idx){return val.SpokenForm.toLowerCase() == value.toLowerCase()}).length
        return existingFormatting <= 0;
    }, "Current formatting already exists. Please enter a unique current formatting.");

    $.validator.addMethod("validFormatting", function (value, element, param) {
        return value.match(/^(?!.*  ).+/);
    }, "Please enter a valid formatting.");

    //Form validation setup
    $('#FormattingForm').validate({
        rules:{
            current: {
                required: true,
                uniqueFormatting: true,
                //validFormatting: true,
                minlength: 2,
            },
            expected:{
                required: true
            }
        },
        messages: {
            current: {
                required: "Please enter the current formatting",
                uniqueFormatting: "Current formatting already exists. Please enter a unique current formatting.",
                validFormatting: "Current formatting cannot have multiple sequential spaces.",
                minlength: "Current formatting must be at least 2 characters long."
            },
            expected: "Please enter the expected formatting"
        },
        submitHandler: function (form) {
            form.submit();
        }
    });
    
1


    // Definition for the formatting details list table
    oTable = $('#FormattingList').DataTable({
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
            {data: "SpokenForm"},

            {data: "DisplayForm"},
            {
                data: 'ID',
                className: 'string',
                render: function (data, type) {
                    if (type === 'display') {   
                        return '<div class="form-group"> <button type="button" class="btn btn-sm btn-primary btn-edit" id="' + encodeURIComponent(data) + '" title="Edit Formatting"><i class="fa fa-pencil-square-o" aria-hidden="true"></i></button>&nbsp;&nbsp;<button type="button" class="btn btn-sm btn-danger btn-delete" id="' + encodeURIComponent(data) + '" title="Delete Formatting" data-toggle="modal" data-target="#confirmModal"><i class="fa fa-trash-o" aria-hidden="true"></i></button> </div>';
                    }
                    return data;
                }
            }
        ],
        "ajax": async function(data, callback, settings){
            LoadFormatting(data).then(function(formatting){
                formattingDetails.draw = data.draw;
                formattingDetails.aaData = formatting.Items;
                formattingDetails.start = data.start;
                // Store the page id for the next and previous pages
                formattingDetails.pageIdNext = formatting.PageIdNext;
                formattingDetails.pageIdPrevious = formatting.PageIdPrevious;
                // Set the total number of displayable records to the start position + the number of records returned + 1 if there is a next page (to enabled the next page button)
                formattingDetails.iTotalDisplayRecords = data.start+formatting.Items.length +  (formatting.PageIdNext?1:0);
                callback(formattingDetails);
            },
            function(data){
                $('.warning-modal-message').html("<strong> Warning!</strong> " + data);
                $('.warning-modal-message').removeClass("alert-success");
                $('.warning-modal-message').addClass("alert-danger");
                $('#warningModal').modal('show');
            
            });
           
        },
        language: {
            zeroRecords: "No format preference added yet",
        },
    });


    /**
     *  Function to load existing vocabulary words for a given subscription, user and model.
     *  onSuccess loads the data into the table.
     */
    function LoadFormatting(data) {
        const { AccountCode, UserTag, AccessKey, PersonalizationURL } = augnitoClient.getConfig();
        requestData = {SubscriptionCode: AccountCode, AccessKey, UserTag, Limit:data.length};
        //If next page is requested, use the page id from the previous request
        if(data.start>formattingDetails.start && formattingDetails.pageIdNext){
            requestData.PageId = formattingDetails.pageIdNext;
        }
        //If previous page is requested, use the page id from the previous request
        else if(data.start<formattingDetails.start && formattingDetails.pageIdPrevious){
            requestData.PageId = formattingDetails.pageIdPrevious;
        }
        return new Promise((resolve, reject)=>{ 
            $.ajax({
                url: PersonalizationURL + "fp/get",
                type: "POST",
                dataType: "json",
                contentType: "application/json",
                data: JSON.stringify(requestData),
                success: function (data) {
                    if(data.Status == 200 && data.Data && data.Data.FormattingPreferenceItems){
                        resolve(data.Data.FormattingPreferenceItems);
                    }
                    else if(data.ErrorMessage)
                    {
                        reject(data.ErrorMessage);
                    }
                    else
                    {
                        reject("Error loading formatting");    
                    }
                },
                error: function (err) {
                    reject("Get Formatting Status Code : " + err.status);
                }
            });
        });
    }

    /**
     * Function to reset the recorder/playback controls
     */
    function Reset(){
        $('.FormattingFormMessage span').hide();
        $('#SubmissionMessage').show();
        $('#current').val('').prop('disabled',false);
        $('#expected').val('');
    }



    /**
     * Event handler to add a new word to the formatting preferences list. 
     */
    $('.btn-submit').on('click', function () {
        if(!$('#FormattingForm').valid()) return;

        const { AccountCode, UserTag, AccessKey, PersonalizationURL } = augnitoClient.getConfig();
        const current = $('#current').val();
        const expected = $('#expected').val();  
        const id = $('#formattingId').val();
        let requestData ={SubscriptionCode: AccountCode, AccessKey,UserTag,SpokenForm:current,DisplayForm:expected};
        if(id)requestData.ID = id;
        $.ajax({
            url: PersonalizationURL + "fp/modify",
            type: "POST",
            dataType: "json",
            contentType: "application/json",
            data:  JSON.stringify(requestData),
            success: function (data) {
                if(data.Status == 200 && data.Data){
                    $('#formattingId').val('');
                    
                    Reset();
                    $('#SubmittedMessage').show();
                    oTable.ajax.reload();
                }
                else if(data.Status == 400 && data.ErrorMessage)
                {
                    $('.FormattingFormMessage span').hide();
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
     * When the user focuses on the input fields, hide the status messages
     */
    $('#current').on('focus', function () {
        $('.FormattingFormMessage span').hide();
    });

    $('#expected').on('focus', function () {
        $('.FormattingFormMessage span').hide();
    });



    /**
     * Event handler to create a confirm delete dialog for a given word.
     * Uses .on as elements are dynamically created.
     */
    $(document).on('click', '.btn-delete', function (ev) {
        ev.preventDefault();
        var id = decodeURIComponent($(this).attr('id'));
        console.log(id);
        $(".btn-confirm-delete").attr("id", id);
        var filteredData = formattingDetails.aaData.filter(function (e) { return e.ID == id });
        if (filteredData.length > 0) {
            $("#txtDeleteConfirmMessage").html("Are you sure you want to delete \"" + filteredData[0].SpokenForm + "\" formatting?");
        }
        else {
            $("#txtDeleteConfirmMessage").html("Are you sure you want to delete this formatting?");
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
     * Event handler to delete a word from the formatting list.
     * Uses .on as elements are dynamically created.
     */
    $(document).on('click', '.btn-confirm-delete', function (ev) {
        ev.preventDefault();
        var word = $(this).attr('id');
        const { AccountCode, UserTag, AccessKey, PersonalizationURL, LmId } = augnitoClient.getConfig();
        $('#confirmModal').modal('hide');
        $.ajax({
            url: PersonalizationURL + "fp/delete",
            type: "POST",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify({SubscriptionCode: AccountCode, AccessKey,UserTag, ID: word}),
            success: function (data) {
                if(data.Status == 200){
                    $(".alert-danger").hide();
                    $(".alert-success").fadeIn(800);
                    $(".alert-success").html("<i class='fa fa-fw fa-check-circle'></i><strong> Success!</strong> Formatting Preferences deleted successfully. Please restart mic");
                    oTable.ajax.reload();
                    setTimeout(function () {
                        $(".alert-success").fadeOut(800);
                    }, 10000);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    });

    /**
     * Event handler to update a word in the formatting list.
     * Uses .on as elements are dynamically created.
     */
    $(document).on('click','.btn-edit', function (ev) {
        ev.preventDefault();
        const id = decodeURIComponent($(this).attr('id'));
        var filteredData = formattingDetails.aaData.filter(function (e) { return e.ID == id });
        $('#current').val(filteredData[0].SpokenForm).prop('disabled',true);
        $('#expected').val(filteredData[0].DisplayForm);
        $('#formattingId').val(id);
    });


});