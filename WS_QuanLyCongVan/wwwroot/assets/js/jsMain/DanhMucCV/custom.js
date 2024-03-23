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
    loadLoaisocv()
    loadLoaicv()
    loadSocv()
    loadLinhvuc()
    loadTinhKhan()
    loadTinhMat()
}
function loadLoaisocv() {
    $('#loaisocv').DataTable({
        "serverSide": true,
        "processing": true,
        "ajax": {
            "url": "/LoaiSoCV/getAll",
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
                            <input type="checkbox" class="rowCheckbox" value="${data}" ></input>
                           `
                },

            },
            { "data": "ten_LSCV", "name": "ten_LSCV", "autowidth": true },
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
        "stateSave": true,
        "bDestroy": true
    })
    $('#loaisocv tbody').on('click', '.rowCheckbox', function () {
        if ($('.rowCheckbox:checked').length === $('.rowCheckbox').length) {
            $('#selectAllCheckbox').prop('checked', false);
        } else {
            $('#selectAllCheckbox').prop('checked', true);
        }
        toggleDeleteButton()
    });
}
function loadLoaicv() {
    $('#loaicv').dataTable({
        "serverSide": true,
        "filter": true,
        "processing": true,
        "ajax": {
            "url": "/LoaiCV/getAll",
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
                            <input type="checkbox" class="rowCheckbox" value="${data}" ></input>
                           `
                },

            },
            { "data": "ten_LVB", "name": "ten_LVB", "autowidth": true },
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
    $('#loaicv tbody').on('click', '.rowCheckbox', function () {
        if ($('.rowCheckbox:checked').length === $('.rowCheckbox').length) {
            $('#selectAllCheckbox').prop('checked', true);
        } else {
            $('#selectAllCheckbox').prop('checked', false);
        }
        toggleDeleteButton()
    });

}
function loadSocv() {
    $('#socv').dataTable({
        "serverSide": true,
        "filter": true,
        "processing": true,
        "ajax": {
            "url": "/socv/getAll",
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
                            <input type="checkbox" class="rowCheckbox" value="${data}" ></input>
                           `
                },

            },
            { "data": "ten_SoCV", "name": "ten_SoCV", "autowidth": true },
            { "data": "ngay_SoCV", "name": "Ngay_SoCV", "autowidth": true },
            { "data": "tb_LoaiSoCV.ten_LSCV", "name": "tb_loaisocv.Ten_LSCV", "autowidth": true },
            { "data": "trangThai", "name": "TrangThai", "autowidth": true },
            { "data": "ghichu", "name": "ghichu", "autowidth": true },
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
    $('#socv tbody').on('click', '.rowCheckbox', function () {
        if ($('.rowCheckbox:checked').length === $('.rowCheckbox').length) {
            $('#selectAllCheckbox').prop('checked', true);
        } else {
            $('#selectAllCheckbox').prop('checked', false);
        }
        toggleDeleteButton()
    });

}
function loadLinhvuc() {
    $('#linhvuc').dataTable({
        "serverSide": true,
        "filter": true,
        "processing": true,
        "ajax": {
            "url": "/linhvuc/getAll",
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
                            <input type="checkbox" class="rowCheckbox" value="${data}" ></input>
                           `
                },

            },
            { "data": "ten_LV", "name": "ten_LV", "autowidth": true },
            { "data": "ghichu", "name": "ghichu", "autowidth": true },
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
    $('#linhvuc tbody').on('click', '.rowCheckbox', function () {
        if ($('.rowCheckbox:checked').length === $('.rowCheckbox').length) {
            $('#selectAllCheckbox').prop('checked', true);
        } else {
            $('#selectAllCheckbox').prop('checked', false);
        }
        toggleDeleteButton()
    });

}
function loadTinhKhan() {
    $('#tinhkhan').dataTable({
        "serverSide": true,
        "filter": true,
        "processing": true,
        "ajax": {
            "url": "/tinhkhan/getAll",
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
                            <input type="checkbox" class="rowCheckbox" value="${data}" ></input>
                           `
                },

            },
            { "data": "ten_MDKhan", "name": "ten_MDKhan", "autowidth": true },
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
    $('#tinhkhan tbody').on('click', '.rowCheckbox', function () {
        if ($('.rowCheckbox:checked').length === $('.rowCheckbox').length) {
            $('#selectAllCheckbox').prop('checked', true);
        } else {
            $('#selectAllCheckbox').prop('checked', false);
        }
        toggleDeleteButton()
    });

}
function loadTinhMat() {
    $('#tinhmat').dataTable({
        "serverSide": true,
        "filter": true,
        "processing": true,
        "ajax": {
            "url": "/tinhmat/getAll",
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
                            <input type="checkbox" class="rowCheckbox" value="${data}" ></input>
                           `
                },

            },
            { "data": "ten_MDMat", "name": "ten_MDMat", "autowidth": true },
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
    $('#tinhmat tbody').on('click', '.rowCheckbox', function () {
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

