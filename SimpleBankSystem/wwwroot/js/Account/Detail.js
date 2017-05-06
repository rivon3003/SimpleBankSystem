function Deposit() {
    $.ajax({
        method: "POST",
        contentType: "application/json",
        url: "/Account/Deposit",
        data: JSON.stringify({
            Amount: $("#depositAmount").val(),
            RowVersion: $("#rowVersion").val()
        }),
        dataType: "JSON",
        success: function (response)
        {
            if (response.result.isSuccessful)
            {
                $("#balance").val(response.result.currentBalance);
                $("#rowVersion").val(response.result.rowVersion);

            }
            alert(response.result.message);
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
        data: JSON.stringify({
            Amount: $("#withdrawAmount").val()
        }),
        dataType: "JSON",
        success: function (response) {
            if (response.isSuccessful) {
                $("#balance").val(response.currentBalance);

            }
            alert(response.message);
        },
        fail: function (response) {
            alert(response);
        }
    });
}

function Transfer() {
    $.ajax({
        method: "POST",
        contentType: "application/json",
        url: "/Account/Transfer",
        data: JSON.stringify({
            Amount: $("#transferAmount").val(),
            TargetAccountNumber: $("#targetAccountNumber").val()
        }),
        dataType: "JSON",
        success: function (response) {
            if (response.isSuccessful) {
                $("#balance").val(response.currentBalance);
            }
            alert(response.message);
        },
        fail: function (response) {
            alert(response);
        }
    });
}

//$("#btnDeposit").click(Deposit());