"use strict";

$(document).ready(() => {
    var answerHub = new signalR.HubConnectionBuilder().withUrl("/answerHub").build();

    answerHub.start().catch(err => {
        return console.error(err.toString());
    });

    answerHub.on("ReceiveAnswer", (connectionId, name, answer) => {
        var newRow = "<tr><td>" + name + "</td><td>" + answer + "</td></tr>";
        $("#answersList tbody").append(newRow);
    });
});