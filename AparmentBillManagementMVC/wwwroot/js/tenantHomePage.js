
setInterval(function () {
    getUnreadMessageCount();
}, 5000);


function getUnreadMessageCount() {
    $.get("/TenantUser/Message/GetUnreadMessagesCount", function (data) {
        console.log("getUnreadMessageCount: " + data);
        let currentCount = Number($("#unreadMessageCount").text());
        if (data > 0) {
            $("#unreadMessageCount").text(data);
            $("#unreadMessageCount").removeClass("visually-hidden");
            if (data != currentCount) {
                $.notify("New Message", "success", { position: "bottom" });
            }

        }
        else {
            $("#unreadMessageCount").addClass("visually-hidden");
        }

    });
}


function changeBillPage(value,tenantId) {
    var billPage = Number($("#billPage").text());
    billPage += value;
    $.post("/TenantUser/Home/GetBills", { tenantId: tenantId, billPage: billPage }, function (data) {
        $("#_BillsPartial").html(data);
        if ($("#noBillAlert").length > 0) {
            $("#billPageIncrease").attr("disabled", true);
        }
        else {
            $("#billPageIncrease").attr("disabled", false);
        }
    });

    $("#billPage").text(billPage);
    if (billPage == 1) {
        $("#billPageDecrease").attr("disabled", true);
    }
    else {
        $("#billPageDecrease").attr("disabled", false);
    }

}
