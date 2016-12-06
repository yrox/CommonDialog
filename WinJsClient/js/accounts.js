WinJS.Namespace.define("AccTypes", {
    items: new WinJS.Binding.List(accountsData)
});

function showAccs() {
    var showButton = document.getElementById("showAccs");
    document.getElementById("accsFlyout").winControl.show(showButton);
}

function confirmEditingAcc() {
    document.getElementById("accsFlyout").winControl.hide();
}

WinJS.UI.processAll().then(function () {
    var element = document.body;
    element.querySelector("#showAccs").addEventListener("click", showAccs, false);
    element.querySelector("#confirmEditingAccButton").addEventListener("click", confirmEditingAcc, false);
});