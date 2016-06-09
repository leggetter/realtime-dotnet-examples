function PubNubChat(publishKey, subscribeKey) {
    AbstractChat.call(this);

    // Connect
    var pubnub = PUBNUB({
        publish_key: publishKey,
        subscribe_key: subscribeKey
    });

    // Subscribe to `chat` channel
    pubnub.subscribe({
        channel: 'chat',
        message: this._messageReceived.bind(this)
    });

    // Bind to subscript succeeded and upon success fetch initial set of messages
    this.fetchInitialMessages();
}
extend(PubNubChat).with(AbstractChat);

PubNubChat.prototype.fetchInitialMessages = function () {
    // Standard AJAX GET to get initial messages
    $.get('/Chat/Messages', function (messages) {

        // Loop and add to UI
        messages.forEach(function (message) {
            this._messageReceived(message);
        }, this);

    }.bind(this));
};

// Make HTTP POST request to controller
PubNubChat.prototype.sendMessage = function (data) {
    $.post('/Chat/PubNubMessage', data)
        .success(this.sendMessageSuccess.bind(this));
};

PubNubChat.prototype._messageReceived = function (data) {
    // TODO: check if the username

    // Work around strange DateTime serialisation
    // http://stackoverflow.com/a/726869/39904
    if (data.created.indexOf('/Date(') !== -1) {
        data.created = parseInt(data.created.replace("/Date(", "").replace(")/", ""), 10);
    }
    this.addMessage(data);
};