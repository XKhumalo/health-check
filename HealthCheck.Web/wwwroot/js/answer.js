"use strict";

$(document).ready(() => {
    var answerHub = new signalR.HubConnectionBuilder().withUrl("/answerHub").build();
    var categoryHub = new signalR.HubConnectionBuilder().withUrl("/categoryHub").build();
    
    categoryHub.start().then(() => {
        console.info("answer.js - categoryHub connected");
    }).catch(function (err) {
        return console.error(err.toString());
    });

    answerHub.start().then(() => {
        console.info("answer.js - answerHub connected");
    }).catch(function (err) {
        return console.error(err.toString());
        });

    answerHub.on("ReceiveAnswer", (connectionId, name, answer) => {
        var newRow = "<tr><td>" + name + "</td><td>" + answer + "</td></tr>";
        console.log(newRow);
        $("#answersList tbody").append(newRow);
    });

    categoryHub.on("ReceiveCategory", (adminId, categoryId) => {
        window.location = `Answer?adminId=${adminId}&categoryId=${categoryId}`;
    });

    $(".answer").click(event => {
        const sendTo = $(event.target).data("send_to");
        const name = $(event.target).data("name");
        const answer = $(event.target).data("answer");
        answerHub.invoke("SendAnswer", sendTo, name, answer).catch(err => {
            return console.log(err.toString());
        });
    });
});