"use strict";

$(document).ready(() => {
    var answerHub = new signalR.HubConnectionBuilder().withUrl("/answerHub").build();
    
    answerHub.on("ReceiveAnswer", (connectionId, answer) => {
        var li = document.createElement("li");
        li.textContent = connectionId + " says " + answer;
        console.log(li);
        $("#answersList").append(li);
    });

    answerHub.start().then(() => {

    }).catch(function (err) {
        return console.error(err.toString());
    });

    $("#btn_bad").click(() => {
        const sendTo = $("#btn_bad").data("send_to");
        answerHub.invoke("SendAnswer", sendTo, "bad").catch(err => {
            return console.log(err.toString());
        });
    });
});