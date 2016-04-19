// Log all Pusher JS info to console
// Pusher.log = function(msg) {
//   console.log(msg);
// };

var extend = function (child) {
    return {
        'with': function (parent) {
            child.prototype = Object.create(parent.prototype);
        }
    }
};

function AbstractChat() {
    this.storage = window.localStorage || { setItem: function () { }, getItem: function () { } };

    // Store Twitter ID entered by User.
    this.twitterUsername = this.storage.getItem('username');

    if (this.twitterUsername) {
        this.setUpUser(this.twitterUsername);
    }

    // Twitter username button click and Enter press handler setup
    $('#try-it-out').click(function (e) {
        var username = $('#input-name').val();
        this.setUpUser(username);
    }.bind(this));
    $('#input-name').keyup(function (e) {
        if (e.keyCode === 13) {
            var username = $('#input-name').val();
            this.setUpUser(username);
        }
    }.bind(this));

    // send button click and Enter keypress handling
    $('.send-message').click(this._sendMessage.bind(this));
    $('.input-message').keypress(this.checkSend.bind(this));
}

/**
 * Get all existing messages.
 */
AbstractChat.prototype.fetchInitialMessages = function () {
    $.get('/chat-api/messages').success(function (messages) {
        messages.forEach(this.addMessage.bind(this));
    }.bind(this));
};

/**
 * Handle user entered events. Ensure there's a value
 * and store for use later.
 *
 * Also hide Twitter username input and show messages.
 */
AbstractChat.prototype.setUpUser = function(username) {
    if (!username) {
        return;
    }

    this.twitterUsername = username;
    this.storage.setItem('username', this.twitterUsername);

    $('.twitter-username-capture').slideUp(function () {
        $('.chat-app').fadeIn();
        this.scrollMessagesToBottom();
    }.bind(this));
}

/**
 * Check to see if the Enter key has been pressed to
 * send a message.
 */
AbstractChat.prototype.checkSend = function(e) {
    if (e.keyCode === 13) {
        return this._sendMessage();
    }
}

/**
 * Check to ensure a 3 char message has been input.
 * If so, send the message to the server.
 */
AbstractChat.prototype._sendMessage = function () {
    var messageText = $('.input-message').val();
    if (messageText.length < 3) {
        return false;
    }

    // Build POST data and make AJAX request
    var data = {
        username: this.twitterUsername,
        text: messageText
    };
    this.sendMessage(data);

    // Ensure the normal browser event doesn't take place
    return false;
};

/**
 * Handle the message post success callback
 */
AbstractChat.prototype.sendMessageSuccess = function () {
    $('.input-message').val('')
    console.log('message sent successfully');
};

/**
 * Build the UI for a new message and add to the DOM
 */
AbstractChat.prototype.addMessage = function (data) {
    // Create element from template and set values
    var el = this.createMessageEl();
    el.find('.message-body').html(data.text);
    el.find('.author').text(data.username);
    el.find('.avatar img').attr('src', 'https://twitter.com/' + data.username + '/profile_image?size=original')

    // Utility to build nicely formatted time
    el.find('.timestamp').text(strftime('%H:%M:%S %P', new Date(data.created)));

    var messages = $('#messages');
    messages.append(el)

    this.scrollMessagesToBottom();
};

/**
 * Make sure the incoming message is shown.
 */
AbstractChat.prototype.scrollMessagesToBottom = function() {
    var messages = $('#messages');
    messages.scrollTop(messages[0].scrollHeight);
}

/**
 * Creates a chat message element from the template
 */
AbstractChat.prototype.createMessageEl = function () {
    var text = $('#chat_message_template').text();
    var el = $(text);
    return el;
};
