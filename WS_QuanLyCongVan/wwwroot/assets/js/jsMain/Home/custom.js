var currentPage = 1; 
var hasMoreData = true;
$(document).ready(function () {
    getCountCvden()
    getCountCvdi()
    getCvdiOfCvden()
    getCountEmployess()
    getCountAccount()
    getPart()
    populateDays()
    loadStatistical()
    loadInitialData()
    $('.dialogs').on('scroll', function () {
        if ($(this).scrollTop() === 0 && hasMoreData) {
            loadMoreData();
        }
    });
    var connection = new signalR.HubConnectionBuilder().withUrl("/NotihubServer").build();
    connection.on("ReceiveMessage", function (message) {
        getCountCvden()
        getCountCvdi()
        getCvdiOfCvden()
        getCountEmployess()
        getCountAccount()
        getPart()
        populateDays()
        loadStatistical()
    });
    connection.start();

    var connectionChat = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
    
    connectionChat.start().catch(function (err) {
        console.error(err.toString());
    });

    $("#sendButton").click(function () {
        var message = $("#messageInput").val();
        $("#messageInput").val("");
        connectionChat.invoke("SendMessageToAll", message).catch(function (err) {
            console.error(err.toString());
        });
        

    });
    connectionChat.on("ReceiveMessage", function (user) {
        $("#lstMessage").append(
            `<div class="itemdiv dialogdiv">
            <div class="user">
                <img alt="Alexa's Avatar" src="data:image/*;base64,${user.hinh}">
            </div>
            <div class="body">
                <div class="time">
                    <i class="icon-time"></i>
                    <span class="green">${user.ngay}</span>
                </div>
                <div class="name">
                    <a href="#">${user.ten}</a>
                </div>
                <div class="text">${user.noidung}</div>
                <div class="tools">
                    <a href="#" class="btn btn-minier btn-info">
                        <i class="icon-only icon-share-alt"></i>
                    </a>
                </div>
            </div>
        </div>`
        );
        scrollToBottom()
    });
});
function getCountCvden() {
    $.ajax({
        type: "GET",
        url: "/home/getCVDEN",
        success: function (res) {
            if (res.data != 0) {
                $("#countCvden").text(res.data)
            } else {
                $("#countCvden").text(0)
            }
        }
    })
}
function getCountCvdi() {
    $.ajax({
        type: "GET",
        url: "/home/getCVDI",
        success: function (res) {
            if (res.data != 0) {
                $("#countCvdi").text(res.data)
            } else {
                $("#countCvdi").text(0)
            }
        }
    })
}
function getCvdiOfCvden() {
    $.ajax({
        type: "GET",
        url: "/home/getCvdiOfCvden",
        success: function (res) {
            if (res.dataCheck != 0 || res.dataNoCheck != 0) {
                $("#countCheck").text(res.dataCheck)
                $("#countNoCheck").text(res.dataNoCheck)
            } else {
                $("#countCheck").text(0)
                $("#countNoCheck").text(0)
            }
        }
    })
}
function getCountEmployess() {
    $.ajax({
        type: "GET",
        url: "/home/getEmployess",
        success: function (res) {
            if (res.data != 0) {
                $("#countEmployess").text(res.data)
            } else {
                $("#countCvdi").text(0)
            }
        }
    })
}
function getCountAccount() {
    $.ajax({
        type: "GET",
        url: "/home/getAccount",
        success: function (res) {
            if (res.data != 0) {
                $("#countAccount").text(res.data)
            } else {
                $("#countAccount").text(0)
            }
        }
    })
}
function getPart() {
    $('#partSelect').empty();
    $('#partSelect').append($('<option>', {
        value: 0,
        text: "All"
    }));
    $.ajax({
        type: "GET",
        url: "/home/getPart",
        success: function (res) {
            if (res.data != null) {
                for (var i = 0; i < res.data.length; i++) {
                    $('#partSelect').append($('<option>', {
                        value: res.data[i].id,
                        text: res.data[i].ten_BP
                    }));
                }
            }
        }
    })
}
//function getMessage() {
//    $.ajax({
//        type: "GET",
//        url: "/home/getMessage",
//        success: function (res) {
//            if (res.data != null) {
//                for (var i = 0; i < res.data.length; i++) {
//                    $("#lstMessage").append(
//                        `<div class="itemdiv dialogdiv">
//                            <div class="user">
//                                <img alt="Alexa's Avatar" src="data:image/*;base64,${res.data[i].hinh}">
//                            </div>
//                            <div class="body">
//                                <div class="time">
//                                    <i class="icon-time"></i>
//                                    <span class="green">${res.data[i].ngay}</span>
//                                </div>
//                                <div class="name">
//                                    <a href="#">${res.data[i].ten}</a>
//                                </div>
//                                <div class="text">${res.data[i].noidung}</div>
//                                <div class="tools">
//                                    <a href="#" class="btn btn-minier btn-info">
//                                        <i class="icon-only icon-share-alt"></i>
//                                    </a>
//                                </div>
//                            </div>
//                        </div>`
//                    );
//                }
//            }
//        }
//    })
//}
function scrollToBottom() {
    var dialogContainer = $('.dialogs')[0];
    dialogContainer.scrollTop = dialogContainer.scrollHeight;
}
// Hàm để hiển thị dữ liệu mới
function loadInitialData() {
    $.ajax({
        url: '/home/loadMoreMessages',
        method: 'GET',
        data: { page: currentPage },
        success: function (res) {
            $('#thongbao').empty(); 
            for (var i = 0; i < res.data.length; i++) {
                    $("#lstMessage").append(
                        `<div class="itemdiv dialogdiv">
                            <div class="user">
                                <img alt="Alexa's Avatar" src="data:image/*;base64,${res.data[i].hinh}">
                            </div>
                            <div class="body">
                                <div class="time">
                                    <i class="icon-time"></i>
                                    <span class="green">${res.data[i].ngay}</span>
                                </div>
                                <div class="name">
                                    <a href="#">${res.data[i].ten}</a>
                                </div>
                                <div class="text">${res.data[i].noidung}</div>
                                <div class="tools">
                                    <a href="#" class="btn btn-minier btn-info">
                                        <i class="icon-only icon-share-alt"></i>
                                    </a>
                                </div>
                            </div>
                        </div>`
                    );
            }
            // Kiểm tra nếu không còn dữ liệu
            if (res.data.length === 0) {
                hasMoreData = false;
            } else {
                scrollToBottom()
            }
        },
        error: function (error) {
            console.error('Error fetching initial data:', error);
        }
    });
}
// Hàm để tải thêm dữ liệu khi cuộn lên
function loadMoreData() {
    currentPage++;
    $.ajax({
        url: '/home/loadMoreMessages',
        method: 'GET',
        data: { page: currentPage },
        success: function (res) {
            $('#thongbao').empty(); 
            for (var i = 0; i < res.data.length; i++) {
                $("#lstMessage").append(
                    `<div class="itemdiv dialogdiv">
                            <div class="user">
                                <img alt="Alexa's Avatar" src="data:image/*;base64,${res.data[i].hinh}">
                            </div>
                            <div class="body">
                                <div class="time">
                                    <i class="icon-time"></i>
                                    <span class="green">${res.data[i].ngay}</span>
                                </div>
                                <div class="name">
                                    <a href="#">${res.data[i].ten}</a>
                                </div>
                                <div class="text">${res.data[i].noidung}</div>
                                <div class="tools">
                                    <a href="#" class="btn btn-minier btn-info">
                                        <i class="icon-only icon-share-alt"></i>
                                    </a>
                                </div>
                            </div>
                        </div>`
                );
            }
            // Kiểm tra nếu không còn dữ liệu
            if (res.data.length === 0) {
                hasMoreData = false;
            } else {
                scrollToBottom()
            }
        },
        error: function (error) {
            console.error('Error fetching more data:', error);
        }
    });
}






