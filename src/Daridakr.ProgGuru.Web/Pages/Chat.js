$(function() {
    var connection = new signalR.HubConnectionBuilder().withUrl("/signalr-hubs/chat").build();

    connection.on("ReceiveMessage", function (message) {
        /*$('#MessageList').append('<li><strong><i class="fas fa-long-arrow-alt-right"></i> ' + message + '</strong></li>');*/
        $('#MessageList').append('<p class="blockquote-footer" style="margin-bottom: 0 !important"> ' + message + '</p>');
    });

    connection.start().then(function () {

    }).catch(function (err) {
        return console.error(err.toString());
    });
    
    $('#SendMessageButton').click(function(e) {
        e.preventDefault();

        var targetUserName = $('#TargetUser').val();
        var message = $('#Message').val();
        $('#Message').val('');

        connection.invoke("SendMessage", targetUserName, message)
            .then(function() {
                $('#MessageList')
                    .append('<p class="blockquote-footer" style="margin-bottom: 0 !important"> ' + abp.currentUser.userName + ': ' + message + '</p>');
            })
            .catch(function(err) {
                return console.error(err.toString());
            });
    });
});