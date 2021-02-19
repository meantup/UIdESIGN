//$(function () {
//    $('#datatable').DataTable({
//        ajax: {
//            type: 'Post',
//            dataType: 'JSON',
//            url: '@Url.Action("Order", "Home")'
//        },
//        columns: [

//            { "data": "iname", "title": "Item Name" },
//            { "data": "idesc", "title": "Item Description" },
//            { "data": "icode", "title": "Item Code" },
//            { "data": "amount", "title": "Amount" },
//            { "data": "quantity", "title": "Quantity" },
//            { "data": "tdt", "title": "Transaction Date" },
//            {
//                "data": "userid", "title": "Action", "render": function (data, type, row) {

//                    var n1 = row["iname"];
//                    var n2 = row["idesc"];
//                    var n3 = row["icode"];
//                    var n4 = row["amount"];
//                    var n5 = row["quantity"];
//                    var n6 = row["tdt"];
//                    var n7 = row["usertype"];

//                    return '<button  class="btn btn-sm btn-success" onclick="viewUserDetials(\'' + n7 + '\', \'' + n1 + '\', \'' + n2 + '\', \'' + n3 + '\', \'' + n4 + '\', \'' + n5 + '\', \'' + n6 + '\',\'' + n8 + '\')" data-toggle="modal" data-target="#edituser" style="width:80px;">Edit</button>' +
//                        '<button class="btn btn-sm btn-danger" onclick="deactivateuser(\'' + n1 + '\', \'' + n3 + '\', \'' + n5 + '\')"  data-toggle="modal" data-target="#confirm-delete" style="width:90px;color:white;margin-left:8px;">Deactivate</button>'
//                    //}
//                }
//            },
//        ],
       
//    })
//});
   