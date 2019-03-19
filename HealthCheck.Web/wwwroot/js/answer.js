"use strict";

$(document).ready(() => {
    var answerHub = new signalR.HubConnectionBuilder().withUrl("/answerHub").build();
    var categoryHub = new signalR.HubConnectionBuilder().withUrl("/categoryHub").build();
    
    

    answerHub.start().then(() => {

    }).catch(function (err) {
        return console.error(err.toString());
        });

    answerHub.on("ReceiveAnswer", (connectionId, answer) => {
        var li = document.createElement("li");
        li.textContent = connectionId + " says " + answer;
        console.log(li);
        $("#answersList").append(li);
    });

    categoryHub.on("BroadcastCategory", (adminId, categoryId) => {
        window.location = `https:////localhost:44324//Answer?adminId=${adminId}&categoryId=${categoryId}`;
    });

    $(".answer").click(event => {
        const sendTo = $(event.target).data("send_to");
        const answer = $(event.target).data("answer");
        answerHub.invoke("SendAnswer", sendTo, answer).catch(err => {
            return console.log(err.toString());
        });
    });
});