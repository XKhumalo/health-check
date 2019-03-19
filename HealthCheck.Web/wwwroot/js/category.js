"use strict";

$(document).ready(() => {
    var categoryHub = new signalR.HubConnectionBuilder().withUrl("/categoryHub").build();

    categoryHub.start().then(() => {

    })
        .catch(err => {
            return console.error(err.toString());
        });

    

    $("#ask").click(event => {
        var btn = event.target;
        var category = $(btn).data("category");
        categoryHub.invoke("BroadcastCategory", category).catch(err => {
            return console.error(err.toString());
        });
    });
});