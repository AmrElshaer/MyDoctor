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
    $("#Content").val('');
    const message = {
        ToName: touser,
        Content: content
    };
    connection.invoke("SendMessage", message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});