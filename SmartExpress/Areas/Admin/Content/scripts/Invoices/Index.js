$(function() {

    $("#From, #To").datepicker({
        dateFormat: customDateFormatJs,
        changeMonth: true,
        changeYear: true
    });

    $("#ClearDates").click(function () {
        $("#From").val("");
        $("#To").val("");
        $("#From").trigger("change");
        $(this).attr("disabled", "disabled");

    });

    initTreeGrid(InvoicesJson);

    $("#To, #From").change(function() {
        var to = $("#To").val();
        var from = $("#From").val();
        $("#ClearDates").removeAttr("disabled");
        $.ajax({
            type: "POST",
            url: InvoicesByReceiveDateUrl,
            data: {
                dateTo: to,
                dateFrom: from
            },
            dataType: "json",
            beforeSend: function () {
                AjaxLoader.ShowLoader();
            },
            success: function (response) {
                AjaxLoader.HideLoader();
                if (response.IsSuccess) {
                    initTreeGrid(response.Data.InvoicesJson);
                } else {
                    successErrorMessage.Init({
                        Message: abort,
                        IsError: true
                    }).ShowMessage();
                }
            }
        });
    });

    $("#treeGrid").on("click", ".invoice-delete", function() {
        return confirm(confirmDeleteText);
    });

});

function initTreeGrid(invoicesJson) {

    var source = {
        dataType: "json",
        dataFields: [
            { name: "ID", type: "number" },
            { name: "ParentID", type: "number" },
            { name: "InvoiceNumber", type: "string" },
            { name: "CompanyName", type: "string" },
            { name: "SenderAddress", type: "string" },
            { name: "ReceiveDate", type: "date"  },
            { name: "MessageMode", type: "string" },
            { name: "Quantity", type: "string" },
            { name: "Weigth", type: "string" },
            { name: "UnitPrice", type: "string" },
            { name: "Direction", type: "string" },
            { name: "ReceiverFirstnameLastname", type: "string" },
            { name: "ReceiverTelephoneNumber", type: "string" },
            { name: "ReceiverAddress", type: "string" }

        ],
        hierarchy:
        {
            keyDataField: { name: 'ID' },
            parentDataField: { name: 'ParentID' }
        },
        id: 'ID',
        localData: invoicesJson


    };
    var dataAdapter = new $.jqx.dataAdapter(source);

    $("#treeGrid").jqxTreeGrid(
    {
        width: "100%",
        source: dataAdapter,
        filterable: true,
        filterMode: "simple",
        sortable: true,
        pageable: false,
        pagerMode: "advanced",
        ready: function () {
            $("#treeGrid").jqxTreeGrid();
        },

        columns: [


            { text: "ზედნ. №", dataField: "InvoiceNumber", width: 100 },
            {
                text: "<i class='fa fa-cog'></i>", width: 100, cellsAlign: 'center', align: "center", columnType: 'none', editable: false, sortable: false, dataField: "ParentID", cellsRenderer: function (id, column, parentID) {
                    // render custom column.
                    return "<a href='/admin/invoices/" + id + "/edit/' class='invoice-edit'><i class='fa fa-pencil'></i></a> <a href='/admin/invoices/" + id + "/create' class='invoice-create'><i class='fa fa-plus'></i></a> <a href='/admin/invoices/" + id + "/delete' class='invoice-delete'><i class='fa fa-trash-o'></i></a>";
                }
            },
            { text: "კომპანია", dataField: "CompanyName", width: 100 },
            { text: "მისამართი", dataField: "SenderAddress", width: 100 },
            { text: "მიღ. თარიღი", dataField: "ReceiveDate", width: 100, cellsformat: customDateFormat },
            { text: "რეჟიმი", dataField: "MessageMode", width: 100 },
            { text: "რაოდენობა", dataField: "Quantity", width: 80 },
            { text: "წონა", dataField: "Weigth", width: 60 },
            { text: "ერთ. ფასი", dataField: "UnitPrice", width: 100 },
            { text: "მიმართულება", dataField: "Direction", width: 100 },
            { text: "მიმღ. სახ. გვარი", dataField: "ReceiverFirstnameLastname", width: 150 },
            { text: "მიმღ. ტელ.", dataField: "ReceiverTelephoneNumber", width: 120 },
            { text: "მიმღ. მისამართი", dataField: "ReceiverAddress", width: 120 }


        ]

    });
}
