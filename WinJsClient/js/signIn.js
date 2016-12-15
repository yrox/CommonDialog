
function askCode() {
    if (authorisedVk == false || authorisedTg == false) {
        var showButton = document.getElementById("askCodeButton");
        var flyout = document.getElementById("codeFlyout").winControl;
        flyout.placement = "right";
        flyout.show(showButton);
    }
}

function signIn() {
    var code = document.getElementById("code").value;
    authorize(code);
}


WinJS.UI.processAll().then(function () {
    var element = document.body;
    element.querySelector("#sendCodeButton").addEventListener("click", signIn, false);
    element.querySelector("#askCodeButton").addEventListener("click", askCode, false);
});