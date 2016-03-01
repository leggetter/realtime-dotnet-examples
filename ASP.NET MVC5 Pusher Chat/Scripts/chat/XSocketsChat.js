function XSocketsChat() {
    AbstractChat.call(this);

    XSocketsChat.prototype._messageReceived = function (data) {
        this.addMessage(data);
    };

    XSocketsChat.prototype.sendMessage = function (data) {
        //Only send the text part, xsockets knows about the username already
        this.controller.publish('chatmessage', { text: data.text }, this.sendMessageSuccess.bind(this));
    };

    var conn = new XSockets.WebSocket('ws://localhost:4502', ['chat'], {username:this.twitterUsername});
    this.controller = conn.controller('chat');

    //When the controller sends out all persisted messages
    this.controller.on('allmessages', function (messages) {
        messages.forEach(function (message) {
            XSocketsChat.prototype._messageReceived(message);
        }.bind(this))
    });

    //Setup a handler for the topic "newmessage"
    this.controller.on('newmessage', this._messageReceived.bind(this));
}
extend(XSocketsChat).with(AbstractChat);


