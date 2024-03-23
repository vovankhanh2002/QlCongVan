$(document).ready(function () {
    Load()
    $('#selectAllCheckbox').on('click', function () {
        if (this.checked) {
            $('.rowCheckbox').prop('checked', true);
        } else {
            $('.rowCheckbox').prop('checked', false);
        }
        toggleDeleteButton()
    });
    $('#delete').on('click', function () {
        if (confirm("Bạn muốn tiếp tục không ?")) {
            var checkedData = [];
            $('.rowCheckbox:checked').each(function () {
                checkedData.push($(this).val());
            });
            $.ajax({
                url: $(this).data('request-url'),
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
    });

})

//Start Load datatable
function Load() {
    loadPhongban()
    loadNhanvien()
}
function loadPhongban() {
    $('#phongban').dataTable({
        "serverSide": true,
        "filter": true,
        "processing": true,
        "ajax": {
            "url": "/phongban/getAll",
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
                            <div class="center">
                                <input type="checkbox" class="rowCheckbox" value="${data}" ></input>
                            </div>
                           `
                },

            },
            { "data": "ten_PB", "name": "ten_PB", "autowidth": true },
            { "data": "ghiChu", "name": "ghiChu", "autowidth": true },
            {
                "data": "id",
                "render": function (data, row) {
                    return `
                             <div class="btn-group">
								<button data-toggle="dropdown" class="btn btn-primary dropdown-toggle">
									Tùy chọn
									<i class="icon-angle-down icon-on-right"></i>
								</button>

								<ul class="dropdown-menu">
									<li>
										 <a href="#" onclick="showInPopup('','${data}')" title="Sửa"><i class="icon-pencil bigger-130"></i>Sửa</a>
									</li>
								</ul>
							</div>
                           `
                }
            }
        ],

        stateSave: true,
        "bDestroy": true
    })
    $('#phongban tbody').on('click', '.rowCheckbox', function () {
        if ($('.rowCheckbox:checked').length === $('.rowCheckbox').length) {
            $('#selectAllCheckbox').prop('checked', true);
        } else {
            $('#selectAllCheckbox').prop('checked', false);
        }
        toggleDeleteButton()
    });

}
function loadNhanvien() {
    $('#nhanvien').dataTable({
        "serverSide": true,
        "filter": true,
        "processing": true,
        "ajax": {
            "url": "/nhanvien/getAll",
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
                            <div class="center">
                                <input type="checkbox" class="rowCheckbox" value="${data}" ></input>
                            </div>
                           `
                },

            },
            { "data": "hoten_NV", "name": "hoten_NV", "autowidth": true },
            { "data": "diaChi_NV", "name": "diaChi_NV", "autowidth": true },
            { "data": "sdT_NV", "name": "SDT_NV", "autowidth": true },
            { "data": "ngaySinh_NV", "name": "ngaySinh_NV", "type": "date", "autowidth": true },
            { "data": "tb_PhongBan.ten_PB", "name": "tb_PhongBan.ten_PB", "autowidth": true },
            { "data": "tb_ChucVu.ten_CV", "name": "tb_ChucVu.ten_CV", "autowidth": true },
            { "data": "ghiChu", "name": "ghiChu", "autowidth": true },

            {
                "data": "id",
                "render": function (data, row) {
                    return `
                             <div class="btn-group">
								<button data-toggle="dropdown" class="btn btn-primary dropdown-toggle">
									Tùy chọn
									<i class="icon-angle-down icon-on-right"></i>
								</button>

								<ul class="dropdown-menu">
									<li>
										 <a href="#" onclick="showInPopup('','${data}')" title="Sửa"><i class="icon-pencil bigger-130"></i>Sửa</a>
									</li>
								</ul>
							</div>
                           `
                }
            }
        ],

        stateSave: true,
        "bDestroy": true
    })
    $('#nhanvien tbody').on('click', '.rowCheckbox', function () {
        if ($('.rowCheckbox:checked').length === $('.rowCheckbox').length) {
            $('#selectAllCheckbox').prop('checked', true);
        } else {
            $('#selectAllCheckbox').prop('checked', false);
        }
        toggleDeleteButton()
    });

}

//-----------------------------

//Start Post Add Update Delete
const JqueryAjaxPost = form => {
    try {
        $.ajax({
            type: "POST",
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.success) {
                    $("#form-modal").modal('hide')
                    $.notify(res.notify, { globalPosition: 'top right', className: "success" });
                    Load()
                } else {
                    $("#form-modal .modal-body").html(res.html);
                }
            },
            error: function (xhr, status, error) {
                console.log(xhr.responseText);
            }
        });
    } catch (e) {
        console.log(e);
    }
    return false;
}
function showInPopup(url, id) {
    url = $('#request').data('request-url') + "/" + id;
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $("#form-modal .modal-body").html(res)
            $("#form-modal").modal('show')
        }
    })
}
//End Post Add Update Delete

//-----------------------------



function closePopup() {
    $("#form-modal").modal('hide')
}

function toggleDeleteButton() {
    if ($('.rowCheckbox:checked').length > 0) {
        $('#delete').prop('disabled', false);
    } else {
        $('#delete').prop('disabled', true);
    }
}

