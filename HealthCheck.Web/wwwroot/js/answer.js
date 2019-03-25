"use strict";

$(document).ready(() => {
    var answerHub = new signalR.HubConnectionBuilder().withUrl("/answerHub").build();
    
    answerHub.start().catch(function (err) {
        return console.error(err.toString());
    });
    
    $(".answer").click(async (event) => {
        const name = $(event.target).data("name");
        const userId = $(event.target).data("user");
        const categoryId = $(event.target).data("category");
        const sessionId = $(event.target).data("session");
        const answer = $(event.target).data("answer");
        const admin = $(event.target).data("admin");
        $.ajax({
            contentType: 'application/json',
            type: "POST",
            data: JSON.stringify({
                UserId: userId,
                SessionId: sessionId,
                CategoryId: categoryId,
                CategoryChosen: answer
            }),
            url: "api/answer",
            success: (data, textStatus, jqXHR) => {
                answerHub.invoke("SendAnswer", sessionKey, name, answer).catch(err => {
                    return console.log(err.toString());
                });
            },
            error: (jqXHR, textStatus, errorThrown) => {
                
            }
        });
        
    });
});