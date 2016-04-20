function PusherChat(key) {
    AbstractChat.call(this);

    // Connect
    var pusher = new Pusher(key);

    // Subscribe to `chat` channel
    this.channel = pusher.subscribe('chat');

    // Bind to subscript succeeded and upon success fetch initial set of messages
    this.channel.bind('pusher:subscription_succeeded', this.fetchInitialMessages.bind(this));

    // Bind to new chat message events
    this.channel.bind('chatmessage', this._messageReceived.bind(this));
}
extend(PusherChat).with(AbstractChat);

PusherChat.prototype.fetchInitialMessages = function () {
    // Standard AJAX GET to get initial messages
    $.get('/Home/Messages', function (messages) {

        // Loop and add to UI
        messages.forEach(function (message) {
            this._messageReceived(message);
        }, this);

    }.bind(this));
};

// Make HTTP POST request to controller
PusherChat.prototype.sendMessage = function (data) {
    $.post('/Home/PusherMessage', data)
        .success(this.sendMessageSuccess.bind(this));
};

PusherChat.prototype._messageReceived = function (data) {
    // Work around strange DateTime serialisation
    // http://stackoverflow.com/a/726869/39904
    if (data.created.indexOf('/Date(') !== -1) {
        data.created = parseInt(data.created.replace("/Date(", "").replace(")/", ""), 10);
    }
    this.addMessage(data);
};