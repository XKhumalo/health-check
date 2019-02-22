"use strict";

$(document).ready(function () {
    var chatHub = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
    var answerHub = new signalR.HubConnectionBuilder().withUrl("/answerHub").build();

    chatHub.on("ReceiveMessage", (user, message, sentBy) => {
        window.location = "https://localhost:44324/Answer?sentBy=" + sentBy;
    });

    answerHub.on("ReceiveAnswer", (connectionId, answer) => {
        var li = document.createElement("li");
        li.textContent = connectionId + " says " + answer;
        $("#answersList").appendChild(li);
    });


    chatHub.start().then(function () {
        
    }).catch(function (err) {
        return console.error(err.toString());
    });

    document.getElementById("sendButton").addEventListener("click", function (event) {
        var user = document.getElementById("userInput").value;
        var message = document.getElementById("messageInput").value;
        chatHub.invoke("SendMessage", user, message).catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();
    });
});
