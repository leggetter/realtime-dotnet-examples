function XSocketsChat() {
    AbstractChat.call(this);

    var conn = new XSockets.WebSocket('ws://localhost:4502', ['chat']);
    this.controller = conn.controller('chat');

    //Setup a subscription for the topic "chatmessage"
    this.controller.subscribe('chatmessage', this._messageReceived.bind(this));

    this.fetchInitialMessages();
}
extend(XSocketsChat).with(AbstractChat);

XSocketsChat.prototype.fetchInitialMessages = function () {
    this.controller.invoke('getallmessages')
        .then(function (messages) {
            messages.forEach(function (message) {
                this._messageReceived(message);
            }.bind(this));
        }.bind(this));
};

XSocketsChat.prototype._messageReceived = function (data) {
    this.addMessage(data);
};

XSocketsChat.prototype.sendMessage = function (data) {
    this.controller.publish('chatmessage', data, this.sendMessageSuccess.bind(this));
};