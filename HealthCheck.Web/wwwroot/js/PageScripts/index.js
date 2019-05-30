"use strict";

$(document).ready(function () {
    // Match element with id ending in 'IsCredentialsIncorrect'
    let credentialFlag = $("[id$='IsCredentialsIncorrect']").val();

    if (credentialFlag && credentialFlag === "True") {
        AutoHideCredentialAlert();
    }
});

function AutoHideCredentialAlert() {
    window.setTimeout(function () {
        $(".alert").fadeTo(500, 0).slideUp(500,
            function () {
                $(this).remove();
            });
    }, 4000);
}
