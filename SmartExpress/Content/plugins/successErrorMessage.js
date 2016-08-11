
var successErrorMessage = {
    Message: null,
    IsError: false,
    IsSuccess: false

}

successErrorMessage.Init = function (options) {
    if (options != null) {
        successErrorMessage.Message = options.Message;
        successErrorMessage.IsError = options.IsError;
        successErrorMessage.IsSuccess = options.IsSuccess;
    }

    return successErrorMessage;
}

successErrorMessage.ShowMessage = function() {
    var container = $(".success-error-message-container");
    container.removeClass("hidden");
    container.show();

    if (successErrorMessage.IsError) {
        container.removeClass("alert-success");
        container.addClass("alert-danger");
        container.find("strong").text("Error!");

    } else if (successErrorMessage.IsSuccess) {
        container.removeClass("alert-danger");
        container.addClass("alert-success");
        container.find("strong").text("Success!");

    }
    container.find("span").text(successErrorMessage.Message);
}

$(".alert .close").click(function () {
    $(this).parent().slideUp();
    return false;
})