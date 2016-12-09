var connection = $.hubConnection("http://localhost:8080/signalr", { useDefaultPath: false });
var chatProxy = connection.createHubProxy("ChatHub");

function showCaptcha(url, sid) {
    var div = document.createElement("div");
    div.className = "captcha";

    var img = document.createElement("img");
    img.src = url;
    div.appendChild(img);

    var form = document.createElement("form");

    var txt = document.createElement("input");
    txt.type = "text";
    txt.id = "captchaText";
    form.appendChild(txt);

    var button = document.createElement("button");
    button.onclick = getCaptcha(sid);
    var btnName = document.createTextNode("ok");
    button.appendChild(btnName);
    form.appendChild(button);

    div.appendChild(form);

    document.body.appendChild(div);
}
function getCaptcha(sid) {
    var captcha = document.getElementById("captchaText").value;
    sendCaptcha(captcha, sid);
}

chatProxy.on("RecognizeCaptcha", function (url, sid) {
    showCaptcha(url, sid);
});

connection.logging = true;
connection.start().done(doSmth);

function doSmth() {
    getDbMetaContacts();
}

function authorize(code) {
    chatProxy.invoke("Authorize", code)
        .done(function () {
            loadData();
        }).fail(function (error) {
            return "failed";
        });
}
function sendCaptcha(captcha, sid) {
    chatProxy.invoke("SendCaptcha", captcha, sid)
        .done(function () {
            return "sucseed";
        }).fail(function (error) {
            return "failed";
        });
}

function saveAccount(login, password, type, phone, id) {
    chatProxy.invoke("SaveAccount", {
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
    chatProxy.invoke("SaveMetaContact", {
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
    chatProxy.invoke("SendMessage", {
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

function getDbMessagHistory(id, name) {
    chatProxy.invoke("GetDbMessageHistory", {
        Name: name,
        Id: id
    })
        .done(function (messages) {
            return messages;
        }).fail(function (error) {
            return "failed";
        });
    
}
function getDbContactsOf(meta) {
    chatProxy.invoke("GetDbContactsOf", { Name: meta.Name, Id: meta.Id })
.done(function (contacts) {
            return contacts;
        }).fail(function (error) {
            return "failed";
        });

}
function getDbMetaContacts() {
    chatProxy.invoke("GetDbMetaContacts")
        .done(function (contacts) {
            tempMeta = contacts[1];
            getDbContactsOf({Name: tempMeta.Name, Id: tempMeta.Id});
        }).fail(function (error) {
            return error.message;
        });
}

function loadMetaMessageHistory(id, name, contacts){
    chatProxy.invoke("LoadMetaMessageHistory", {
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
    chatProxy.invoke("LoadContactMessageHistory", {
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
    chatProxy.invoke("LoadContactsOfType", type)
        .done(function (contacts) {
            return contacts;
        }).fail(function (error) {
            return "failed";
        });
}
function loadAllContacts() {
    chatProxy.invoke("LoadAllContacts")
        .done(function (contacts) {
            return contacts;
        }).fail(function (error) {
            return "failed";
        });
}
function getContactById(id, type) {
    chatProxy.invoke("GetContactById", type, id)
        .done(function (contact) {
            return contact;
        }).fail(function (error) {
            return "failed";
        });
}
function getContact(type, nameOrPhone) {
    chatProxy.invoke("GetContact", type, nameOrPhone)
        .done(function (contact) {
            return contact;
        }).fail(function (error) {
            return "failed";
        });
}


//var chatHub = $.connection.ChatHub;
//chatHub.client.SaveAccount({Type: ""})
