"use strict";

$(document).ready(() => { 
    var commentHub = new signalR.HubConnectionBuilder().withUrl("/commentHub").build();

    //Disable send button until connection is established
    document.getElementById("sendMessage").disabled = true;

    commentHub.on("ReceiveComment", function(name, comment) {
        var message = comment.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
        var encodedMessage = name + " says " + message;
        var li = document.createElement("li");
        li.textContent = encodedMessage;
        document.getElementById("discussion").appendChild(li);
    });

    commentHub.start().then(function() {
        document.getElementById("sendMessage").disabled = false;
    }).catch(function(err) {
        return console.log(err.toString());
    });

    document.getElementById("sendMessage").addEventListener("click", function(event) {
        var userName = $("#UserViewModel_Name").val();
        var comment = $("#message").val();

        commentHub.invoke("SendComment", userName, comment)
            .catch(function(err) {
                return console.log(err.toString());
            });
        event.preventDefault();
    });
});