var metaContactsData = [];
var accountsData = [];
var vkContacts = [];
var tgContacts = [];

function parseMeta(list) {
}

function loadData() {
    
    loadContactsOfType("Vk")
        .done(function (vk) {
            vkContacts = vk;
    });
}
