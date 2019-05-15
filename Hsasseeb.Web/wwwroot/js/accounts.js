$(document).ready(function ()  
        {  
    $("#myDatatable").DataTable({  
                "processing": true, // for show progress bar  
                "serverSide": true, // for process server side  
                "filter": true, // this is for disable filter (search box)  
                "orderMulti": false, // for disable multiple column at once  
                "order": [[0, 'asc'], [1, 'asc']],
                "ajax": {  
                    "url": "/Accounts/GetAccounts",  
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
        });  
  