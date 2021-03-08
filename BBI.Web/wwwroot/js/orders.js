﻿var dataTable;

$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("approved")) {
        loadDataTable("GetAllApprovedOrders");
    }
    else {
        if (url.includes("pending")) {
            loadDataTable("GetAllPedingOrders");
        }
        else {
            loadDataTable("GetAllOrders");
        }
    }
});


function loadDataTable(url) {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/admin/order/" + url,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "fullName", "width": "20%" },
            { "data": "phoneNumber", "width": "20%" },
            { "data": "email", "width": "20%" },
            { "data": "status", "width": "15%" },

            {
                "data": "id",
                "render": function (data) {

                    return `<div class="text-center"> 
                            <a href="/Admin/order/Details/${data}" class='btn btn-success text-white' style='cursor:pointer; width:100px;' >
                                <i class='far fa-edit'></i> Detalji
                            </a>
                            </div>
                        `;
                }, "width": "30%"
            }


        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
}
