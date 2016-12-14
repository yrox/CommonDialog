var currentMetaCon = new MetaContact();

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
        if (currentMetaCon.messages.count > 0) {
            var dialog = document.getElementById("dialogListView").winControl;
            var dList = new WinJS.Binding.List(currentMetaCon.messages);
            dialog.itemDataSource = dList.dataSource;
            dialog.forceLayout();
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
    var vkSelected = vkList.options[vkList.selectedIndex];
    var tgSelected = tgList.options[vkList.selectedIndex];
    var newMeta = new MetaContact(name);
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

