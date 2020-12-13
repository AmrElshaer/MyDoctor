"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/messageHup").build();

connection.on("receiveMessage", function (messages) {
    $("#Messages").html('');
    $('#NumberOfMessage').html(messages.length == 0 ? "" : messages.length);
    if (messages.length == 0) {
        $("#Messages").append("No messages");
    }
    /* Add Messages in Message Notification Content*/
    messages.forEach(a => {

        var content = `   <li> <a href="/Messages/MessageDetail?fromName=${a.fromName}">
                                                <img class="float-left mr-3 avatar-img" src="${a.fromImage}" alt="">
                                                <div class="notification-content">
                                                    <div class="notification-heading">${a.fromName}</div>
                                                    <div class="notification-timestamp">${a.createDate}</div>
                                                    <div class="notification-text">${a.content}</div>
                                                </div>
                                            </a>
                                        </li>`
        $("#Messages").append(content);
    })

    $("#Messages").append(content);
});

connection.start().then(function () {
    console.log("Connection Start");
}).catch(function (err) {
    return console.error(err.toString());
});

