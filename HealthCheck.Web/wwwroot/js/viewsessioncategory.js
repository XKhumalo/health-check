"use strict";

$(document).ready(() => {
    var answerHub = new signalR.HubConnectionBuilder().withUrl("/answerHub").build();
    var categoryHub = new signalR.HubConnectionBuilder().withUrl("/categoryHub").build();
    var answers = [];

    answerHub.start().catch(err => {
        return console.error(err.toString());
    });

    categoryHub.start().catch(err => {
        return console.error(err.toString());
    });

    answerHub.on("ReceiveAnswer", (senderId, name, categoryId, sessionId, answer) => {
        var answerToAdd = {
            senderId,
            categoryId,
            sessionId,
            answer,
            guestId
        };
        var existingAnswer = answers.find(a => a.senderId === senderId);
        var rowClass = '';
        if (existingAnswer) {
            rowClass = 'table-danger';
        }
        answers.push(answerToAdd);
        var newRow = `<tr class=${rowClass}><td>${name}</td><td>${answer}</td></tr>`;
        $("#answersList tbody").append(newRow);
    });

    $("#close").click(event => {
        event.preventDefault();
        var btn = event.target;
        var sessionId = $(btn).data("sessionid");
        categoryHub.invoke("CloseCategory")
            .then(() => {
                window.location = "/Sessions/ViewSession?sessionId=" + sessionId;
            })
            .catch(err => {
            
        });
    });

    $(".ask").click(async (event) => {
        event.preventDefault();
        var btn = event.target;
        var categoryId = $(btn).data("category");
        var sessionId = $(btn).data("session");
        await categoryHub.invoke("BroadcastCategory", sessionId, categoryId)
            .then(() => {
                window.location = `/Categories/ViewSessionCategory?sessionId=${sessionId}&categoryId=${categoryId}`;
            })
            .catch(err => {
                return console.error(err.toString());
            });
    });
});