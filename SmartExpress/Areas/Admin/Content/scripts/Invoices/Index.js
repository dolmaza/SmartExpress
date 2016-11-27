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

    
    $("#To, #From").change(function () {
        $("#ClearDates").removeAttr("disabled");
        InvociesTree.PerformCallback();
    });

});

function OnBeginCallback(s, e) {
    var to = $("#To").val();
    var from = $("#From").val();
    e.customArgs["to"] = to;
    e.customArgs["from"] = from;
}
