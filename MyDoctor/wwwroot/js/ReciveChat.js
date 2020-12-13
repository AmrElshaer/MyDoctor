"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/messageHup").build();

connection.on("receiveMessage", function (messages) {
    let currentMessages = messages.filter(a => a.fromName == $("#ToUser").val());
    currentMessages.forEach(a => {
        var content = `
                                                <div class="media pt-5">
                                                    <img class="mr-3 rounded-circle" style="max-width:40px" src="${a.fromImage}">
                                                    <div class="media-body">
                                                        <h5 class="m-b-3">${a.fromName}</h5>
                                                        <p class="m-b-2">${a.createDate}</p>
                                                    </div>

                                                </div>
                                                <p>${a.content}</p>

                                 `
        $("#CurrentChat").append(content);
    })
});

connection.start().then(function () {
    console.log("Connection Start");
}).catch(function (err) {
    return console.error(err.toString());
});