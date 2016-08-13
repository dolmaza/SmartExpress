$(function () {
    $(".Apply-Numeric").numericInput({
        allowFloat: true
    });

    $("#ReceiverTelephoneNumber, #SenderTelephoneNumber").mask("(599)-99-99-99");

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
                MessageType: messageType,
                ReceiveDate: receiveDate,
                DeliveryDate: deliveryDate,
                UnitPrice: unitPrice,
                TotalPrice: totalPrice,
                Direction: direction,
                MessageMode: messageMode,
                Payer: payer,
                FormOfPayment: formOfPayment,
                Quantity: quantity,
                Weigth:weigth,

                ContractNumber: contractNumber,
                CompanyName: companyName,
                SenderFirstname: senderFirstname,
                SenderLastname: senderLastname,
                SenderTelephoneNumber: senderTelephoneNumber,
                SenderAddress: senderAddress,

                ReceiverFirstname: receiverFirstname,
                ReceiverLastname: receiverLastname,
                RecieverTelephoneNumber: recieverTelephoneNumber,
                RecieverAddress: recieverAddress,
                WhoReceived: whoReceived,
                WhoReceivedAdditional: whoReceivedAdditional

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