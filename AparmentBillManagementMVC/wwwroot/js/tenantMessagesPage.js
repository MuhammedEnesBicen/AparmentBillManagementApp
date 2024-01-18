﻿$(document).on("click", ".tenantMessage", function () {
    $(this).children().first().children().last().toggleClass("visually-hidden");
});

const textareaEle = document.getElementById('textarea');

textareaEle.addEventListener('input', () => {
    textarea.style.height = 'auto';
    textarea.style.height = `${textarea.scrollHeight}px`;

});


function getMessages(tenantId) {

    $("#spinner").prop("hidden", false);

    $.post("Messages", { tenantId: tenantId }, function (data) {
        $("#spinner").prop("hidden", true);
        $("#messagesArea").html(data);
    }).fail(function () {
        $("#spinner").prop("hidden", true);
        $.notify("Error while getting messages!");
    });
}

function sendMessage(tenantId) {
    var message = $("#sendMessage").prev().val();
    if (message == "") {
        $.notify("Message Cant be empty!");
        return;
    }
    $.post("Message", { tenantId: tenantId, text: message, sender: 0 }, function (data) {
        if (data.includes("message")) {
            $("#messagesArea").append(data);
            $("#sendMessage").prev().val("");
            $(".scroller").scrollTop($(".scroller")[0].scrollHeight)
        }
        else {
            $.notify("Message couldnt send! Something went wrong!");
        }

    });
}

function getNewMessages(tenantId) {
    let messages = $("#messagesArea > div > div > input");
    if (messages.length == 0) { return; }
    let messageId = messages.last().attr("name");
    $.get("Message?tenantId=" + tenantId + "&messageId=" + messageId, function (data) {
        if (data.includes("message")) {
            $("#messagesArea").append(data);
            //$(".scroller").scrollTop($(".scroller")[0].scrollHeight)
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