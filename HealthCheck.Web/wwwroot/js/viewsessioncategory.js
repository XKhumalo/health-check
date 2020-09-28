"use strict";

$(document).ready(() => {
    var answerHub = new signalR.HubConnectionBuilder().withUrl("/answerHub").build();
    var categoryHub = new signalR.HubConnectionBuilder().withUrl("/categoryHub").build();

    answerHub.start().catch(err => {
        return console.error(err.toString());
    });

    categoryHub.start().catch(err => {
        return console.error(err.toString());
    });

    answerHub.on("ReceiveAnswer", (senderId, name, categoryId, sessionId, answer, guestId, answerDescription) => {
        if (isGuestUser(guestId)) {
            var answerToAdd = {
                sessionOnlyUserId: guestId,
                name: name,
                categoryId: categoryId,
                sessionId: sessionId,
                answer: answer,
                answerDescription: answerDescription
            };

            addOrUpdateGuestAnswer(guestId, name, answerDescription, answerToAdd)
        } else {
            var answerToAdd = {
                userId: senderId,
                name: name,
                categoryId: categoryId,
                sessionId: sessionId,
                answer: answer,
                answerDescription: answerDescription
            };

            addOrUpdateUserAnswer(senderId, name, answerDescription, answerToAdd)
        }
    });

    function isGuestUser(guestId) {
        return guestId > 0;
    }

    function addOrUpdateGuestAnswer(guestId, name, answerDescription, answerToAdd) {
        var existingAnswer = guestAnswers.findIndex(a => a.sessionOnlyUserId == guestId);
        if (existingAnswer != -1) {
            var newRow = `<tr id=${guestId}>                         
                                <td class="user-email">${name}</td>
                                <td class="user-answer">${answerDescription}</td>
                              </tr>`;
            $(newRow).replaceAll("#" + guestId);
        }
        else {
            guestAnswers.push(answerToAdd);
            numAnswersCaptured++;
            var newAnswers = `<h3 class="text-center" id="answersCaptured">Answers Captured: ${numAnswersCaptured}</h3>`;
            $(newAnswers).replaceAll("#answersCaptured");
            var newRow = `<tr id=${guestId}>
                                <td class="user-email">${name}</td>
                                <td class="user-answer">${answerDescription}</td>
                              </tr>`;
            $("#answersList tbody").append(newRow);
        }
    }

    function addOrUpdateUserAnswer(senderId, name, answerDescription, answerToAdd) {
        var existingAnswer = userAnswers.findIndex(a => a.userId == senderId);
        if (existingAnswer != -1) {
            var newRow = `<tr id=${senderId}>                         
                                <td class="user-email">${name}</td>
                                <td class="user-answer">${answerDescription}</td>
                              </tr>`;
            $(newRow).replaceAll("#" + senderId);
        }
        else {
            userAnswers.push(answerToAdd);
            numAnswersCaptured++;
            var newAnswers = `<h3 class="text-center" id="answersCaptured">Answers Captured: ${numAnswersCaptured}</h3>`;
            $(newAnswers).replaceAll("#answersCaptured");
            var newRow = `<tr id=${senderId}>
                                <td class="user-email">${name}</td>
                                <td class="user-answer">${answerDescription}</td>
                              </tr>`;
            $("#answersList tbody").append(newRow);
        }
    }

    $("#close").click(event => {
        event.preventDefault();
        var btn = event.target;
        var sessionId = $(btn).data("sessionid");
        categoryHub.invoke("CloseCategory")
            .then(() => {
                window.location = "/Sessions/ViewSession?sessionId=" + sessionId;
            })
            .catch(err => {
            
        });
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