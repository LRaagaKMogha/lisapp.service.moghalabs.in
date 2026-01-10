var MacroListdata = [];

$(document).ready(function (e) {
    $("#MacroForm").validate({
        // Specify the validation rules
        rules: {
            macroKey: {
                required: true
            },
            macroText: {
                required: true
            }
        },

        // Specify the validation error messages
        messages: {
            macroKey: "Please enter macro key",
            macroText: "Please enter macro text"
        },
        submitHandler: function (form) {
            form.submit();
        }
    });

    
    $(document).on('click', '.btn-submit', function (ev) {
        ev.preventDefault();
        var btn_button = $(this);
        if ($("#MacroForm").valid() == true) {
            btn_button.html(' <i class="fa fa fa-spinner fa-spin"></i> Processing...');
            btn_button.attr("disabled", true);

            var config = getAugnitoConfig();

            var macroId = $("#MacroForm #macroId").val();
            var macroKey = $("#MacroForm #macroKey").val();

            if (macroId.trim() != "") {
                var filteredData = MacroListdata.filter(function (e) { return e.ID == macroId });
                if (filteredData.length > 0 && filteredData[0].MacroKey == macroKey) {
                    macroKey = "";
                }
            }
            var data = {
                "AccountCode": config.AccountCode,
                "UserTag": config.UserTag,
                "AccessKey": config.AccessKey,
                "ID": macroId,
                "MacroKey": macroKey,
                "MacroValue": $("#MacroForm #macroText").val()
            }
            //Save Logic

            $.ajax({
                url: config.MacroServiceURL + 'macro/modify',
                type: 'post',
                dataType: 'json',
                contentType: 'application/json',
                success: function (data) {
                    if (data.Status == 200) {
                        $(".alert-danger").hide();
                        $(".alert-success").fadeIn(800);
                        if (macroId != "") {
                            $(".alert-success").html("<i class='fa fa-fw fa-check-circle'></i><strong> Success! </strong> Macro updated successfully.");
                        }
                        else {
                            $(".alert-success").html("<i class='fa fa-fw fa-check-circle'></i><strong> Success! </strong> Macro saved successfully.");
                        }
                        btn_button.html('Submit').attr("disabled", false);
                        ReloadData();
                        setTimeout(function () {
                            $(".alert-success").fadeOut(800);
                        }, 10000);
                    }
                    else {
                        $(".alert-success").hide();
                        $(".alert-danger").fadeIn(800);
                        $("#macroErrorMessage").html("<i class='fa fa-fw fa-times-circle'></i><strong> Warning!</strong> " + data.ErrorMessage );
                        btn_button.html('Submit').attr("disabled", false);
                    }
                },
                error: function (data) {
                    $(".alert-success").hide();
                    $(".alert-danger").fadeIn(800);
                    $("#macroErrorMessage").html("<i class='fa fa-fw fa-times-circle'></i><strong> Warning!</strong> Status Code : " + data.status);
                    btn_button.html('Submit').attr("disabled", false);
                },
                data: JSON.stringify(data)
            });

        }
    });

    $(document).on('click', '.btn-reset', function (ev) {
        ev.preventDefault();
        $("#macroId").val('');
        $("#macroKey").val('').focus();
        $("#macroText").val('');
        $("label.error").hide('');
    });

    $(document).on('click', '.btn-edit', function (ev) {
        ev.preventDefault();
        var tbl_id = $(this).attr("id");
        $('.btn-reset').trigger('click');

        //Edit button Click fill data to form
        var filteredData = MacroListdata.filter(function (e) { return e.ID == tbl_id });
        if (filteredData.length > 0) {
            $("#macroId").val(filteredData[0].ID);
            $("#macroKey").val(filteredData[0].MacroKey).focus();
            $("#macroText").val(filteredData[0].MacroValue);
        }
    });


    $(document).on('click', '.btn-confirm-delete', function (ev) {
        ev.preventDefault();
        var btn_button = $(this);
        var tbl_id = $('.btn-confirm-delete').attr("id");
        $('#confirmModal').modal('hide');

        var config = getAugnitoConfig();

        var data = {
            "AccountCode": config.AccountCode,
            "UserTag": config.UserTag,
            "AccessKey": config.AccessKey,
            "ID": tbl_id
        }
        //Call delete macro API

        $.ajax({
            url: config.MacroServiceURL + 'macro/delete',
            type: 'post',
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                if (data.Status == 200) {
                    $(".alert-danger").hide();
                    $(".alert-success").fadeIn(800);
                    $(".alert-success").html("<i class='fa fa-fw fa-check-circle'></i><strong> Success!</strong> Macro deleted successfully.");
                    ReloadData();
                    setTimeout(function () {
                        $(".alert-success").fadeOut(800);
                    }, 10000);
                }
                else {

                    $(".alert-success").hide();
                    $(".alert-danger").fadeIn(800);
                    $("#macroErrorMessage").html("<i class='fa fa-fw fa-times-circle'></i><strong> Warning!</strong> " + data.ErrorMessage);
                }
            },
            error: function (data) {
                $(".alert-success").hide();
                $(".alert-danger").fadeIn(800);
                $("#macroErrorMessage").html("<i class='fa fa-fw fa-times-circle'></i><strong> Warning!</strong> Status Code : " + data.status);
            },
            data: JSON.stringify(data)
        });
    });

    $(document).on('click', '.btn-delete', function (ev) {
        ev.preventDefault();
        var id = $(this).attr('id');
        $(".btn-confirm-delete").attr("id", id);
        // Delete button click
        var filteredData = MacroListdata.filter(function (e) { return e.ID == id });
        if (filteredData.length > 0) {
            $("#txtDeleteConfirmMessage").html("Are you sure you want to delete \"" + filteredData[0].MacroKey + "\" Macro?");
        }
        else {
            $("#txtDeleteConfirmMessage").html("Are you sure you want to delete this Macro?");
        }
    });

    $(document).on('click', '.btn-confirm-close', function (ev) {
        ev.preventDefault();
        $(".btn-confirm-delete").attr("id", "");
    });


    var oTable = $('#MacroList').DataTable({
        data: MacroListdata,
        "ordering": false,
        "stateSave": true,
        "searching": true,
        "columnDefs": [
            { "targets": [2], "searchable": false }
        ],
        columns: [
            { data: 'MacroKey' },
            {
                data: 'MacroValue',
                className: 'string',
                render: function (data, type) {
                    if (type === 'display') {
                        if (data.length > 150) {
                            return data.substr(0, 150) + " ...";
                        }
                        else {
                            return data;
                        }
                    }
                    return data;
                }
            },
            {
                data: 'ID',
                className: 'string',
                render: function (data, type) {
                    if (type === 'display') {
                        return '<div class="form-group"> <button type="button" class="btn btn-sm btn-primary btn-edit" id="' + data + '" title="Edit Macro"><i class="fa fa-pencil-square-o" aria-hidden="true"></i></button>&nbsp;&nbsp;<button type="button" class="btn btn-sm btn-danger btn-delete" id="' + data + '" title="Delete Macro" data-toggle="modal" data-target="#confirmModal"><i class="fa fa-trash-o" aria-hidden="true"></i></button> </div>';
                    }
                    return data;
                }
            }
        ]
    });

    function ReloadData() {

        var currentPage = oTable.page();
        $('.btn-reset').trigger('click');

        var config = getAugnitoConfig();

        var data = {
            "AccountCode": config.AccountCode,
            "UserTag": config.UserTag,
            "AccessKey": config.AccessKey
        }
        
        $.ajax({
            url: config.MacroServiceURL + 'macro/fetchall',
            type: 'post',
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                if (data.Status == 200 && data.Data && data.Data.MacroItems && data.Data.MacroItems.Items) {
                    MacroListdata = data.Data.MacroItems.Items;

                    oTable.clear();
                    oTable.rows.add(MacroListdata);
                    oTable.search($('input[type=search]').val()).draw();

                    var lastPage = Math.floor((MacroListdata.length - 1) / oTable.page.info().length);
                    if (currentPage >= lastPage) {
                        oTable.page(lastPage).draw(false);
                    }
                    else {
                        oTable.page(currentPage).draw(false);
                    }
                }
                else {

                    $('.warning-modal-message').html("<i class='fa fa-fw fa-times-circle'></i><strong> Warning!</strong> " + data.ErrorMessage);
                    $('.warning-modal-message').removeClass("alert-success");
                    $('.warning-modal-message').addClass("alert-danger");
                    $('#warningModal').modal('show');
                }
            },
            error: function (data) {
                $('.warning-modal-message').html("<strong> Warning!</strong> Get Macro Status Code : " + data.status);
                $('.warning-modal-message').removeClass("alert-success");
                $('.warning-modal-message').addClass("alert-danger");
                $('#warningModal').modal('show');
            },
            data: JSON.stringify(data)
        });
    }
 
    function getAugnitoConfig() {
        return augnitoClient.getConfig();
    }

    ReloadData();
});