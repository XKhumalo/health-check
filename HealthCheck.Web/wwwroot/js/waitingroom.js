"use strict";

$(document).ready(() => {
    var categoryHub = new signalR.HubConnectionBuilder().withUrl("/categoryHub").build();
    
    categoryHub.start().catch(err => {
        return console.error(err.toString());
    });

    categoryHub.on("ReceiveCategory", (adminId, sessionKey, categoryId) => {
        window.location = `Answer?adminId=${adminId}&sessionKey=${sessionKey}&categoryId=${categoryId}`;
    });
});