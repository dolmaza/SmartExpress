$(function () {

    // prepare the data
    var source =
    {
        dataType: "json",
        dataFields: [
            { name: 'ID', type: 'number' },
            { name: 'ParentID', type: 'number' },
            { name: 'Caption', type: 'string' },
            { name: 'StringCode', type: 'string' },
            { name: 'IntCode', type: 'number' },
            { name: 'DictionaryCode', type: 'number' },
            { name: 'IsVisible', type: 'bool' },
            { name: 'SortVal', type: 'int' }

        ],
        hierarchy:
        {
            keyDataField: { name: 'ID' },
            parentDataField: { name: 'ParentID' }
        },
        id: 'ID',
        localData: dictionaries


    };
    var dataAdapter = new $.jqx.dataAdapter(source);
    // create Tree Grid
    $("#treeGrid").jqxTreeGrid(
    {
        width: "100%",
        source: dataAdapter,
        sortable: true,
        pageable: true,
        pagerMode: 'advanced',
        ready: function () {
            $("#treeGrid").jqxTreeGrid('expandRow', '2');
        },

        columns: [
            { text: "Caption", dataField: "Caption" },
            { text: "String Code", dataField: 'StringCode', width: 150 },
            { text: "Int Code", dataField: 'IntCode', width: 65 },
            { text: "Dictionary Code", dataField: "DictionaryCode", width: 115 },
            //{ text: 'Is Visible', dataField: 'IsVisible' },
            {
                text: "Is Visible", width: 100, cellsAlign: 'center', align: "center", columnType: "none", editable: false, sortable: false, dataField: "IsVisible", cellsRenderer: function (row, column, value) {
                    if (value) {
                        return "<i class='fa fa-check-circle'></i> Visible";
                    }
                    // render custom column.
                    return "<i class='fa fa-minus-circle'></i> Not Visible";
                }
            },
            { text: "Sort Val", dataField: "SortVal", width: 100 },
            {
                text: "<i class='fa fa-cog'></i>", width: 90, cellsAlign: 'center', align: "center", columnType: 'none', editable: false, sortable: false, dataField: "ParentID", cellsRenderer: function (id, column, parentID) {
                    // render custom column.
                    return "<a href='/admin/dictionaries/" + id + "/edit/' data-row='" + id + "' class='dictionary-edit'><i class='fa fa-pencil'></i></a> <a href='#'><i class='fa fa-plus' data-parent='" + parentID + "'></i></a> <a href='/admin/dictionaries/" + id + "/delete' data-row='" + id + "' class='dictionary-delete'><i class='fa fa-trash-o'></i></a>";
                }
            }

        ]

    });

    $("#treeGrid").on("click", ".dictionary-delete", function () {
        return confirm("Do you really want to delete?");
    });


    $(".create-new").click(function() {

        var url = $(this).attr("href");

        FancyBox.Init({
            href: url,
            width: 900,
            height: 400
        }).ShowPopup();

        return false;
    });


});

