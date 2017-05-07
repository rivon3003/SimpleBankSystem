function Success(response) {
    if (response.result.isSuccessful) {
        $("#balance").val(response.result.currentBalance);
        $("#rowVersion").val(response.result.rowVersion);

    }
    alert(response.result.message);
}

function CreateDataObject(amountEleName)
{
    return {
        Amount: $("#" + amountEleName).val(),
        RowVersion: $("#rowVersion").val()
    }
}

function Deposit() {
    $.ajax({
        method: "POST",
        contentType: "application/json",
        url: "/Account/Deposit",
        data: JSON.stringify(CreateDataObject("depositAmount")),
        dataType: "JSON",
        success: function (response)
        {
            Success(response);
        },
        fail: function (response)
        {
            alert(response);
        }
    });
}

function Withdraw() {
    $.ajax({
        method: "POST",
        contentType: "application/json",
        url: "/Account/Withdraw",
        data: JSON.stringify(CreateDataObject("withdrawAmount")),
        dataType: "JSON",
        success: function (response) {
            Success(response);
        },
        fail: function (response) {
            alert(response);
        }
    });
}

function Transfer() {
    var data = CreateDataObject("transferAmount");
    data.TargetAccountNumber = $("#targetAccountNumber").val();

    $.ajax({
        method: "POST",
        contentType: "application/json",
        url: "/Account/Transfer",
        data: JSON.stringify(data),
        dataType: "JSON",
        success: function (response) {
            Success(response);
        },
        fail: function (response) {
            alert(response);
        }
    });
}