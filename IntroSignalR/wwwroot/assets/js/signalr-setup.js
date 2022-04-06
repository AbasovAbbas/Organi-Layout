//$.connection=null;

$(document).ready(function () {

    //$('#btnSend').click(function () {
    //    let msg = $('#chat-message').val();
    //    $.connection.invoke('SayHello', msg);
    //});
    $('#btnSend').click(function () {
        let msg = $('#chat-message').val();
        let mail = $('#chat-email').val();

        $.connection.invoke("SendMessage", mail, msg);
    });

    $('btnJoinGroup').click(function () {
        let group = $('chat-group').val();
        $.connection.invoke("JoinGroup", group);
    });
    /*gropa gonder buttonu olamlidir
        $.connection.invoke("SendGroup", "admins", "adminler burada")
    */
    $('#btnConnect').click(function () {
        let mail = $('#eMail').val();

        if (mail.length > 0) {

            $.connection = new signalR.HubConnectionBuilder()
                .configureLogging(signalR.LogLevel.None)
                .withUrl(`/chat?email=${mail}`).build(); 

            $.connection.on('PrintHello', function (message) {
                var mItem = `<a href="#" class="list-group-item list-group-item-action">
                                <div class="d-flex w-100 justify-content-between">
                                    <h5 class="mb-1">${name}</h5>
                                    <small>3 days ago</small>
                                </div>
                                <p class="mb-1">${message}</p>
                            </a>`;

                $('#messages').append($(mItem));
            });

            $.connection.start()
                .then(function () { console.log('###Connected'); })
                .catch(function (ex) { console.log(ex); });
        }
    });

});