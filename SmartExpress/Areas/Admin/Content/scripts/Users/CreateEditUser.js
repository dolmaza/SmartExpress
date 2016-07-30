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
            success: function(response) {
                if (response.IsSuccess) {
                    alert("success");
                } else {
                    alert("error");
                }
            }
        });

        return false;
    });
});