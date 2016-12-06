function askCode() {
    var showButton = document.getElementById("askCodeButton");
    document.getElementById("codeFlyout").winControl.show(showButton);
}

function signIn() {
    var code = document.getElementById("code").valueAsString;
    authorize(code);
}

WinJS.UI.processAll().then(function () {
    var element = document.body;
    element.querySelector("#sendCodeButton").addEventListener("click", showAccs, false);
    element.querySelector("#askCodeButton").addEventListener("click", askCode, false);
});