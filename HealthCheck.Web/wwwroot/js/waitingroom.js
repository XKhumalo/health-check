"use strict";

$(document).ready(() => {
    var categoryHub = new signalR.HubConnectionBuilder().withUrl("/categoryHub").build();
    
    categoryHub.start().catch(err => {
        return console.error(err.toString());
    });

    categoryHub.on("ReceiveCategory", (adminId, sessionId, categoryId) => {
        window.location = `Answer?adminId=${adminId}&sessionId=${sessionId}&categoryId=${categoryId}`;
    });
});