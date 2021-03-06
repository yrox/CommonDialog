﻿function metaAdapter(metas) {
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

function pushMessage(message) {
    var index = arrayObjectIndexOf(metaContactsData, message.metaContact.id, "id");
    metaContactsData[index].messages.push(message);
    var dialog = document.getElementById("dialogListView").winControl;
    var dList = new WinJS.Binding.List(currentMetaCon.messages);
    dialog.itemDataSource = dList.dataSource;
    dialog.forceLayout();
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

function sortMessages(messages) {
    if (messages != undefined) {
        $.each(messages,
            function() {
                var message = new Message(
                    this.Id,
                    this.Text,
                    this.Date,
                    this.Type,
                    this.ContactIdentifier,
                    this.MetaContact);
                metaContactsData[this.MetaContact.Id].messages.push(message);
 
                //if (this.Type == "Vk") {
                //    var i = arrayObjectIndexOf(vkContacts, this.ContactIdentifier, "contactIdentifier");
                //    $.each(metaContactsData,
                //        function () {
                //            if (arrayObjectIndexOf(this
                //                    .contacts,
                //                    vkContacts[i].contactIdentifier,
                //                    "contactIdentifier") >
                //                -1)
                //                meta = arrayObjectIndexOf(metaContactsData, this.id, "");
                //        });
                //}

                //if (this.Type == "Telegram") {
                //    var i = arrayObjectIndexOf(tgContacts, this.ContactIdentifier, "contactIdentifier");
                //    $.each(metaContactsData,
                //        function() {
                //            if (arrayObjectIndexOf(this
                //                    .contacts,
                //                    tgContacts[i].contactIdentifier,
                //                    "contactIdentifier") >
                //                -1)
                //                meta = arrayObjectIndexOf(metaContactsData, this.id, "");
                //        });
                //}
                
            });
    }
}