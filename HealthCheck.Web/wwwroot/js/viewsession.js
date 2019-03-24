"use strict";

$(document).ready(() => {
    var categoryHub = new signalR.HubConnectionBuilder().withUrl("/categoryHub").build();

    categoryHub.start().catch(err => {
        return console.error(err.toString());
    });

    $("#ask").click(event => {
        event.preventDefault();
        var btn = event.target;
        var categoryId = $(btn).data("category");
        var sessionId = $(btn).data("session");
        categoryHub.invoke("BroadcastCategory", sessionId, categoryId)
            .then(() => {
                window.location = `ViewSessionCategory?sessionId=${sessionId}&categoryId=${categoryId}`;
            })
            .catch(err => {
                return console.error(err.toString());
            });
    });
});