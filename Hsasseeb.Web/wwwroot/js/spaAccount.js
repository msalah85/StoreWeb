
$(document).ready(function () {
    //Initially load pagenumber=1
    GetPageData(1);
});
function GetPageData(pageNum, pageSize) {
    $("#tblData").empty();
    $("#paged").empty();
    $.getJSON("/SPAAccounts/GetAccounts", { pageNumber: pageNum, pageSize: pageSize }, function (response) {
        var rowData = "";
        for (var i = 0; i < response.Data.length; i++) {
            rowData = rowData+ "<tr id='ID' class='row_" + response.Data[i].ID + "'>" +
            "<td>" + response.Data[i].AccountName + "</td>" +
            "<td>" + response.Data[i].AccountDesc + "</td>" +

            "<td>" + "<a href='#' class='btn btn-warning' onclick='EditAccount(" + response.Data[i].ID + ")' ><span class='glyphicon glyphicon-edit'></span> Edit </a>" + "</td>" +
            "<td>" + "<a href='#' class='btn btn-danger' onclick='DeleteAccount(" + response.Data[i].ID + ")'><span class='glyphicon glyphicon-trash'></span> Delete</a>" + "</td>" +
            "</tr>";
        }
        $("#tblData").append(rowData);
        PaggingTemplate(response.TotalPages, response.CurrentPage);
    });
}
//This is paging temlpate ,you should just copy paste
function PaggingTemplate(totalPage, currentPage) {
    var template = "";
    var TotalPages = totalPage;
    var CurrentPage = currentPage;
    var PageNumberArray = Array();


    var countIncr = 1;
    for (var i = currentPage; i <= totalPage; i++) {
        PageNumberArray[0] = currentPage;
        if (totalPage != currentPage && PageNumberArray[countIncr - 1] != totalPage) {
            PageNumberArray[countIncr] = i + 1;
        }
        countIncr++;
    };
    PageNumberArray = PageNumberArray.slice(0, 5);
    var FirstPage = 1;
    var LastPage = totalPage;
    if (totalPage != currentPage) {
        var ForwardOne = currentPage + 1;
    }
    var BackwardOne = 1;
    if (currentPage > 1) {
        BackwardOne = currentPage - 1;
    }

    template = "<p>" + CurrentPage + " of " + TotalPages + " pages</p>"
    template = template + '<ul class="pager">' +
        '<li class="previous"><a href="#" onclick="GetPageData(' + FirstPage + ')"><i class="fa fa-fast-backward"></i>&nbsp;First</a></li>' +
        '<li><select ng-model="pageSize" id="selectedId"><option value="20" selected>20</option><option value="50">50</option><option value="100">100</option><option value="150">150</option></select> </li>' +
        '<li><a href="#" onclick="GetPageData(' + BackwardOne + ')"><i class="glyphicon glyphicon-backward"></i></a>';

    var numberingLoop = "";
    for (var i = 0; i < PageNumberArray.length; i++) {
        numberingLoop = numberingLoop + '<a class="page-number active" onclick="GetPageData(' + PageNumberArray[i] + ')" href="#">' + PageNumberArray[i] + ' &nbsp;&nbsp;</a>'
    }
    template = template + numberingLoop + '<a href="#" onclick="GetPageData(' + ForwardOne + ')" ><i class="glyphicon glyphicon-forward"></i></a></li>' +
        '<li class="next"><a href="#" onclick="GetPageData(' + LastPage + ')">Last&nbsp;<i class="fa fa-fast-forward"></i></a></li></ul>';
    $("#paged").append(template);
    $('#selectedId').change(function () {
        GetPageData(1, $(this).val());
    });
}




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
var ConfirmDelete = function () {
    var ID = $("#ID").val();;
    $.ajax({
        type: "POST",
        url: "/SPAAccounts/DeleteAccount?ID=" + ID,
        success: function (result) {
            $("#DeleteConfirmation").modal("hide");
            window.location.href = "/SPAAccounts/Index";

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
