function SignalRChat() {
    AbstractChat.call(this);

    // A reference to the SignalR Server ChatHub
    // This is a dynamically created proxy object.
    this.chatHub = $.connection.chatHub;

    // Add a `newMessage` function to the client part of the hub
    // This defines this function and means the server can call
    // it on the client.
    this.chatHub.client.newMessage = this.addMessage.bind(this);

    // Start the connection to the hub.
    $.connection.hub.start().done(function () {

        // when succeeded, fetch any stored messages.
        this.fetchInitialMessages();

    }.bind(this));
}
extend(SignalRChat).with(AbstractChat);

/**
 * Get all existing messages.
 */
SignalRChat.prototype.fetchInitialMessages = function () {
    // RMI - returns Promise
    var getAll = this.chatHub.server.getAll();

    getAll.then(function (messages) {

        // Loop through existing messages
        messages.forEach(function (message) {

            // Add to the UI
            this.addMessage(message);

        }, this);
    }.bind(this)).fail(function(e) {
        console.error(e);
    });
};

/**
 * Send message over SignalR.
 */
SignalRChat.prototype.sendMessage = function (data) {
    var self = this;
    self.chatHub.server.send(data.username, data.text)
        .done(self.sendMessageSuccess)
        .fail(function(error) {
            console.error(error);
        });
};