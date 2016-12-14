var metaContactsData = [];
var accountsData = [];
var vkContacts = [];
var tgContacts = [];

var authorisedVk = new Boolean(false);
var authorisedTg = new Boolean(false);

function arrayObjectIndexOf(myArray, searchTerm, property) {
    for (var i = 0, len = myArray.length; i < len; i++) {
        if (myArray[i][property] === searchTerm) return i;
    }
    return -1;
}