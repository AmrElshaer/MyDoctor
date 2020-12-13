"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/messageHup").build();

connection.start().then(function () {
    console.log("Connection Start");
}).catch(function (err) {
    return console.error(err.toString());
});
document.getElementById("SendButton").addEventListener("click", function (event) {
    let touser = $("#ToUser").val();
    let content = $("#Content").val();
    let fromUser = $("#FromUser").val();
    let imagePath = $("#ImagePath").val();
    $("#Content").val('');
    const message = {
        ToName: touser,
        Content: content
    };
    var contentHtml = `
                                        <div class="media pt-5">
                                            <img class="mr-3 rounded-circle" style="max-width:40px" src="/images/${imagePath}">
                                            <div class="media-body">
                                                <h5 class="m-b-3">${fromUser}</h5>
                                            </div>

                                        </div>
                                        <p>${message.Content}</p>

                         `;
    $("#CurrentChat").append(contentHtml);
    connection.invoke("SendMessage", message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});