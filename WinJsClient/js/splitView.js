//document.getElementById("sendButtton").addEventListener("click", sendMessage, false);

function sendMessage() {
    var text = document.getElementById("messageToSend").value;
    //var type = currentMetaCon.messages[currentMetaCon.messages.length - 1].type;
    var id = currentMetaCon.contacts[arrayObjectIndexOf(currentMetaCon.contacts, "Telegram", "type")].contactIdentifier;
    var meta = new MetaContact(currentMetaCon.name, currentMetaCon.id);
    var mes = new Message(0, text, new Date(), "Telegram", id, meta);
    doSmth(mes);
    document.getElementById("messageToSend").value = "";
}

WinJS.UI.processAll().done(function () {
    var splitView = document.querySelector(".splitView").winControl;
    document.body.querySelector("#sendButtton").addEventListener("click", sendMessage, false);
    
    new WinJS.UI._WinKeyboard(splitView.paneElement); // Temporary workaround: Draw keyboard focus visuals on NavBarCommands
});


