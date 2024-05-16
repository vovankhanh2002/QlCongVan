$(document).ready(function () {
    Thongbao()
    var connection = new signalR.HubConnectionBuilder().withUrl("/NotihubServer").build();
    connection.on("ReceiveMessage", function (message) {
        Thongbao()
    });
    connection.start();
    
})
function Thongbao() {
    $.ajax({
        url: "/thongbao/index",
        type: 'GET',
        success: function (res) {
            if (res.data != null) {
                var thongbaoList = res.data;
                $('#thongbao').empty(); 
                thongbaoList.forEach(function (item) {
                    if (item.trangthai) {
                        var newItem = $('<li>').addClass('notification');
                        newItem.attr('data-id', item.id);
                        var link = $('<a>').attr('href', '#');

                        var img = $('<img>').addClass('msg-photo').attr('src', 'data:image/*;base64,' + item.hinh).attr('alt', 'Avatar');

                        var msgBody = $('<span>').addClass('msg-body');

                        var msgTitle = $('<span>').addClass('msg-title').append(
                            $('<span>').addClass('blue').text(item.name),
                        );
                        var msgNoidung = $('<span>').addClass('msg-title').append(
                            item.noidung
                        );
                        var msgTime = $('<span>').addClass('msg-time').append(
                            $('<i>').addClass('icon-time'),
                            $('<span>').text(item.thoigian)
                        );
                        var deleteButton = $('<span>').addClass('delete-button').append(
                            $('<i>').addClass('icon-remove'),
                        );
                        msgBody.append(msgTitle, msgNoidung, msgTime);
                        link.append(img, msgBody, deleteButton);
                        newItem.append(link);

                        newItem.click(function () {
                            var id = item.id;
                            toggleNotificationStatus(id);
                            event.stopPropagation();
                        });
                        deleteButton.click(function (event) {
                            var id = item.id;
                            deleteNotification(id);
                            event.stopPropagation();
                        });
                        $('#thongbao').append(newItem);
                        $('#thongbao li').addClass('seen');
                    }
                    else {
                        var newItem = $('<li>').addClass('notification');
                        newItem.attr('data-id', item.id);
                        var link = $('<a>').attr('href', '#');

                        var img = $('<img>').addClass('msg-photo').attr('src', 'data:image/*;base64,' + item.hinh).attr('alt', 'Avatar');

                        var msgBody = $('<span>').addClass('msg-body');

                        var msgTitle = $('<span>').addClass('msg-title').append(
                            $('<span>').addClass('blue').text(item.name),
                            $('<i>').addClass('message-star icon-star orange2')
                        );
                        var msgNoidung = $('<span>').addClass('msg-title').append(
                            item.noidung
                        );
                        var msgTime = $('<span>').addClass('msg-time').append(
                            $('<i>').addClass('icon-time'),
                            $('<span>').text(item.thoigian)
                        );
                        var deleteButton = $('<span>').addClass('delete-button').append(
                            $('<i>').addClass('icon-remove'),
                        );
                        msgBody.append(msgTitle, msgNoidung, msgTime);
                        link.append(img, msgBody, deleteButton);
                        newItem.append(link);

                        newItem.click(function () {
                            var id = item.id;
                            toggleNotificationStatus(id);
                            event.stopPropagation();
                        });
                        deleteButton.click(function (event) {
                            var id = item.id;
                            deleteNotification(id);
                            event.stopPropagation();
                        });
                        $('#thongbao').append(newItem);
                        $('#thongbao li').addClass('unseen');
                        countNotification()

                    }
                });
                $('#thongbao').append('<li></li > ');
            }
        },
        error: function (xhr, status, error) {
            console.error('Error sending data:', error);
        }
    });
}

function toggleNotificationStatus(id) {
    $.ajax({
        url: "/thongbao/Edit",
        type: 'POST',
        data: { id: id },
        success: function (res) {
            if (res.status) {
                if (res.statusCV) {
                    $('#thongbao li[data-id="' + id + '"]').removeClass('unseen').addClass('seen');
                    $('#thongbao li[data-id="' + id + '"]').find('.msg-title i.message-star').remove();
                    countNotification();
                    window.location.href = $('#requestCVDEN').data('request-url')
                } else {
                    $('#thongbao li[data-id="' + id + '"]').removeClass('unseen').addClass('seen');
                    $('#thongbao li[data-id="' + id + '"]').find('.msg-title i.message-star').remove();
                    countNotification();
                    window.location.href = $('#requestCVDI').data('request-url')
                }
            } 
        },
        error: function (xhr, status, error) {
            console.error('Error updating notification status:', error);
        }
    });
}
function deleteNotification(id) {
    $.ajax({
        url: "/thongbao/delete",
        type: 'POST',
        data: { id: id },
        success: function (res) {
            if (res.status) {
                $('#thongbao li[data-id="' + id + '"]').remove();
                countNotification();
            }
        },
        error: function (xhr, status, error) {
            console.error('Error deleting notification:', error);
        }
    });
}

function countNotification() {
    var unseenCount = $('#thongbao li.unseen').length;
    $('#cntThongbao').text(unseenCount)
}