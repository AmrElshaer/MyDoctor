"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/tablesTrackerHup").build();

connection.on("notifiyTableTracker", function (listTableTracks) {
    $("#NotificationContent").html('');
    $('#NumberOfNotification').html(listTableTracks.length == 0 ? "" : listTableTracks.length);
    if (listTableTracks.length==0) {
        $("#NotificationContent").append("You Have Last Update")
    }
    listTableTracks.forEach(a => {

        var content=` <li>
            <a href="/${a.controller}/${a.action}/${a.itemId}">
                <span class="mr-3 avatar-icon bg-success-lighten-2"><img style="max-width:50px" src="${a.image}" class="img-fluid"/></span>
                <div class="notification-content">
                    <h6 class="notification-heading">${a.title}</h6>
                    <span class="notification-text">${a.content}</span >
                </div>
            </a>
        </li>`
        $("#NotificationContent").append(content);
    })
});

connection.start().then(function () {
    console.log("Connection Start");
}).catch(function (err) {
    return console.error(err.toString());
});

