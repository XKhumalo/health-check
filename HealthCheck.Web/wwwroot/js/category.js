"use strict";

$(document).ready(() => {
    var categoryHub = new signalR.HubConnectionBuilder().withUrl("/categoryHub").build();

    categoryHub.start().then(() => {
        console.info("category.js - categoryHub connected");
    })
        .catch(err => {
            return console.error(err.toString());
        });

    categoryHub.on("ReceiveCategory", (sentBy, categoryId) => {
        window.location = `//Answer?sentBy=${sentBy}&categoryId=${categoryId}`;
    });
    
    $("#ask").click(event => {
        event.preventDefault();
        var btn = event.target;
        var category = $(btn).data("category");
        categoryHub.invoke("BroadcastCategory", category)
            .then(() => {
                
            })
            .catch(err => {
                return console.error(err.toString());
        });
    });
});