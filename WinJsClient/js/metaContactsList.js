var currentMetaCon;
var metaConItems = [];


metaConItems = [
        { title: "Marvelous Mint", text: "Gelato", picture: "/images/fruits/60Mint.png" },
        { title: "Succulent Strawberry", text: "Sorbet", picture: "/images/fruits/60Strawberry.png" },
        { title: "Banana Blast", text: "Low-fat frozen yogurt", picture: "/images/fruits/60Banana.png" },
        { title: "Lavish Lemon Ice", text: "Sorbet", picture: "/images/fruits/60Lemon.png" },
        { title: "Creamy Orange", text: "Sorbet", picture: "/images/fruits/60Orange.png" },
        { title: "Very Vanilla", text: "Ice Cream", picture: "/images/fruits/60Vanilla.png" },
        { title: "Banana Blast", text: "Low-fat frozen yogurt", picture: "/images/fruits/60Banana.png" },
        { title: "Lavish Lemon Ice", text: "Sorbet", picture: "/images/fruits/60Lemon.png" }
];


function remove() {
    // Get the control
    var list = document.getElementById("metaListView").winControl;
    var source = list.itemDataSource;
    if (list.selection.count() > 0) {
        list.selection.getItems().done(function (items) {
            source.beginEdits();
            items.forEach(function (item) {
                source.remove(item.key);
            });
            source.endEdits();
        });
    }
}

function selectionMode() {
    var myListView = document.getElementById("metaListView").winControl;
    if (myListView.selectionMode == WinJS.UI.SelectionMode.single)
        myListView.selectionMode = WinJS.UI.SelectionMode.multi;
    else
        myListView.selectionMode = WinJS.UI.SelectionMode.single

}

function showAddFlyout() {

    var addButton = document.getElementById("addMeta");
    document.getElementById("addMetaFlyout").winControl.show(addButton);
}

function confirmAddMeta() {
    var name = document.getElementById("nameFlyout").value;
    var vkList = document.getElementById("vkListFlyout");
    var vkSelected = vkList.options[vkList.selectedIndex];
    
    var newMeta = new MetaContact(name);
    saveMetaContact(newMeta);
    document.getElementById("addMetaFlyout").winControl.hide();
}

function double(e) {
    var target = e.target;
    var listFlyout = document.getElementById("listItemFlyout");
    listFlyout.winControl.show(target);
}


WinJS.Namespace.define("Pane.MetaListView", {
    data: new WinJS.Binding.List(metaConItems)
});
WinJS.Namespace.define("Data", {
    items: new WinJS.Binding.List(metaConItems)
});

WinJS.UI.processAll().then(function () {
    var element = document.body;
    element.querySelector("#metaListView").addEventListener("dblclick", double, false);
    element.querySelector("#removeMeta").addEventListener("click", remove, false);
    element.querySelector("#selectMeta").addEventListener("click", selectionMode, false);
    element.querySelector("#addMeta").addEventListener("click", showAddFlyout, false);
    element.querySelector("#confirmAddingMetaButton").addEventListener("click", confirmAddMeta, false);
});

