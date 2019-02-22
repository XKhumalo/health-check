"use strict";

$(document).ready(function () {
    var answerHub = new signalR.HubConnectionBuilder().withUrl("/answerHub").build();
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