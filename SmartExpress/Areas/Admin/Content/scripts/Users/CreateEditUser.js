$(function() {
    $("#TelephoneNumber").mask("(599)-99-99-99");
    $("#ContractNumber").mask("99-999");

    $("#save").click(function () {
        var url = $(this).attr("href");

        var contractNumber = $("#ContractNumber").val();
        var IDNumber = $("#IDNumber").val();
        var firstname = $("#Firstname").val();
        var lastname = $("#Lastname").val();
        var address = $("#Address").val();
        var telephoneNumber = $("#TelephoneNumber").val();
        var companyName = $("#CompanyName").val();
        var roleID = $("#Role").val();

        $.ajax({
            type: "POST",
            url: url,
            data: {
                ContractNumber: contractNumber,
                IDNumber: IDNumber,
                Firstname: firstname,
                Lastname: lastname,
                Address: address,
                TelephoneNumber: telephoneNumber,
                CompanyName: companyName,
                RoleID: roleID
            },
            dataType: "json",
            beforeSend: function() {
                //$("input, select").val("");
            },
            success: function (response) {
                if (response.IsSuccess) {
                    var redirectUrl = response.Data.RedirectUrl;
                    console.log(redirectUrl);
                    if (redirectUrl == null) {
                        successErrorMessage.Init({
                            Message: response.Data.Message,
                            IsSuccess: true
                        }).ShowMessage();

                    } else {
                        window.location = redirectUrl;
                    }
                } else {
                    successErrorMessage.Init({
                        Message: response.Data.Message,
                        IsError: true
                    }).ShowMessage();
                }
            }
        });

        return false;
    });
});