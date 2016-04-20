function XSocketsChat() {
    AbstractChat.call(this);

    // Connect and identify the XSockets controller to connect to: `chat`
    var conn = new XSockets.WebSocket('ws://localhost:4502', ['chat']);

    // Get a reference to the controller
    this.controller = conn.controller('chat');

    //Setup a subscription for the topic "chatmessage"
    this.controller.subscribe('chatmessage', this.addMessage.bind(this));

    this.fetchInitialMessages();
}
extend(XSocketsChat).with(AbstractChat);

XSocketsChat.prototype.fetchInitialMessages = function () {
    // RMI - returns Promise
    var getAll = this.controller.invoke('getallmessages')

    getAll.then(function (messages) {

        // Loop through existing messages
        messages.forEach(function (message) {

            // Add to the UI
            this.addMessage(message);

        }, this);
    }.bind(this));
};

/**
 * Send message to the XSockets controller.
 */
XSocketsChat.prototype.sendMessage = function (data) {
    this.controller.publish('chatmessage', data, this.sendMessageSuccess.bind(this));
};