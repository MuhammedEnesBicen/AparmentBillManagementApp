let spinnerElement = $("<div  class='d-flex align-self-center spinner-border text-info' role='status'><span class='visually-hidden'> Loading...</span></div>");

function getMessages(tenantId) {
    $("#spinner").html(spinnerElement);

    if (tenantId == -1 || tenantId==null) {
        $("#emptyChatArea").removeClass("visually-hidden");
        $("#spinner").html("");
        return;
    }
    $("#emptyChatArea").addClass("visually-hidden");

    $("#spinner").html(spinnerElement);

    $.post("MessageList", { tenantId: tenantId }, function (data) {
        $("#spinner").html("");
        $("#messagesArea").html(data);
    }).fail(function () {
        $("#spinner").html("");
        $.notify("Error while getting messages!");
    });
}

function sendMessage() {
    let tenantIdVal = $("#tenantIdSpan").attr("value") ?? -1;
    if (tenantIdVal == -1) {
        $.notify("You should select a chat from chat list");
        return;
    }
    var message = $("#sendMessage").prev().val();
    if (message == "") {
        $.notify("Message Cant be empty!");
        return;
    }
    $.post("MessageItem", { tenantId: tenantIdVal, text: message, sender: 1 }, function (data) {
        $("#messagesArea").append(data);
        if (data.includes("message")) {
            $("#sendMessage").prev().val("");
            $(".scroller").scrollTop($(".scroller")[0].scrollHeight)
        }
    });
}

function getNewMessages() {
    let tenantIdVal = $("#tenantIdSpan").attr("value") ?? -1;
    if (tenantIdVal == -1) {
        $.notify("You should select a chat from chat list");
        return;
    }
    let messages = $("#messagesArea > div > div > input");
    let messageId = messages.last().attr("name");
    $.get("MessageItem?tenantId=" + tenantIdVal + "&messageId=" + messageId, function (data) {
        $("#messagesArea").append(data);
    });
}

function removeMessage(messageId) {
    $.ajax({
        url: "Delete?messageId=" + messageId,
        type: 'DELETE',
        success: function (result) {
            var elementToRemove = $("#messagesArea > div > div > input[name='" + messageId + "']").parent().parent();
            if (elementToRemove.next().hasClass("messageDiv") == false) {
                if (elementToRemove.prev().hasClass("messageDateDivider") == true) {
                    console.log(elementToRemove.prev());

                    elementToRemove.prev().remove();
                }
            }
            elementToRemove.remove();
            $.notify("Message removed!", { className: "success" });
        },
        error: function (result) {
            $.notify("Error while deleting message!");
        }
    });
}

function filterTenants() {
    let blockName = $("#blockRadioBtns input[type='radio']:checked").attr("id");
    let chatRoomItems = $(".chatRoomItem");
    if (blockName == "all") {
        chatRoomItems.show();
    }
    else {
        chatRoomItems.hide();
        chatRoomItems.each(function () {
            if ($(this).find("span").text().includes(blockName)) {
                $(this).show();
            }
        });
    }
}

function openChatRoom(tenantId) {
    let roomDivId = "#room_" + tenantId;
    getMessages(tenantId);
    $(".selectedRoom > h5 > span").removeClass("text-white");
    $(".selectedRoom > h5 > span").addClass("text-info");
    $(".selectedRoom").removeClass("bg-info");
    $(".selectedRoom").addClass("bg-white");
    $(".selectedRoom").removeClass("text-white");
    $(".selectedRoom").removeClass("selectedRoom");

    $(roomDivId).removeClass("bg-white");
    $(roomDivId).addClass("bg-info");
    $(roomDivId).addClass("selectedRoom");
    $(roomDivId).addClass("text-white");

    let span = roomDivId + " > h5 > span";
    $(span).removeClass("text-info");
    $(span).addClass("text-white");
}