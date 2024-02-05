$(document).on("click", ".tenantMessage", function () {
    $(this).children().first().children().last().toggleClass("visually-hidden");
});

const textareaEle = document.getElementById('textarea');

textareaEle.addEventListener('input', () => {
    textarea.style.height = 'auto';
    textarea.style.height = `${textarea.scrollHeight}px`;

});


function getMessages(chatRoomId) {

    $("#spinner").prop("hidden", false);

    $.post("Message/Messages", { chatRoomId: chatRoomId }, function (data) {
        $("#spinner").prop("hidden", true);
        $("#messagesArea").html(data);
    }).fail(function () {
        $("#spinner").prop("hidden", true);
        $.notify("Error while getting messages!");
    });
}

function sendMessage(chatRoomId) {
    var message = $("#sendMessage").prev().val();
    if (!message.replace(/\s/g, '').length) {
        $.notify("Message Cant be empty!");
        return;
    }
    $.post("Message/Message", { chatRoomId: chatRoomId, text: message, sender: 0 }, function (data) {
        $(".noMessage").addClass("visually-hidden");
        if (data.includes("message")) {
            $("#messagesArea").append(data);
            $("#sendMessage").prev().val("");
            $("#sendMessage").prev().css('height', 'auto');
            $(".scroller").scrollTop($(".scroller")[0].scrollHeight)
        }
        else {
            $.notify("Message couldnt send! Something went wrong!");
        }

    });
}

function getNewMessages(chatRoomId) {
    let messages = $("#messagesArea > div > div > input");
    if (messages.length == 0) { return; }
    let lastMessageId = messages.last().attr("name");
    $.get("Message/Message?chatRoomId=" + chatRoomId + "&lastMessageId=" + lastMessageId, function (data) {
        if (data.includes("message")) {
            $.notify("New Message!", { position: "right bottom", className:"success" });
            $("#messagesArea").append(data);
            $(".scroller").scrollTop($(".scroller")[0].scrollHeight)
            $(".noMessage").addClass("visually-hidden");
        }

    });
}

function removeMessage(chatRoomId,messageId) {
    $.ajax({
        url: "Message/Delete?chatRoomId="+chatRoomId+"&messageId=" + messageId,
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
            $.notify("An Error occured while deleting message!");
        }
    });
}