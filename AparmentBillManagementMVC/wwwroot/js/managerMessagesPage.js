let spinnerElement = $("<div  class='d-flex align-self-center spinner-border text-info' role='status'><span class='visually-hidden'> Loading...</span></div>");

function getMessages(chatRoomId) {
    $("#spinner").html(spinnerElement);

    if (chatRoomId == -1 || chatRoomId ==null) {
        $("#emptyChatArea").removeClass("visually-hidden");
        $("#spinner").html("");
        return;
    }
    $("#emptyChatArea").addClass("visually-hidden");

    $("#spinner").html(spinnerElement);

    $.post("MessageList", { chatRoomId: chatRoomId }, function (data) {
        $("#spinner").html("");
        $("#messagesArea").html(data);

        // updating chat rooms last seen mesaage
        let lastSeenMessageIdInput = "#room_" + chatRoomId + " > input.lastSeenMessageId";
        $(lastSeenMessageIdInput).attr("value", $("#lastSeenMessageIdSpan").attr("value"));
    }).fail(function () {
        $("#spinner").html("");
        $.notify("Error while getting messages!");
    });
}

function sendMessage() {
    let chatRoomIdVal = $("#chatRoomIdSpan").attr("value") ?? -1;
    if (chatRoomIdVal == -1) {
        $.notify("You should select a chat from chat list");
        return;
    }
    var message = $("#sendMessage").prev().val();
    if (message == "") {
        $.notify("Message Cant be empty!");
        return;
    }
    $.post("MessageItem", { chatRoomId: chatRoomIdVal, text: message, sender: 1 }, function (data) {
        $("#messagesArea").append(data);
        if (data.includes("message")) {
            $("#sendMessage").prev().val("");
            $(".scroller").scrollTop($(".scroller")[0].scrollHeight)
        }
    });
}

function getNewMessages(chatRoomId) {
    $.get("MessageItem?chatRoomId=" + chatRoomId , function (data) {
        if (data.includes("message")) {
            $("#messagesArea").append(data);
            //$(".scroller").scrollTop($(".scroller")[0].scrollHeight)
            let chatRoomIdVal = parseInt($("#chatRoomIdSpan").attr("value"));
            console.log("after get new messages chatRoomIdVal: " + chatRoomIdVal);
        }
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

function openChatRoom(chatRoomId) {

    let chatsButton = $("#chatsButton");
    // write code to hide sidenav if chats button is visible

    if (chatsButton.is(":visible")) {
        $(".sideNav").toggleClass("d-none");
    }

    let roomDivId = "#room_" + chatRoomId;
    getMessages(chatRoomId);
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

    let unreadMsgSpan = roomDivId + " > span";
    $(unreadMsgSpan).addClass("visually-hidden");


}