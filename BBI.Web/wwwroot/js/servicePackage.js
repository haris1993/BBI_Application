var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/admin/servicePackage/GetAllServicePackage",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "30%" },
            { "data": "package.name", "width": "30%" },
            { "data": "price", "width": "15%" },


            {
                "data": "id",
                "render": function (data) {

                    return `<div class="text-center"> 
                            <a href="/Admin/ServicePackage/Upsert/${data}" class='btn btn-success text-white' style='cursor:pointer; width:100px;' >
                                <i class='far fa-edit'></i> Uredi
                            </a>
                            &nbsp;
                            <a class='btn btn-danger text-white' style='cursor:pointer; width:100px;' onclick=Delete('/Admin/ServicePackage/Delete/'+${data})>
                               <i class='far fa-trash-alt'></i> Obriši
                            </a></div>
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

function Delete(url) {
    swal({
        title: "Da li ste sigurni da želite obrisati?",
        text: "Neće te moći vratit podatke!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Da obriši!",
        closeOnConfirm: true
    }, function () {
        $.ajax({
            type: 'DELETE',
            url: url,
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    dataTable.ajax.reload();

                }
                else {
                    toastr.error(data.message);
                }
            }
        });
    });


}