//$(document).ready(function () {
//    var oTable = $('#myDatatable').DataTable({
//        "processing": true, // for show progress bar  
//        "serverSide": true, // for process server side  
//        "filter": true, // this is for disable filter (search box)  
//        "orderMulti": false, // for disable multiple column at once  
//        "ajax": {
//            "url": "/SPAAccounts/GetAccounts",
//            "contentType": "application/json; charset=utf-8",
//            "type": "GET",
//            async: true,
//            "datatype": "json"
//        },
//        //data: function (data) {
//        //    let additionalValues = [];
//        //    additionalValues[0] = "Additional Parameters 1";
//        //    additionalValues[1] = "Additional Parameters 2";
//        //    data.AdditionalValues = additionalValues;
//        //    return JSON.stringify(data);
//        //},
//        "columns": [
//            { "data": "ID", "autoWidth": true },
//            { "data": "AccountName", "autoWidth": true },
//            { "data": "AccountDesc", "autoWidth": true },
//            { "data": "GroupOrder", "autoWidth": true },
//            { "data": "AccountSerial", "autoWidth": true },
//            {
//                "data": "ID", "render": function (ID, type, full, meta) {
//                    return '<a href="#" onclick="EditAccount(' + ID + ')"><i class = "glyphicon glyphicon-pencil"></i> Edit</a>';
//                }
//            },
//            {
//                "data": "ID", "render": function (ID, type, full, meta) {
//                    return '<a href="#" onclick="DeleteAccount(' + ID + ')"><i class = "glyphicon glyphicon-pencil"> Delete</i></a>';
//                }
//            }
//        ]
//    });

//    $('.tablecontainer').on('click', 'a.popup', function (e) {
//        e.preventDefault();
//        OpenPopup($(this).attr('href'));
//    })
//    function OpenPopup(pageUrl) {
//        var $pageContent = $('<div/>');
//        $pageContent.load(pageUrl, function () {
//            $('#popupForm', $pageContent).removeData('validator');
//            $('#popupForm', $pageContent).removeData('unobtrusiveValidation');
//            $.validator.unobtrusive.parse('form');

//        });

//        $dialog = $('<div class="popupWindow" ></div>')
//            .html($pageContent)
//            .dialog({
//                draggable: false,
//                autoOpen: false,
//                resizable: false,
//                model: true,
//                title: 'Popup Dialog',
//                height: 550,
//                width: 600,
//                close: function () {
//                    $dialog.dialog('destroy').remove();
//                }
//            })

//        $('.popupWindow').on('submit', '#popupForm', function (e) {
//            var url = $('#popupForm')[0].action;
//            $.ajax({
//                type: "POST",
//                url: url,
//                data: $('#popupForm').serialize(),
//                success: function (data) {
//                    if (data.status) {
//                        $dialog.dialog('close');
//                        oTable.ajax.reload();
//                    }
//                }
//            })

//            e.preventDefault();
//        })


//        $dialog.dialog('open');
//    }
//})


