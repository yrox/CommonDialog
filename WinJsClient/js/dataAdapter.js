function metaAdapter(metas) {
    if (metas != undefined) {
        $.each(metas,
        function () {
            var meta = new MetaContact(this.Name, this.Id);
            pushContacts(this.Contacts, meta);
            pushMessages(this.Messages, meta);
            metaContactsData.push(meta);
        });
    };
}

function pushContacts(contacts, meta) {
    if (contacts != undefined) {
        $.each(contacts,
            function() {
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
}

function pushMessages(messages, meta) {
    if (messages != undefined) {
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
    
}

function pushAccsContacts(contacts) {
    if (contacts != undefined) {
        $.each(contacts,
        function () {
            var contact = new Contact(
                this.Id,
                this.Name,
                this.PhoneNumber,
                this.ContactIdentifier,
                this.Type,
                null);
            if (this.Type == "Vk") {
                vkContacts.push(contact);
            }
            if (this.Type == "Telegram") {
                tgContacts.push(contact);
            }
        });
    }
}

function pushAccs(accs) {
    if (accs != undefined) {
        $.each(accs,
       function () {
           var acc = new Account(
               this.Id,
               this.Login,
               this.Password,
               this.PhoneNumber,
               this.AccountIdentifier,
               this.Type,
               null);
           accountsData.push(acc);
       });
    }
}