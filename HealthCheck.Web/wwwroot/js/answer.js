"use strict";

$(document).ready(() => {
    var answerHub = new signalR.HubConnectionBuilder().withUrl("/answerHub").build();
    var categoryHub = new signalR.HubConnectionBuilder().withUrl("/categoryHub").build();
    $("#answerSubmitted").hide();

    answerHub.start().catch(function (err) {
        return console.error(err.toString());
    });

    categoryHub.start().catch(err => {
        return console.error(err.toString());
    });

    $(".answer").click(async (event) => {
        const name = $(event.target).data("name");
        const userId = $(event.target).data("user");
        const categoryId = $(event.target).data("category");
        const sessionId = $(event.target).data("session");
        const answer = $(event.target).data("answer");
        const admin = $(event.target).data("admin");
        answerHub.invoke("SendAnswer", userId, name, categoryId, sessionId, answer, admin)
            .then(() => {
                $("#answerSubmitted").show();
            })
            .catch(err => {
            return console.log(err.toString());
        });
    });

    categoryHub.on("BackToWaitingRoom", sessionKey => {
        window.location = "WaitingRoom?sessionKey=" + sessionKey;
    });
});