function loadStatistical() {
    var cv = $('#cvSelect').val();
    var day = $('#daySelect').val();
    var month = $('#monthSelect').val();
    var year = $('#yearSelect').val();
    var part = $('#partSelect').val();
    
    $.ajax({
        url: '/Home/Statistical',
        type: 'GET',
        data: {
            cv: cv,
            day: day,
            month: month,
            year: year,
            part: part
        },
        success: function (response) {
            if (response.success) {
                var combinedData = response.data;
                var combinedCheck = response.ckecked;
                var combinedNoCheck = response.noCkecked;

                // Tạo mảng chứa ngày và số lượng
                var labels = [];
                var counts = [];
                var countChecks = [];
                var countNoChecks = [];

                // Lặp qua dữ liệu kết hợp từ controller và điền vào mảng labels và counts
                for (var i = 0; i < combinedData.length; i++) {
                    labels.push(combinedData[i].date);
                    counts.push(combinedData[i].count);
                }
                for (var i = 0; i < combinedCheck.length; i++) {
                    countChecks.push(combinedCheck[i].count);
                }
                for (var i = 0; i < combinedNoCheck.length; i++) {
                    countNoChecks.push(combinedNoCheck[i].count);
                }

                // Vẽ biểu đồ sử dụng Chart.js
                var ctx = document.getElementById('myChart').getContext('2d');
                window.myChart = new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: labels,
                        datasets: [
                            {
                                label: 'Số lượng công văn',
                                data: counts,
                                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                                borderColor: 'rgba(75, 192, 192, 1)',
                                borderWidth: 1,
                            },
                            {
                                label: 'Đã duyệt',
                                data: countChecks,
                                backgroundColor: 'rgba(255, 99, 132, 0.2)',
                                borderColor: 'rgba(255, 99, 132, 1)',
                                borderWidth: 1,
                                hidden: true
                            },
                            {
                                label: 'Chưa duyệt',
                                data: countNoChecks,
                                backgroundColor: 'rgba(255, 206, 86, 0.2)',
                                borderColor: 'rgba(255, 206, 86, 1)',
                                borderWidth: 1,
                                hidden: true
                            }

                        ]
                    },
                    options: {
                        scales: {
                            y: {
                                beginAtZero: true
                            }
                        },
                        onClick: function (event, elements) {
                            if (elements.length > 0 && elements[0].index == 1) { // Nếu click vào nhãn thứ 2
                                var dataset = this.data.datasets[1];
                                dataset.hidden = !dataset.hidden; // Đảo ngược trạng thái ẩn/hiện
                                this.update(); // Cập nhật biểu đồ
                            }
                        }
                    }
                });
            }
            else {
                $.notify("Hãy chọn ngày tháng năm cho hợp lý.", { globalPosition: 'top right', className: "error" });
            }


        },
        error: function (xhr, status, error) {
            // Xử lý lỗi nếu có
        }
    });
}
function Statistical() {
    var cv = $('#cvSelect').val();
    var day = $('#daySelect').val();
    var month = $('#monthSelect').val();
    var year = $('#yearSelect').val();
    var part = $('#partSelect').val();
    $.ajax({
        url: '/Home/Statistical',
        type: 'GET',
        data: {
            cv: cv,
            day: day,
            month: month,
            year: year,
            part: part
        },
        success: function (response) {
            if (response.success) {
                if (window.myChart !== undefined && window.myChart !== null) {
                    window.myChart.destroy();
                }
                var combinedData = response.data;
                var combinedCheck = response.ckecked;
                var combinedNoCheck = response.noCkecked;

                // Tạo mảng chứa ngày và số lượng
                var labels = [];
                var counts = [];
                var countChecks = [];
                var countNoChecks = [];

                // Lặp qua dữ liệu kết hợp từ controller và điền vào mảng labels và counts
                for (var i = 0; i < combinedData.length; i++) {
                    labels.push(combinedData[i].date);
                    counts.push(combinedData[i].count);
                }
                for (var i = 0; i < combinedCheck.length; i++) {
                    countChecks.push(combinedCheck[i].count);
                }
                for (var i = 0; i < combinedNoCheck.length; i++) {
                    countNoChecks.push(combinedNoCheck[i].count);
                }

                // Vẽ biểu đồ sử dụng Chart.js
                var ctx = document.getElementById('myChart').getContext('2d');
                window.myChart = new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: labels,
                        datasets: [
                            {
                                label: 'Số lượng công văn',
                                data: counts,
                                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                                borderColor: 'rgba(75, 192, 192, 1)',
                                borderWidth: 1,
                            },
                            {
                                label: 'Đã duyệt',
                                data: countChecks,
                                backgroundColor: 'rgba(255, 99, 132, 0.2)',
                                borderColor: 'rgba(255, 99, 132, 1)',
                                borderWidth: 1,
                                hidden: true
                            },
                            {
                                label: 'Chưa duyệt',
                                data: countNoChecks,
                                backgroundColor: 'rgba(255, 206, 86, 0.2)',
                                borderColor: 'rgba(255, 206, 86, 1)',
                                borderWidth: 1,
                                hidden: true
                            }

                        ]
                    },
                    options: {
                        scales: {
                            y: {
                                beginAtZero: true
                            }
                        },
                        onClick: function (event, elements) {
                            if (elements.length > 0 && elements[0].index == 1) { // Nếu click vào nhãn thứ 2
                                var dataset = this.data.datasets[1];
                                dataset.hidden = !dataset.hidden; // Đảo ngược trạng thái ẩn/hiện
                                this.update(); // Cập nhật biểu đồ
                            }
                        }
                    }
                });
            }
            else {
                $.notify("Hãy chọn ngày tháng năm cho hợp lý.", { globalPosition: 'top right', className: "error" });
            }
            
            
        },
        error: function (xhr, status, error) {
            // Xử lý lỗi nếu có
        }
    });
}

