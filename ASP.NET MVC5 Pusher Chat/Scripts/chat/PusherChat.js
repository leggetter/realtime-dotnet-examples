function PusherChat(key) {
    AbstractChat.call(this);

    var pusher = new Pusher(key);
    this.channel = pusher.subscribe('chat');

    this.channel.bind('pusher:subscription_succeeded', this.fetchInitialMessages.bind(this));
    this.channel.bind('chatmessage', this._messageReceived.bind(this));
}
extend(PusherChat).with(AbstractChat);

PusherChat.prototype.fetchInitialMessages = function () {
    $.get('/Home/Messages', function (messages) {
        messages.forEach(function (message) {
            this._messageReceived(message);
        }, this);
    }.bind(this));
};

PusherChat.prototype.sendMessage = function (data) {
    $.post('/Home/PusherMessage', data)
        .success(this.sendMessageSuccess.bind(this));
};

PusherChat.prototype._messageReceived = function (data) {
    if (data.created.indexOf('/Date(') !== -1) {
        data.created = parseInt(data.created.replace("/Date(", "").replace(")/", ""), 10);
    }
    this.addMessage(data);
};