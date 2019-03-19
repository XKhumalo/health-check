"use strict";

$(document).ready(() => {
    var chatHub = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
    
    chatHub.on("ReceiveMessage", (user, message, sentBy) => {
        window.location = "Answer?sentBy=" + sentBy;
    });

    chatHub.start().then(() => {

    })
    .catch(err => {
        return console.error(err.toString());
    });

    $("#sendButton").click(event => {
        var user = $("#userInput").value;
        var message = document.getElementById("messageInput").value;
        chatHub.invoke("SendMessage", user, message).catch(err => {
            return console.error(err.toString());
        });
        event.preventDefault();
    });
});
