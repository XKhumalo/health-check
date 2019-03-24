"use strict";

$(document).ready(() => {
    var answerHub = new signalR.HubConnectionBuilder().withUrl("/answerHub").build();
    
    answerHub.start().catch(function (err) {
        return console.error(err.toString());
    });
    
    $(".answer").click(event => {
        const sessionKey = $(event.target).data("sessionKey");
        const name = $(event.target).data("name");
        const answer = $(event.target).data("answer");
        answerHub.invoke("SendAnswer", sessionKey, name, answer).catch(err => {
            return console.log(err.toString());
        });
    });
});