var id = "";
var iname = "";
var idesc = "";
var icode = "";
var amount = "";
var quantity = "";
$(function () {
    $('#formdate').on('submit', function (e) {
        e.preventDefault();
        const formData = $('#formdate').serializeArray();
        $.ajax({
            type: "POST",
            data: formData,
            dataType: 'json',
            url: url.historyOrder,
            success: function (response) {
                inQuiry(response);
            },
            failure: function (response) {
                alert(response.d);
            },
            error: function (response) {
                alert(response.d);
            }
        });
    });

    $('#update').on('click', function (e) {
        const formData = $('#UserUpdate').serialize();
        const fdata = $('#UserUpdate').serializeArray();
        console.log(fdata);
        var count = 0;
        fdata.forEach(function (item, index) {
            if (iname === item.value || idesc === item.value || icode === item.value || amount === item.value || quantity === item.value) {
                count++;
            }
        });
        if (count === fdata.length) {
            Swal.fire({
                allowOutsideClick: false,
                title: 'Reminder',
                html: 'Please Update Some Data.',
                icon: 'info'
            }).then(function () {
                $('#edituser').modal('hide');
            });
        } else {
            $.ajax({
                type: 'POST',
                cache: false,
                data: formData,
                url: url.updateHistory + "?id=" + id,
                success: function (response) {
                    if (response.isSuccess === true) {
                        Swal.fire({
                            allowOutsideClick: false,
                            title: 'Successful',
                            html: response.msg,
                            icon: 'success'
                        }).then(function () {
                            var table = $('#datatable').DataTable();
                            table.clear().draw();
                            table.destroy();
                            const formData = $('#formdate').serialize();
                            $.ajax({
                                type: "POST",
                                data: formData,
                                dataType: 'json',
                                url: url.historyOrder,
                                success: function (response) {
                                    console.log(response);
                                    inQuiry(response);
                                    $('#edituser').modal('hide');
                                },
                                failure: function (response) {
                                    alert(response.d);
                                },
                                error: function (response) {
                                    alert(response.d);
                                }
                            });
                        });
                    } else {
                        Swal.fire({
                            allowOutsideClick: false,
                            title: 'Unsuccessful',
                            html: response.msg,
                            icon: 'warning'
                        }).then(function () {

                        })
                    }
                }
            });
        }
    });
});

function inQuiry(d) {
    if ($.fn.DataTable.isDataTable("#datatable")) {
        $("#datatable").DataTable().destroy();
    }
    $("#datatable").DataTable(
    {
        data: d,
        columns: [
            {
                'data': 'photoImage', "title" : "Product Photo",
                "render": function (data) {
                    return '<img src="' + data + '" width="150px" height="150px">';
                }
            },
            { 'data': 'productName', "title": "Item Name" },
            { 'data': 'productDesc', "title": "Item Description" },
            { 'data': 'productCode', "title": "Item Code" },
            { 'data': 'amounT', "title": "Amount" },
            { 'data': 'quantity', "title": "Quantity" },
            { 'data': 'tdt', "title": "Transaction Date" },
            {
                'data': 'id', "title": "Action", "render": function (data, type, row) {
                    var n1 = row["productName"];
                    var n2 = row["productDesc"];
                    var n3 = row["productCode"];
                    var n4 = row["amounT"];
                    var n5 = row["quantity"];

                    return '<button class="btn btn-sm btn-success" onclick="funclick(\'' + data + '\', \'' + n1 + '\', \'' + n2 + '\', \'' + n3 + '\', \'' + n4 + '\', \'' + n5 + '\')" data-toggle="modal" data-target="#edituser" style="width:50px;"><span class="fas fa-user-edit"></span></button>' +
                        '<button class="btn btn-sm btn-danger" id="btnRemove" onclick="remove(\'' + data + '\', \'' + n1 + '\')" style="width:50px;"><span class="fas fa-trash"></span></button>'
                }
            }
        ]

    });
}
function funclick(ID, n1, n2, n3, n4, n5) {
    reset();
    id = ID;
    $('#productName').val(n1);
    $('#productDesc').val(n2);
    $('#productCode').val(n3);
    $('#amounT').val(n4);
    $('#quantity').val(n5);
    iname = $('#productName').val();
    idesc = $('#productDesc').val();
    icode = $('#productCode').val();
    amount = $('#amounT').val();
    quantity = $('#quantity').val();

}
function reset() {
    //$('#id').css("border-color", "gray"); $('#req0').hide();
    $('#productName').css("border-color", "gray"); $('#req1').hide();
    $('#productDesc').css("border-color", "gray"); $('#req2').hide();
    $('#productCode').css("border-color", "gray"); $('#req3').hide();
    $('#amounT').css("border-color", "gray"); $('#req4').hide();
    $('#quantity').css("border-color", "gray"); $('#req5').hide();
}

function remove(id_item, name) {
    //var table = $('#datatable').DataTable();
    Swal.fire({
        allowOutsideClick: false,
        title: 'Are you sure you want to delete ' + name + '',
        html: 'Do you want to proceed?',
        icon: 'warning',
        showDenyButton: false,
        showCancelButton: true,
        confirmButtonText: 'Yes',
        denyButtonText: 'Cancel',
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'get',
                data: JSON.stringify({ 'id': id_item }),
                url: url.removeHistory + "?id=" + id_item,
                success: function (data) {
                    if (data.hasOwnProperty('isSuccess') && data.isSuccess === true) {
                        Swal.fire({
                            allowOutsideClick: false,
                            title: 'Deleted!',
                            html: data.msg,
                            icon: 'success'
                        }).then(function () {
                            $(row).closest('tr').remove();
                        });
                    }
                }
            });
        }

    });
}
function WholeNUmber() {

}