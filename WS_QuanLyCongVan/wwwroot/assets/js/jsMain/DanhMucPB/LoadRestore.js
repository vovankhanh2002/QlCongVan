$(document).ready(function () {
    Load()
    $('#selectAllCheckboxRes').on('click', function () {
        if (this.checked) {
            $('.rowCheckboxRes').prop('checked', true);
        } else {
            $('.rowCheckboxRes').prop('checked', false);
        }
        toggleDeleteButtonRes()
    });
    $('#restore').on('click', function () {
        var checkedData = [];
        $('.rowCheckboxRes:checked').each(function () {
            checkedData.push($(this).val());
        });
        if (checkedData.length > 0) {
            $.ajax({
                url: $(this).data('request-url'),
                type: 'POST',
                data: { lst: checkedData },
                success: function (res) {
                    if (res.isValue) {
                        $.notify(res.notify, { globalPosition: 'top right', className: "success" });
                        $('#selectAllCheckboxRes').prop('checked', false);
                        location.reload();
                        toggleDeleteButtonRes()
                    }
                },
                error: function (xhr, status, error) {
                    console.error('Error sending data:', error);
                }
            });
        } else {
            $.notify("Bạn cần chọn để phục hồi", { globalPosition: 'top right', className: "error" });
        }

    });
    $('#deleteres').on('click', function () {
        if (confirm("Bạn muốn tiếp tục không ?")) {
            var checkedData = [];
            $('.rowCheckboxRes:checked').each(function () {
                checkedData.push($(this).val());
            });
            if (checkedData.length > 0) {
                $.ajax({
                    url: $(this).data('request-url'),
                    type: 'POST',
                    data: { lst: checkedData },
                    success: function (res) {
                        if (res.isValue) {
                            $.notify(res.notify, { globalPosition: 'top right', className: "success" });
                            $('#selectAllCheckboxRes').prop('checked', false);
                            location.reload();
                            toggleDeleteButtonRes()
                        }
                    },
                    error: function (xhr, status, error) {
                        console.error('Error sending data:', error);
                    }
                });
            } else {
                $.notify("Bạn cần chọn để phục hồi", { globalPosition: 'top right', className: "error" });
            }

        }
    });
})

function Load() {
    phongban_res()
}
function phongban_res() {
    $('#phongbanrestore').dataTable({
        "serverSide": true,
        "filter": true,
        "processing": true,
        "ajax": {
            "url": "/phongban/getRestore",
            "type": "Post",
            "datatype": "json"
        },
        "columnDefs": [
            {
                "searchable": false,
                "orderable": false,
                "targets": [0],
                "defaultContent": "-"
                /*"visible": false,*/
            }
        ],
        "columns": [
            {
                "data": "id",
                "render": function (data, row) {
                    return `
                            <input type="checkbox" class="rowCheckboxRes" value="${data}" ></input>
                           `
                },
            },
            { "data": "ten_PB", "name": "ten_PB", "autowidth": true },
            { "data": "ghiChu", "name": "ghiChu", "autowidth": true }
        ],

        stateSave: true,
        "bDestroy": true
    })

    $('#phongbanrestore tbody').on('click', '.rowCheckboxRes', function () {
        if ($('.rowCheckboxRes:checked').length === $('.rowCheckboxRes').length) {
            $('#selectAllCheckboxRes').prop('checked', true);
        } else {
            $('#selectAllCheckboxRes').prop('checked', false);
        }
        toggleDeleteButtonRes()
    })

}

function toggleDeleteButtonRes() {
    if ($('.rowCheckboxRes:checked').length > 0) {
        $('#restore').prop('disabled', false);
        $('#deleteres').prop('disabled', false);
    } else {
        $('#restore').prop('disabled', true);
        $('#deleteres').prop('disabled', true);

    }
}