"use strict";

$(document).ready(() => {
    var sessionHub = new signalR.HubConnectionBuilder().withUrl("/sessionHub").build();

    sessionHub.start().catch(function (err) {
            return console.error(err.toString());
        });


    $("#join-session").click(event => {
        event.preventDefault();
        var sessionKey = $("#SessionKey").val();
        if (sessionKey.length !== 6) {
            console.err("Session Key must be 6 characters");
            return;
        }
        sessionHub.invoke("AddToGroup", sessionKey)
            .then(() => {
                window.location = "WaitingRoom?sessionKey=" + sessionKey;
            })
            .catch(err => { throw err; });
    });
});