$(document).ready(function () {
    $("#myDatatable").DataTable({
        "processing": true, // for show progress bar  
        "serverSide": true, // for process server side  
        "filter": true, // this is for disable filter (search box)  
        "orderMulti": false, // for disable multiple column at once  
        "order": [[0, 'asc'], [1, 'asc']],
        "ajax": {
            "url": "/SPAAccounts/GetAccounts",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs":
            [{
                "targets": [0],
                "visible": false,
                "searchable": false
            }],
        "columns": [
            { "data": "ID", "autoWidth": true },
            { "data": "AccountName", "autoWidth": true },
            { "data": "AccountDesc", "autoWidth": true },
            { "data": "GroupOrder", "autoWidth": true },
            { "data": "AccountSerial", "autoWidth": true },
            {
                "data": "ID", "render": function (ID, type, full, meta) {
                    return '<a href="#" onclick="EditAccount(' + ID + ')"><i class = "glyphicon glyphicon-pencil"></i> Edit</a>';
                }
            },
            {
                "data": "ID", "render": function (ID, type, full, meta) {
                    return '<a href="#" onclick="DeleteAccount(' + ID + ')"><i class = "glyphicon glyphicon-pencil"> Delete</i></a>';
                }
            }
        ]

    });


    $('.tablecontainer').on('click', 'a.popup', function (e) {
        e.preventDefault();
        OpenPopup($(this).attr('href'));
    })
    function OpenPopup(pageUrl) {
        var $pageContent = $('<div/>');
        $pageContent.load(pageUrl, function () {
            $('#popupForm', $pageContent).removeData('validator');
            $('#popupForm', $pageContent).removeData('unobtrusiveValidation');
            $.validator.unobtrusive.parse('form');

        });

        $dialog = $('<div class="popupWindow" ></div>')
            .html($pageContent)
            .dialog({
                draggable: false,
                autoOpen: false,
                resizable: false,
                model: true,
                title: 'Popup Dialog',
                height: 550,
                width: 600,
                close: function () {
                    $dialog.dialog('destroy').remove();
                }
            })

        $('.popupWindow').on('submit', '#popupForm', function (e) {
            var url = $('#popupForm')[0].action;
            $.ajax({
                type: "POST",
                url: url,
                data: $('#popupForm').serialize(),
                success: function (data) {
                    if (data.status) {
                        $dialog.dialog('close');
                        oTable.ajax.reload();
                    }
                }
            })

            e.preventDefault();
        })


        $dialog.dialog('open');
    }

});  




//$("#LoadingStatus").html("Loading....");
//$.get("/SPAAccounts/GetAccounts", null, DataBind);
//function DataBind(AccountList) {
//    var SetData = $("#SetList");
//    for (var i = 0; i < AccountList.length; i++) {
//        var Data = "<tr class='row_" + AccountList[i].ID + "'>" +
//            "<td>" + AccountList[i].AccountName + "</td>" +
//            "<td>" + AccountList[i].AccountDesc + "</td>" +
          
//            "<td>" + "<a href='#' class='btn btn-warning' onclick='EditAccount(" + AccountList[i].ID + ")' ><span class='glyphicon glyphicon-edit'></span> Edit </a>" + "</td>" +
//            "<td>" + "<a href='#' class='btn btn-danger' onclick='DeleteAccount(" + AccountList[i].ID + ")'><span class='glyphicon glyphicon-trash'></span> Delete</a>" + "</td>" +
//            "</tr>";
//        SetData.append(Data);
//        $("#LoadingStatus").html(" ");

//    }
//}



$.ajax({
    type: "Get",
    url: "/SPAAccounts/AccountNatures",
    dataType: "json",
    success: function (data) {

        var v = '<option value="-1">Please Select One</option>';
        v += '<option value=""></option>';

        for (var i = 0; i < data.length; i++) {
            v += '<option value="' + data[i].ID + '">' + data[i].AccountNatureName + '</option>';
        }
        $("#AccountNatureID").append(v);
    }
});

$.ajax({
    type: "Get",
    url: "/SPAAccounts/AllAccounts",
    dataType: "json",
    success: function (data) {

        var s = '<option value="-1">Please Select One</option>';
        s += '<option value=""></option>';
        for (var i = 0; i < data.length; i++) {
            s += '<option value="' + data[i].ID + '">' + data[i].AccountName + '</option>';
        }
        $("#ParentAccountID").append(s);
    }
});



$(".AddNew").on('click', function () {
    $('#form').each(function () { this.reset() });
    $("#ModalTitle").html("Add New Account");
    $("#MyModal").addClass('show').css('display', 'block');
});

$(".AddNewNature").on('click', function () {
    $('#form').each(function () { this.reset() });
    $("#ModalTitle").html("Add New Account Nature");
    $("#AccountNature").addClass('show').css('display', 'block');
});


$(".tree").on('click', function () {
    $('#form').each(function () { this.reset() });
    $("#ModalTitle").html("Accounts Tree");
    $("#TreeModal").addClass('show').css('display', 'block');
});

$(".close").on('click', function () {
    $("#MyModal").removeClass('show').css('display', '');
    $("#TreeModal").removeClass('show').css('display', '');
    $("#DeleteConfirmation").removeClass('show').css('display', '');
    $("#AccountNature").removeClass('show').css('display', '');
});





function EditAccount(ID) {
    $("#TreeModal").removeClass('show').css('display', '');
    var url = "/SPAAccounts/GetAccountByID?id=" + ID;
    $("#ModalTitle").html("Update Record");
    $("#MyModal").addClass('show').css('display', 'block');
    $.ajax({
        type: "Post",
        url: url,
        dataType: "json",
        success: function (data) {
            $("#ID").val(data.ID);
            $("#AccountSerial").val(data.AccountSerial);
            $("#AccountName").val(data.AccountName);
            $("#AccountDesc").val(data.AccountDesc);
            $("#GroupOrder").val(data.GroupOrder);
            document.getElementById("Active").checked = data.Active;
            //$("Active").prop("checked", data.Active);
            document.getElementById("IsMain").checked = data.IsMain;
            $("#AccountNatureID option:selected").val(data.AccountNatureID);
            $("#AccountNatureID option:selected").text(data.AccountNatureName);
            $("#ParentAccountID option:selected").val(data.ParentAccountID);
            $("#ParentAccountID option:selected").text(data.ParentAccountName);

        }
    });
}

function EditAccountNature(ID) {
    $("#TreeModal").removeClass('show').css('display', '');
    var url = "/SPAAccounts/GetAccountNatureByID?id=" + ID;
    $("#ModalTitle").html("Update Record");
    $("#AccountNature").addClass('show').css('display', 'block');
    $.ajax({
        type: "Post",
        url: url,
        dataType: "json",
        success: function (data) {
            $("#ID").val(data.ID);
            $("#AccountNatureName").val(data.AccountNatureName);
           

        }
    });
}

$("#SaveAccountNature").click(function () {
    
    $.ajax({
        type: "Post",
        url: "/SPAAccounts/SaveAccountNature",
        data: {
            ID: $("#ID").val(),
            AccountNatureName: $("#AccountNatureName").val(),
            
        },
        success: function (result) {
            alert("Success!..");
            window.location.href = "/SPAAccounts/Index";
            $("#MyModal").modal("hide");

        }
    })
});
$("#SaveAccount").click(function () {
    var active = false;
    var isMain = false;

    if ($('#Active').is(":checked")) {
        active = true;
    } else {
       active = false;
    };

    if ($('#IsMain').is(":checked")) {
       isMain = true;
    } else {
        isMain = false;
    };
    

    $.ajax({
        type: "Post",
        url: "/SPAAccounts/Save",
        data: {
            ID: $("#ID").val(),
            AccountSerial: $("#AccountSerial").val(),
            AccountName: $("#AccountName").val(),
            AccountDesc: $("#AccountDesc").val(),
            GroupOrder: $("#GroupOrder").val(),
            Active: active,
            IsMain: isMain,
            AccountNature: $("#AccountNatureID option:selected").val(),
            ParentAccount: $("#ParentAccountID option:selected").val()
        },
        success: function (result) {
            alert("Success!..");
            window.location.href = "/SPAAccounts/Index";
            $("#MyModal").modal("hide");

        }
    })
});


var DeleteAccount = function (id) {
    $("#TreeModal").removeClass('show').css('display', '');
    $("#ID").val(id);
    $("#DeleteConfirmation").addClass('show').css('display', 'block');
    console.log(id);
}
var ConfirmDeleteAccount = function () {
    var ID = $("#ID").val();;
    $.ajax({
        type: "POST",
        url: "/SPAAccounts/DeleteAccount?ID=" + ID,
        success: function (result) {
            $("#DeleteConfirmation").modal("hide");

        }
    })
};

var DeleteAccountNature = function (id) {
    $("#TreeModal").removeClass('show').css('display', '');
    $("#ID").val(id);
    $("#DeleteConfirmation").addClass('show').css('display', 'block');
    console.log(id);
}
var ConfirmDelete = function () {
    var ID = $("#ID").val();;
    $.ajax({
        type: "POST",
        url: "/SPAAccounts/DeleteAccountNature?ID=" + ID,
        success: function (result) {
            $("#DeleteConfirmation").modal("hide");
            window.location.href = "/SPAAccounts/Index";

        }
    })
}
