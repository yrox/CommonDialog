WinJS.Namespace.define("AccTypes", {
    items: new WinJS.Binding.List(accountsData)
});

var vkAccIndex;

function showAccs() {
    var showButton = document.getElementById("showAccs");
    vkAccIndex = arrayObjectIndexOf(accountsData, "Vk", "type");
    document.getElementById("vkLogin").value = accountsData[vkAccIndex].login;
    document.getElementById("vkPassword").value = accountsData[vkAccIndex].password;
    document.getElementById("accsFlyout").winControl.show(showButton);
}

function confirmEditingAcc() {
    saveAccounts(accountsData);
    document.getElementById("accsFlyout").winControl.hide();
}

WinJS.UI.processAll().then(function () {
    var element = document.body;
    element.querySelector("#showAccs").addEventListener("click", showAccs, false);
    element.querySelector("#confirmEditingAccButton").addEventListener("click", confirmEditingAcc, false);
});