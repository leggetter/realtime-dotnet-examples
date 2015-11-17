function SignalRChat() {
    AbstractChat.call(this);

    this.hub = $.connection.chatHub;
    this.hub.client.newMessage = this._addMessage.bind(this);

    $.connection.hub.start().done(function () {
        this.fetchInitialMessages();
    }.bind(this));
}
extend(SignalRChat).with(AbstractChat);

/**
 * Get all existing messages.
 */
SignalRChat.prototype.fetchInitialMessages = function () {
    var getAll = this.hub.server.getAll();
    getAll.then(function (messages) {
        messages.forEach(function (message) {
            this._addMessage(message);
        }, this);
    }.bind(this)).fail(function(e) {
        console.error(e);
    });
};

/**
 * Check to ensure a 3 char message has been input.
 * If so, send the message to the server.
 */
SignalRChat.prototype.sendMessage = function (data) {
    this.hub.server.send(data.username, data.chat_text);
};

SignalRChat.prototype._addMessage = function (message) {
    this.addMessage(message);
};