var connection = $.hubConnection("http://localhost:8080/signalr", { useDefaultPath: false });
var chatProxy = connection.createHubProxy('ChatHub');

function saveAccount(login, password, type, phone, id) {
    chatProxy.invoke('SaveAccount', {
            Login: login,
            Password: password,
            Type: type,
            PhoneNumber: phone,
            AccountIdentifier: id
        })
        .done(function() {
            return "sucseed";
        }).fail(function(error) {
            return "failed";
        });
}
function saveMetaContact(name, contacts, id) {
    chatProxy.invoke('SaveMetaContact', {
        Name: name,
        Contacts: contacts,
        Id: id
    })
        .done(function () {
            return "sucseed";
        }).fail(function (error) {
            return "failed";
        });
}

function sendMessage(text, type, contactId, metaContact) {
    chatProxy.invoke('SendMessage', {
        Text: text,
        Type: type,
        ContactIdentifier: contactId,
        MetaContact: metaContact
    })
        .done(function () {
            return "sucseed";
        }).fail(function (error) {
            return "failed";
        });
}

function getDbMessagHistory(id, name, contacts) {
    chatProxy.invoke('GetDbMessageHistory', {
        Name: name,
        Contacts: contacts,
        Id: id
    })
        .done(function (messages) {
            return messages;
        }).fail(function (error) {
            return "failed";
        });
    
}
function getDbContactsOf(id, name) {
    chatProxy.invoke('GetDbContactsOf', {
        Name: name,
        Id: id
    })
        .done(function (contacts) {
            return contacts;
        }).fail(function (error) {
            return "failed";
        });

}
function getDbMetaContacts() {
    chatProxy.invoke('GetDbMetaCotacts')
        .done(function (contacts) {
            return contacts;
        }).fail(function (error) {
            return "failed";
        });
}

function loadMetaMessageHistory(id, name, contacts){
    chatProxy.invoke('LoadMetaMessageHistory', {
    Name: name,
    Contacts: contacts,
    Id: id
})
        .done(function (messages) {
            return messages;
        }).fail(function (error) {
            return "failed";
        });
    
}
function loadContactMessageHistory(id, name, phone, type) {
    chatProxy.invoke('LoadContactMessageHistory', {
        Name: name,
        PhoneNumber: phone,
        Id: id,
        Type: type
    })
        .done(function (messages) {
            return messages;
        }).fail(function (error) {
            return "failed";
        });

}

function loadContactsOfType(type) {
    chatProxy.invoke('LoadContactsOfType', type)
        .done(function (contacts) {
            return contacts;
        }).fail(function (error) {
            return "failed";
        });
}
function loadAllContacts() {
    chatProxy.invoke('LoadAllContacts')
        .done(function (contacts) {
            return contacts;
        }).fail(function (error) {
            return "failed";
        });
}
function getContactById(id, type) {
    chatProxy.invoke('GetContactById', type, id)
        .done(function (contact) {
            return contact;
        }).fail(function (error) {
            return "failed";
        });
}
function getContact(type, nameOrPhone) {
    chatProxy.invoke('GetContact', type, nameOrPhone)
        .done(function (contact) {
            return contact;
        }).fail(function (error) {
            return "failed";
        });
}
//var chatHub = $.connection.ChatHub;
//chatHub.client.SaveAccount({Type: ""})
