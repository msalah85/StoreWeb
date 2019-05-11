$(document).ready(function () {
    var oTable = $('#myDatatable').DataTable({
        "processing": true, // for show progress bar  
        "serverSide": true, // for process server side  
        "filter": true, // this is for disable filter (search box)  
        "orderMulti": false, // for disable multiple column at once  
        "ajax": {
            "url": "/Accounts/GetAccounts",
            "contentType": "application/json; charset=utf-8",
            "type": "GET",
            async: true,
            "datatype": "json"
        },
        "columns": [
            { "data": "AccountName", "autoWidth": true },
            { "data": "AccountDesc", "autoWidth": true },
            { "data": "GroupOrder", "autoWidth": true },
            { "data": "AccountSerial", "autoWidth": true },
            {
                "data": "ID", "width": "50px", "render": function (data) {
                    return '<a class="popup" href="/Accounts/Save/' + data + '">Edit</a>';
                }
            },
            {
                "data": "ID", "width": "50px", "render": function (data) {
                    return '<a class="popup" href="/Accounts/SPADelete/' + data + '">Delete</a>';
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

        $dialog = $('<div class="popupWindow" style="overflow:auto"></div>')
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
})