function loadMetasData(metas) {
    $.each(metas,
        function() {
            var meta = new MetaContact(this.Name, this.id);
            loadMetasContacts(meta);
            loadMetasMessages(meta);
            metaContactsData.push(meta)
        });
}

function loadMetasContacts(meta) {
    var contacts = getDbContactsOf(meta);
    $.each(contacts,
        function () {
            var contact = new Contact(
                this.Id,
                this.Name,
                this.PhoneNumber,
                this.ContactIdentifier,
                this.Type,
                this.MetaContact);
            meta.contacts.push(contact);
        });
}

function loadMetasMessages(meta) {
    var messages = getDbMessagHistory(meta);
    $.each(messages,
        function () {
            var message = new Message(
                this.Id,
                this.Text,
                this.Date,
                this.Type,
                this.ContactIdentifier,
                this.MetaContact);
            meta.messages.push(message);
        });
}

function loadVkContacts() {
    var contacts = loadContactsOfType("Vk");
    $.each(contacts,
        function () {
            var contact = new Contact(
                this.Id,
                this.Name,
                this.PhoneNumber,
                this.ContactIdentifier,
                this.Type,
                null);
            vkContacts.push(contact);
        });
}

function loadAccs() {
    
}