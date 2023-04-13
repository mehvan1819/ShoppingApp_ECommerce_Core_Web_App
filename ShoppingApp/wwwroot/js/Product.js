var datatbl;
$(document).ready(function () {
    datatbl = $('#tbldata').DataTable({

        "ajax": {
            "url": "/Admin/Product/GetAllProducts",
            "dataScr": ''
        },
        "columns": [
            { "data": "id" },
            { "data": "name" },
            { "data": "description" },
            { "data": "price" },
            { "data": "category.categoryName" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                    <a class="btn btn-primary" href="/Admin/Product/CreateUpdate?id=${data}"><i class="bi bi-pencil-square"></i>&nbsp; Edit</a> |
                    <a class="btn btn-primary" href="/Admin/Product/Details?id=${data}"><i class="bi bi-card-list"></i>&nbsp; Details</a> |
                    <a class="btn btn-primary" onClick="removeProduct(${data})"><i class="bi bi-trash"></i>&nbsp; Delete</a>`
                }
            }
        ]
    });
});

function removeProduct(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/Admin/Product/Delete/" + id,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        datatbl.ajax.reload();
                        toastr.success(data.message)
                    }
                    else {
                        toastr.error(data.message)
                    }
                }    
            });
        }
    })
}
