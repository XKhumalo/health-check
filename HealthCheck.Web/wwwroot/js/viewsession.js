"use strict";

$(document).ready(() => {
    var categoryHub = new signalR.HubConnectionBuilder().withUrl("/categoryHub").build();

    categoryHub.start().catch(err => {
        return console.error(err.toString());
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