$(function () {
    $(".Apply-Numeric").numericInput({
        allowFloat: true
    });
    $("#DeliveryDate").datepicker({ dateFormat: customDateFormatJs, defaultDate: receiveDate });
    $("#ReceiveDate").datepicker({ dateFormat: customDateFormatJs, defaultDate: deliveryDate });

    $("#ReceiverTelephoneNumber, #SenderTelephoneNumber").mask("(599)-99-99-99");

    $("#ContractNumber").change(function() {
        var userID = $(this).val();
        console.log(userID);
        $.ajax({
            type: "POST",
            url: getSenderInformationUrl,
            data: {
                ID: userID
            },
            dataType: "json",
            success: function(response) {
                if (response.IsSuccess) {

                    if (response.Data.Sender == null) {
                        $("#CompanyName").val("");
                        $("#SenderFirstname").val("");
                        $("#SenderLastname").val("");
                        $("#SenderTelephoneNumber").val("");
                        $("#SenderAddress").val("");
                    } else {
                        $("#CompanyName").val(response.Data.Sender.CompanyName);
                        $("#SenderFirstname").val(response.Data.Sender.Firstname);
                        $("#SenderLastname").val(response.Data.Sender.Lastname);
                        $("#SenderTelephoneNumber").val(response.Data.Sender.TelephoneNumber);
                        $("#SenderAddress").val(response.Data.Sender.Address);
                    }
                    
                } else {
                    alert(response.Data.Message);
                }
            }
        });
    });

    $("#save").click(function() {
        var url = $(this).attr("href");

        var invoiceNumber = $("#InvoiceNumber").val();
        var messageType = $("#MessageType").val();
        var receiveDate = $("#ReceiveDate").val();
        var deliveryDate = $("#DeliveryDate").val();
        var unitPrice = $("#UnitPrice").val();
        var totalPrice = $("#TotalPrice").val();
        var direction = $("#Direction").val();
        var messageMode = $("#MessageMode").val();
        var payer = $("#Payer").val();
        var formOfPayment = $("#FormOfPayment").val();
        var quantity = $("#Quantity").val();
        var weigth = $("#Weigth").val();

        var contractNumber = $("#ContractNumber").val();
        var companyName = $("#CompanyName").val();
        var senderFirstname = $("#SenderFirstname").val();
        var senderLastname = $("#SenderLastname").val();
        var senderTelephoneNumber = $("#SenderTelephoneNumber").val();
        var senderAddress = $("#SenderAddress").val();
        
        var receiverFirstname = $("#ReceiverFirstname").val();
        var receiverLastname = $("#ReceiverLastname").val();
        var recieverTelephoneNumber = $("#ReceiverTelephoneNumber").val();
        var recieverAddress = $("#ReceiverAddress").val();
        var whoReceived = $("#WhoReceived").val();
        var whoReceivedAdditional = $("#WhoReceivedAdditional").val();

        $.ajax({
            type: "POST",
            url: url,
            data: {
                ParentID: parentID == 0 ? null : parentID,
                InvoiceNumber: invoiceNumber,
                MessageTypeID: messageType,
                ReceiveDate: receiveDate,
                DeliveryDate: deliveryDate,
                UnitPrice: unitPrice,
                TotalPrice: totalPrice,
                Direction: direction,
                MessageModeID: messageMode,
                PayerID: payer,
                FormOfPaymentID: formOfPayment,
                Quantity: quantity,
                Weigth:weigth,

                UserID: contractNumber,
                CompanyName: companyName,
                SenderFirstname: senderFirstname,
                SenderLastname: senderLastname,
                SenderTelephoneNumber: senderTelephoneNumber,
                SenderAddress: senderAddress,

                ReceiverFirstname: receiverFirstname,
                ReceiverLastname: receiverLastname,
                ReceiverTelephoneNumber: recieverTelephoneNumber,
                ReceiverAddress: recieverAddress,
                WhoReceived: whoReceived,
                WhoReceivedAdditional: whoReceivedAdditional

            },
            dataType: "json",
            success: function(response) {
                if (response.IsSuccess) {
                    if (response.Data.RedirectUrl == null) {
                        successErrorMessage.Init({
                            Message: response.Data.Message,
                            IsSuccess: true
                        }).ShowMessage();
                    } else {
                        window.location = response.Data.RedirectUrl;
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