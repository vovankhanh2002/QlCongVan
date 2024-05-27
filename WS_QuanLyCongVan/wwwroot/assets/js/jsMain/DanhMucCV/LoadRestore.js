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
    loaicv_res()
    loaisocv_res()
    socv_res()
    linhvuc_res()
    tinhkhan_res()
    tinhmat_res()
}
function loaisocv_res() {
    $('#loaisocvrestore').dataTable({
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
            "url": "/LoaisoCV/getRestore",
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
            { "data": "ten_LSCV", "name": "ten_LSCV", "autowidth": true },
            { "data": "ghiChu", "name": "ghiChu", "autowidth": true },
        ],

        stateSave: true,
        "bDestroy": true
    })

    $('#loaisocvrestore tbody').on('click', '.rowCheckboxRes', function () {
        if ($('.rowCheckboxRes:checked').length === $('.rowCheckboxRes').length) {
            $('#selectAllCheckboxRes').prop('checked', true);
        } else {
            $('#selectAllCheckboxRes').prop('checked', false);
        }
        toggleDeleteButtonRes()
    })

}
function loaicv_res() {
    $('#loaicvrestore').dataTable({
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
            "url": "/LoaiCV/getRestore",
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
            { "data": "ten_LVB", "name": "ten_LVB", "autowidth": true },
            { "data": "ghiChu", "name": "ghiChu", "autowidth": true },
        ],

        stateSave: true,
        "bDestroy": true
    })
    
    $('#loaicvrestore tbody').on('click', '.rowCheckboxRes', function () {
        if ($('.rowCheckboxRes:checked').length === $('.rowCheckboxRes').length) {
            $('#selectAllCheckboxRes').prop('checked', true);
        } else {
            $('#selectAllCheckboxRes').prop('checked', false);
        }
        toggleDeleteButtonRes()
    })
    
}
function socv_res() {
    $('#socvrestore').dataTable({
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
            "url": "/socv/getRestore",
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
            { "data": "ten_SoCV", "name": "ten_SoCV", "autowidth": true },
            { "data": "ngay_SoCV", "name": "Ngay_SoCV", "autowidth": true },
            { "data": "tb_LoaiSoCV.ten_LSCV","name": "tb_loaisocv.Ten_LSCV", "autowidth": true},
            {
                "data": "trangThai", "name": "TrangThai",
                "render": function (data) {
                    if (data) {
                        return `<span class="label label-success arrowed">True</span>`
                    } else {
                        return `<span class="label label-danger arrowed-in">False</span>`
                    }
                },
                "autowidth": true
            },
            { "data": "ghichu", "name": "ghichu", "autowidth": true }
        ],

        stateSave: true,
        "bDestroy": true
    })

    $('#socvrestore tbody').on('click', '.rowCheckboxRes', function () {
        if ($('.rowCheckboxRes:checked').length === $('.rowCheckboxRes').length) {
            $('#selectAllCheckboxRes').prop('checked', true);
        } else {
            $('#selectAllCheckboxRes').prop('checked', false);
        }
        toggleDeleteButtonRes()
    })

}
function linhvuc_res() {
    $('#linhvucrestore').dataTable({
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
            "url": "/linhvuc/getRestore",
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
            { "data": "ten_LV", "name": "ten_LV", "autowidth": true },
            { "data": "ghichu", "name": "ghichu", "autowidth": true }
        ],

        stateSave: true,
        "bDestroy": true
    })

    $('#linhvucrestore tbody').on('click', '.rowCheckboxRes', function () {
        if ($('.rowCheckboxRes:checked').length === $('.rowCheckboxRes').length) {
            $('#selectAllCheckboxRes').prop('checked', true);
        } else {
            $('#selectAllCheckboxRes').prop('checked', false);
        }
        toggleDeleteButtonRes()
    })

}
function tinhkhan_res() {
    $('#tinhkhanrestore').dataTable({
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
            "url": "/tinhkhan/getRestore",
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
            { "data": "ten_MDKhan", "name": "ten_MDKhan", "autowidth": true },
            { "data": "ghiChu", "name": "ghiChu", "autowidth": true }
        ],

        stateSave: true,
        "bDestroy": true
    })

    $('#tinhkhanrestore tbody').on('click', '.rowCheckboxRes', function () {
        if ($('.rowCheckboxRes:checked').length === $('.rowCheckboxRes').length) {
            $('#selectAllCheckboxRes').prop('checked', true);
        } else {
            $('#selectAllCheckboxRes').prop('checked', false);
        }
        toggleDeleteButtonRes()
    })

}
function tinhmat_res() {
    $('#tinhmatrestore').dataTable({
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
            "url": "/tinhmat/getRestore",
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
            { "data": "ten_MDMat", "name": "ten_MDMat", "autowidth": true },
            { "data": "ghiChu", "name": "ghiChu", "autowidth": true }
        ],

        stateSave: true,
        "bDestroy": true
    })

    $('#tinhmatrestore tbody').on('click', '.rowCheckboxRes', function () {
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