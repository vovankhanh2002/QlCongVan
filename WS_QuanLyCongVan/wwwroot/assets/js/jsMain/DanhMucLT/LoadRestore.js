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
        request = $(this).data('request-url')
        var checkedData = [];
        $('.rowCheckboxRes:checked').each(function () {
            checkedData.push($(this).val());
        });
        bootbox.dialog({
            message: "<span class='bigger-110'>Bạn có muốn tiếp tục?</span>",
            buttons:
            {
                "success":
                {
                    "label": "<i class='icon-ok'></i> Đồng ý",
                    "className": "btn-sm btn-success",
                    "callback": function () {
                        $.ajax({
                            url: request,
                            type: 'POST',
                            data: { lst: checkedData },
                            success: function (res) {
                                if (res.isValue) {
                                    $.notify(res.notify, { globalPosition: 'top right', className: "success" });
                                    $('#selectAllCheckbox').prop('checked', false);
                                    $('#delete').prop('disabled', true);
                                    Load()
                                }
                            },
                            error: function (xhr, status, error) {
                                console.error('Error sending data:', error);
                            }
                        });
                    }
                },
                "button":
                {
                    "label": "Đóng",
                    "className": "btn-sm"
                }
            }
        });

    });
})

function Load() {
    kho_res()
    ke_res()
    hop_res()
}
function kho_res() {
    $('#khorestore').dataTable({
        "serverSide": true,
        "filter": true,
        "processing": true,
        "language": {
            "info": "Bắt đầu _START_ kết thúc _END_ số lượng _TOTAL_ bảng ghi",
            "search": "Tìm kiếm",
            "loadingRecords": "Đang tải...",
            "emptyTable": "Không bảng ghi"
        },
        "ajax": {
            "url": "/Kho/getRestore",
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
            { "data": "ten_Kho", "name": "ten_Kho", "autowidth": true },
            { "data": "ghiChu", "name": "ghiChu", "autowidth": true }
        ],

        stateSave: true,
        "bDestroy": true
    })

    $('#khorestore tbody').on('click', '.rowCheckboxRes', function () {
        if ($('.rowCheckboxRes:checked').length === $('.rowCheckboxRes').length) {
            $('#selectAllCheckboxRes').prop('checked', true);
        } else {
            $('#selectAllCheckboxRes').prop('checked', false);
        }
        toggleDeleteButtonRes()
    })

}
function ke_res() {
    $('#kerestore').dataTable({
        "serverSide": true,
        "filter": true,
        "processing": true,
        "language": {
            "info": "Bắt đầu _START_ kết thúc _END_ số lượng _TOTAL_ bảng ghi",
            "search": "Tìm kiếm",
            "loadingRecords": "Đang tải...",
            "emptyTable": "Không bảng ghi"
        },
        "ajax": {
            "url": "/Ke/getRestore",
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
            { "data": "ten_Ke", "name": "ten_Kho", "autowidth": true },
            { "data": "ghiChu", "name": "ghiChu", "autowidth": true }
        ],

        stateSave: true,
        "bDestroy": true
    })

    $('#kerestore tbody').on('click', '.rowCheckboxRes', function () {
        if ($('.rowCheckboxRes:checked').length === $('.rowCheckboxRes').length) {
            $('#selectAllCheckboxRes').prop('checked', true);
        } else {
            $('#selectAllCheckboxRes').prop('checked', false);
        }
        toggleDeleteButtonRes()
    })

}
function hop_res() {
    $('#hoprestore').dataTable({
        "serverSide": true,
        "filter": true,
        "processing": true,
        "language": {
            "info": "Bắt đầu _START_ kết thúc _END_ số lượng _TOTAL_ bảng ghi",
            "search": "Tìm kiếm",
            "loadingRecords": "Đang tải...",
            "emptyTable": "Không bảng ghi"
        },
        "ajax": {
            "url": "/hop/getRestore",
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
            { "data": "ten_Hop", "name": "ten_Ke", "autowidth": true },
            { "data": "tb_Kho.ten_Kho", "name": "tb_Kho.ten_Kho", "autowidth": true },
            { "data": "tb_Ke.ten_Ke", "name": "tb_Ke.ten_Ke", "autowidth": true },
            { "data": "ghiChu", "name": "ghiChu", "autowidth": true }
        ],

        stateSave: true,
        "bDestroy": true
    })

    $('#hoprestore tbody').on('click', '.rowCheckboxRes', function () {
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