// Hàm để thêm các tuỳ chọn ngày vào dropdownlist
function populateDays() {
    var month = $('#monthSelect').val();
    var year = $('#yearSelect').val();
    var daysInMonth = new Date(year, month, 0).getDate();

    $('#daySelect').empty(); // Xóa các tuỳ chọn cũ
    $('#daySelect').append($('<option>', {
        value: 0,
        text: "All"
    }));
    for (var i = 1; i <= daysInMonth; i++) {
        $('#daySelect').append($('<option>', {
            value: i,
            text: i
        }));
    }
}
// Hàm để thêm các tuỳ chọn năm vào dropdownlist
function populateYears() {
    var currentYear = new Date().getFullYear();

    $('#yearSelect').empty(); // Xóa các tuỳ chọn cũ
    $('#yearSelect').append($('<option>', {
        value: 0,
        text: "All"
    }));
    for (var i = currentYear - 10; i <= currentYear + 10; i++) {
        $('#yearSelect').append($('<option>', {
            value: i,
            text: i
        }));
    }
}
// Gọi các hàm khi thay đổi tháng hoặc năm
$('#monthSelect').change(populateDays);
$('#yearSelect').change(populateDays);
// Khởi tạo dropdownlist cho ngày và năm khi trang được tải
populateDays();
populateYears();

