"use strict";

$(document).ready(() => {
    var answerHub = new signalR.HubConnectionBuilder().withUrl("/answerHub").build();
    var categoryHub = new signalR.HubConnectionBuilder().withUrl("/categoryHub").build();

    let categoryId = '';
    let sessionId = '';
    let answer = '';

    let sessionKey = $("#SessionViewModel_SessionKey").val();

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
        categoryId = $(event.target).data("category");
        sessionId = $(event.target).data("session");
        answer = $(event.target).data("answer");
        const guestId = $(event.target).data("guest");
        const admin = $(event.target).data("admin");
        answerHub.invoke("SendAnswer", userId, name, categoryId, sessionId, answer, admin, guestId)
            .then(() => {
                $("#answerSubmitted").show();
                $(".answer").prop("disabled", true);
            })
            .catch(err => {
                return console.log(err.toString());
            });
    });

    categoryHub.on("BackToWaitingRoom", () => {
        window.location = `/SaveAnswer?sessionKey=${sessionKey}&categoryId=${categoryId}&sessionId=${sessionId}&answer=${answer}`;
    });
});