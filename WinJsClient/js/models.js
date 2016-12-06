function Account(id, login, password, phone, accId, type) {
    this.id = id;
    this.login = login;
    this.password = password;
    this.phoneNumber = phone;
    this.accountIdentifier = accId;
    this.type = type;
}

function Message(id, text, date, type, contactId, meta) {
    this.id = id;
    this.text = text;
    this.date = date;
    this.type = type;
    this.contactIdentifier = contactId;
    this.metaContact = meta;
}

function Contact(id, name, phone, contactId, type, meta) {
    this.id = id;
    this.name = name;
    this.contactIdentifier = contactId;
    this.type = type;
    this.metaContact = meta;
}

function MetaContact(name, id) {
    this.id = id;
    this.name = name;
    this.contacts = [];
    this.messages = [];
}