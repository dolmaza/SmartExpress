$(function () {
    $(".physical-form").hide();
    $(".juridical-form").show();

    $("#juridical-btn").click(function() {
        $(".physical-form").hide();
        $(".juridical-form").show();
        $("#form-title").text("იურიდიული პირისთვის");
    });

    $("#physical-btn").click(function () {
        $(".juridical-form").hide();
        $(".physical-form").show();
        $("#form-title").text("ფიზიკური პირისთვის");
    });
});