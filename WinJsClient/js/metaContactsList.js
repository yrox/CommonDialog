var currentMetaCon = new MetaContact();
var currentVkCon = new Contact();
var currentTgCon = new Contact();

function removeMeta() {
    // Get the control
    var list = document.getElementById("metaListView").winControl;
    var itemsToDelete = [];
    var source = list.itemDataSource;
    if (list.selection.count() > 0) {
        list.selection.getItems().done(function (items) {
            source.beginEdits();
            items.forEach(function (item) {
                itemsToDelete.push(metaContactsData[item.index]);
                source.remove(item.key);
            });
            source.endEdits();
        });
        delMetaContacts(itemsToDelete);
    }
}

function selectionMode() {
    var myListView = document.getElementById("metaListView").winControl;
    if (myListView.selectionMode == WinJS.UI.SelectionMode.single)
        myListView.selectionMode = WinJS.UI.SelectionMode.multi;
    else
        myListView.selectionMode = WinJS.UI.SelectionMode.single

}

function getSelectedItem() {
    var list = document.getElementById("metaListView").winControl;
    if (list.selection.count() > 0) {
        list.selection.getItems()
            .done(function(items) {
                currentMetaCon = metaContactsData[items[0].index];
            });
    }
    var dTitle = document.getElementById("dialogHeader");
    dTitle.textContent = currentMetaCon.name;
    if (currentMetaCon.messages != undefined) {
        //if (currentMetaCon.messages.length > 0) {
            var dialog = document.getElementById("dialogListView").winControl;
            var dList = new WinJS.Binding.List(currentMetaCon.messages);
            dialog.itemDataSource = dList.dataSource;
            //dialog.data = new WinJS.Binding.List(currentMetaCon.messages);
            dialog.forceLayout();
        //}
    }
    if (currentMetaCon.contacts != undefined){
        if (currentMetaCon.contacts.length > 0) {
            var vkI = arrayObjectIndexOf(currentMetaCon.contacts, "Vk", "type");
            if (vkI > -1) {
                var vi = arrayObjectIndexOf(vkContacts,
                    currentMetaCon.contacts[vkI].contactIdentifier,
                    "contactIdentifier");
                currentVkCon = vkContacts[vi];
            } else {
                currentVkCon = undefined;
            }
            var tgI = arrayObjectIndexOf(currentMetaCon.contacts, "Telegram", "type");
            if (tgI > -1) {
                var ti = arrayObjectIndexOf(tgContacts, currentMetaCon.contacts[tgI].contactIdentifier, "contactIdentifier");
                currentTgCon = tgContacts[ti];
            } else {
                currentTgCon = undefined;
            }
        }
    }
}

function showAddFlyout() {

    var addButton = document.getElementById("addMeta");
    document.getElementById("addMetaFlyout").winControl.show(addButton);
}

function addMeta() {
    var name = document.getElementById("nameFlyout").value;
    var list = document.getElementById("metaListView").winControl;
    var vkList = document.getElementById("vkListFlyout");
    var tgList = document.getElementById("tgListFlyout");
    var vkSelected = vkContacts[vkList.selectedIndex];
    var tgSelected = tgContacts[tgList.selectedIndex];
    var newMeta = new MetaContact(name);
    newMeta.contacts.push(vkSelected);
    newMeta.contacts.push(tgSelected);
    var source = list.itemDataSource;
    source.beginEdits();
    source.insertAtEnd(null, newMeta);
    source.endEdits();
    saveMetaContact(newMeta);
    document.getElementById("addMetaFlyout").winControl.hide();
}

function editItem(e) {
    var target = e.target;
    var listFlyout = document.getElementById("addMetaFlyout");
    document.getElementById("nameFlyout").value = currentMetaCon.name;
    //var vkSelected = arrayObjectIndexOf(vkContacts, currentVkCon.contactIdentifier, "contactIdentifier");
    //var tgSelected = arrayObjectIndexOf(tgContacts, currentTgCon.contactIdentifier, "contactIdentifier");
    if (currentMetaCon.contacts != undefined && currentMetaCon.contacts.length > 0) {
        if (vkContacts != undefined && vkContacts.length > 0) {
            var vkI = arrayObjectIndexOf(vkContacts, currentVkCon.name, "name");
            document
                .getElementById("vkListFlyout")
                .selectedIndex = vkI;
        }
        if (tgContacts != undefined && tgContacts.length > 0) {
            var tgI = arrayObjectIndexOf(tgContacts, currentTgCon.contactIdentifier, "contactIdentifier");
            document
                .getElementById("tgListFlyout")
                .selectedIndex = tgI;
        }
    }

    listFlyout.winControl.show(target);
}

function notify(text) {
    var flyout = document.getElementById("notiftFlyout").winControl;
    var flyoutText = document.getElementById("notifyText");
    flyoutText.textContent = text;
    flyout.placement = "top";
    flyout.show(document.getElementById("app"));
}

WinJS.Namespace.define("Data",
{
    meta: new WinJS.Binding.List(metaContactsData),
    vk: new WinJS.Binding.List(vkContacts)
});

WinJS.UI.processAll().then(function () {
        var element = document.body;
        var list = element.querySelector("#metaListView").winControl;
        list.onselectionchanged = getSelectedItem;
        element.querySelector("#metaListView").addEventListener("dblclick", editItem, false);
        element.querySelector("#removeMeta").addEventListener("click", removeMeta, false);
        element.querySelector("#selectMeta").addEventListener("click", selectionMode, false);
        element.querySelector("#addMeta").addEventListener("click", showAddFlyout, false);
        element.querySelector("#confirmAddingMetaButton").addEventListener("click", addMeta, false);
        element.querySelector("#confirmButton").addEventListener("click", function() {
            document.getElementById("notiftFlyout").winControl.hide();
        }, false);

    
    });

setTimeout(function() {
    //Data.items = new WinJS.Binding.List(metaContactsData);
    var mListView = document.getElementById("metaListView").winControl;
    var list = new WinJS.Binding.List(metaContactsData);
    mListView.itemDataSource = list.dataSource;
    mListView.forceLayout();
    },
    6000);

function bindContactLists() {
    var vkListView = document.getElementById("vkListFlyout").winControl;
    var vklist = new WinJS.Binding.List(vkContacts);
    vkListView.data = new WinJS.Binding.List(vkContacts);
    var tgListView = document.getElementById("tgListFlyout").winControl;
    var tglist = new WinJS.Binding.List(tgContacts);
    tgListView.data = new WinJS.Binding.List(tgContacts);
}

