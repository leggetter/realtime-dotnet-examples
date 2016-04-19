function SignalRChat() {
    AbstractChat.call(this);

    this.chatHub = $.connection.chatHub;
    this.chatHub.client.newMessage = this._addMessage.bind(this);

    $.connection.hub.start().done(function () {
        this.fetchInitialMessages();
    }.bind(this));
}
extend(SignalRChat).with(AbstractChat);

/**
 * Get all existing messages.
 */
SignalRChat.prototype.fetchInitialMessages = function () {
    var getAll = this.chatHub.server.getAll();
    getAll.then(function (messages) {
        messages.forEach(function (message) {
            this._addMessage(message);
        }, this);
    }.bind(this)).fail(function(e) {
        console.error(e);
    });
};

SignalRChat.prototype._addMessage = function (message) {
    this.addMessage(message);
};

SignalRChat.prototype.sendMessage = function (data) {
    this.chatHub.server.send(data.username, data.text);
};