﻿$(document).ready(function () {
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
    loadRoles()
    loadUser()
}
function loadRoles() {
    $('#roles').dataTable({
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
            "url": "/roles/getAll",
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
            { "data": "name", "name": "ten_CV", "autowidth": true },
        ],
        layout: {
            topStart: {
                buttons: [
                    {
                        extend: 'colvis',
                        text: '<span >Hiện / Ẩn</span>',
                        titleAttr: 'Hiện / Ẩn',
                    },
                    {
                        extend: 'copy',
                        text: '<span >Sao chép</span>',
                        titleAttr: 'Sao chép',
                    },
                    {
                        extend: 'csv',
                        text: '<span >CSV</span>',
                        titleAttr: 'CSV',
                    },
                    {
                        extend: 'excel',
                        text: '<span >Excel</span>',
                        titleAttr: 'Excel',
                    },
                    {
                        extend: 'pdf',
                        text: '<span >PDF</span>',
                        titleAttr: 'pdf',
                    }
                ]
            }
        },
        stateSave: true,
        "bDestroy": true
    })
    $('#chucvu tbody').on('click', '.rowCheckbox', function () {
        if ($('.rowCheckbox:checked').length === $('.rowCheckbox').length) {
            $('#selectAllCheckbox').prop('checked', true);
        } else {
            $('#selectAllCheckbox').prop('checked', false);
        }
        toggleDeleteButton()
    });

}
function loadUser() {
    $('#user').dataTable({
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
            "url": "/user/getAll",
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
            { "data": "hoten_NV", "name": "hoten_NV", "autowidth": true },
            { "data": "diaChi_NV", "name": "diaChi_NV", "autowidth": true },
            { "data": "ngaySinh_NV", "name": "ngaySinh_NV", "autowidth": true },
            { "data": "roles", "name": "roles", "autowidth": true },
            {
                "data": "id",
                "render": function (data, row) {
                    return `
                             <div class="visible-md visible-lg hidden-sm hidden-xs action-buttons">
									<a class="blue" onclick="showInPopup('1','${data}')" href="#">
										<i class="icon-zoom-in bigger-130"></i>
									</a>

									<a class="green" onclick="showInPopup('2','${data}')" href="#">
										<i class="icon-pencil bigger-130"></i>
									</a>
                                    <a class="pink" onclick="showInPopup('3','${data}')" href="#">
										<i class="icon-key bigger-130"></i>
									</a>
								</div>
                                <div class="visible-xs visible-sm hidden-md hidden-lg">
									<div class="inline position-relative">
										<button class="btn btn-minier btn-yellow dropdown-toggle" data-toggle="dropdown">
											<i class="icon-caret-down icon-only bigger-120"></i>
										</button>
										<ul class="dropdown-menu dropdown-only-icon dropdown-yellow pull-right dropdown-caret dropdown-close">
											<li>
												<a onclick="showInPopup('1','${data}')" href="#" data-rel="tooltip" title="" data-original-title="View">
													<span class="blue">
														<i class="icon-zoom-in bigger-120"></i>
													</span>
												</a>
											</li>
                                            <li>
												<a onclick="showInPopup('2','${data}')" href="#" class="tooltip-success" data-rel="tooltip" title="" data-original-title="Edit">
													<span class="green">
														<i class="icon-edit bigger-120"></i>
													</span>
												</a>
											</li>
                                            <li>
												<a onclick="showInPopup('3','${data}')" href="#" class="tooltip-success" data-rel="tooltip" title="" data-original-title="Edit">
													<span class="pink">
														<i class="icon-key bigger-120"></i>
													</span>
												</a>
											</li>
										</ul>
									</div>
								</div>`
                }
            }
        ],
        layout: {
            topStart: {
                buttons: [
                    {
                        extend: 'colvis',
                        text: '<span >Hiện / Ẩn</span>',
                        titleAttr: 'Hiện / Ẩn',
                    },
                    {
                        extend: 'copy',
                        text: '<span >Sao chép</span>',
                        titleAttr: 'Sao chép',
                    },
                    {
                        extend: 'csv',
                        text: '<span >CSV</span>',
                        titleAttr: 'CSV',
                    },
                    {
                        extend: 'excel',
                        text: '<span >Excel</span>',
                        titleAttr: 'Excel',
                    },
                    {
                        extend: 'pdf',
                        text: '<span >PDF</span>',
                        titleAttr: 'pdf',
                    }
                ]
            }
        },
        stateSave: true,
        "bDestroy": true
    })
    $('#user tbody').on('click', '.rowCheckbox', function () {
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
    if (url == 2) {
        url = $('#requestid').data('request-url') + "/" + id;
    } else if (url == 3) {
        url = $('#requestRoles').data('request-url') + "/" + id;
    }
    else {
        url = $('#request').data('request-url') + "/" + id;
    }
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
