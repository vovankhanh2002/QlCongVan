$(document).ready(function () {
    Load()
    var connection = new signalR.HubConnectionBuilder().withUrl("/NotihubServer").build();
    connection.on("ReceiveMessage", function (message) {
        $.notify(message, { globalPosition: 'top right', className: "success" });

    });
    connection.start();

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



    var currentUrl = window.location.pathname.split("/")[1];
    if (currentUrl != "") {
        $("#loaderbody").addClass('hide')
        $(document).bind('ajaxStart', function () {
            $("#loaderbody").removeClass('hide')
        }).bind('ajaxStop', function () {
            $("#loaderbody").addClass('hide')
        })
       
    }
    const toastTrigger = document.getElementById('liveToastBtn')
    const toastLiveExample = document.getElementById('liveToast')

    if (toastTrigger) {
        const toastBootstrap = bootstrap.Toast.getOrCreateInstance(toastLiveExample)
        toastTrigger.addEventListener('click', () => {
            toastBootstrap.show()
        })
    }

    
   
})

//Start Load datatable
function Load() {
    loadDanhmuccv()
    loadCvden()
    loadCvdi()
}
function loadDanhmuccv() {
    $('#danhmuccv').dataTable({
        "serverSide": true,
        "filter": true,
        "processing": true,
        "ajax": {
            "url": "/danhmuccv/getAll",
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
            { "data": "ma_HS", "name": "ma_HS", "autowidth": true },
            { "data": "ten_HS", "name": "ten_HS", "autowidth": true },
            { "data": "tb_Kho.ten_Kho", "name": "tb_Kho.ten_Kho", "autowidth": true },
            { "data": "tb_Ke.ten_Ke", "name": "tb_Ke.ten_Ke", "autowidth": true },
            { "data": "tb_Hop.ten_Hop", "name": "tb_Hop.ten_Hop", "autowidth": true },
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
    $('#danhmuccv tbody').on('click', '.rowCheckbox', function () {
        if ($('.rowCheckbox:checked').length === $('.rowCheckbox').length) {
            $('#selectAllCheckbox').prop('checked', true);
        } else {
            $('#selectAllCheckbox').prop('checked', false);
        }
        toggleDeleteButton()
    });

}
function loadCvden() {
    $('#cvden').DataTable({
        "serverSide": true,
        "filter": true,
        "processing": true,
        "ajax": {
            "url": "/cvden/getAll",
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
            { "data": "skh_CVDEN", "name": "skh_CVDEN", "autowidth": true },
            { "data": "ngayBH_CVDEN", "name": "ngayBH_CVDEN", "autowidth": true },
            { "data": "hanTL_CVDEN", "name": "hanTL_CVDEN", "autowidth": true },
            { "data": "trichYeu_CVDEN", "name": "trichYeu_CVDEN", "autowidth": true },
            { "data": "tb_LoaiVB.ten_LVB", "name": "tb_LoaiCV.ten_LVB", "autowidth": true },
            { "data": "tb_NhanVien.hoten_NV", "name": "tb_NhanVien.hoten_NV", "autowidth": true },
            { "data": "tb_MDMat.ten_MDMat", "name": "tb_MDMat.ten_MDMat", "autowidth": true },
            { "data": "tb_MDKhan.ten_MDKhan", "name": "tb_MDKhan.ten_MDKhan", "autowidth": true },
            { "data": "tb_LinhVuc.ten_LV", "name": "tb_LinhVuc.ten_LV", "autowidth": true },
            {
                "data": "trangThai_CVDI",
                "render": function (data) {
                    if (data) {
                        return `<span class="label label-success arrowed">True</span>`
                    }
                    return `<span class="label label-danger arrowed-in">False</span>`
                },
                "name": "trangThai_CVDI",
                "autowidth": true
            },

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
                                    <a class="dark " onclick="showInPopup('3','${data}')" href="#">
										<i class="icon-book bigger-130"></i>
									</a>
								</div>
                                <div class="visible-xs visible-sm hidden-md hidden-lg">
									<div class="inline position-relative">
										<button class="btn btn-minier btn-yellow dropdown-toggle" data-toggle="dropdown">
											<i class="icon-caret-down icon-only bigger-120"></i>
										</button>
										<ul class="dropdown-menu dropdown-only-icon dropdown-yellow pull-right dropdown-caret dropdown-close">
											<li>
												<a onclick="showInPopup('1','${data}')" href="#" data-rel="tooltip" title="" data-original-title="Xem">
													<span class="blue">
														<i class="icon-zoom-in bigger-120"></i>
													</span>
												</a>
											</li>
                                            <li>
												<a onclick="showInPopup('2','${data}')" href="#" class="tooltip-success" data-rel="tooltip" title="" data-original-title="Cập nhật">
													<span class="green">
														<i class="icon-edit bigger-120"></i>
													</span>
												</a>
											</li>
                                            <li>
												<a onclick="showInPopup('3','${data}')" href="#" class="tooltip-dark" data-rel="tooltip" title="" data-original-title="Danh mục">
													<span class="dark ">
														<i class="icon-book bigger-120"></i>
													</span>
												</a>
											</li>
										</ul>
									</div>
								</div>
                           
                           `
                }, "autowidth": true
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
        "bDestroy": true,
    })
    $('#cvden tbody').on('click', '.rowCheckbox', function () {
        if ($('.rowCheckbox:checked').length === $('.rowCheckbox').length) {
            $('#selectAllCheckbox').prop('checked', true);
        } else {
            $('#selectAllCheckbox').prop('checked', false);
        }
        toggleDeleteButton()
    });

}
function loadCvdi() {
    $('#cvdi').DataTable({
        "serverSide": true,
        "filter": true,
        "processing": true,
        "ajax": {
            "url": "/cvdi/getAll",
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
            { "data": "skh_CVDI", "name": "skh_CVDEN", "autowidth": true },
            { "data": "ngayBH_CVDI", "name": "ngayBH_CVDEN", "autowidth": true },
            { "data": "trichYeu_CVDI", "name": "trichYeu_CVDEN", "autowidth": true },
            { "data": "tb_LoaiVB.ten_LVB", "name": "tb_LoaiCV.ten_LVB", "autowidth": true },
            { "data": "tb_NhanVien.hoten_NV", "name": "tb_NhanVien.hoten_NV", "autowidth": true },
            { "data": "tb_MDMat.ten_MDMat", "name": "tb_MDMat.ten_MDMat", "autowidth": true },
            { "data": "tb_MDKhan.ten_MDKhan", "name": "tb_MDKhan.ten_MDKhan", "autowidth": true },
            { "data": "tb_LinhVuc.ten_LV", "name": "tb_LinhVuc.ten_LV", "autowidth": true },
            {
                "data": "trangThai_CVDI",
                "render": function (data) {
                    if (data) {
                        return `<span class="label label-success arrowed">True</span>`
                    }
                    return `<span class="label label-danger arrowed-in">False</span>`
                },
                "name": "trangThai_CVDI",
                "autowidth": true
            },
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
                                    <a class="dark " onclick="showInPopup('3','${data}')" href="#">
										<i class="icon-book bigger-130"></i>
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
										</ul>
									</div>
								</div>
                           
                           `
                }, "autowidth": true
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
        "bDestroy": true,
    })
    $('#cvdi tbody').on('click', '.rowCheckbox', function () {
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
                    $.notify(res.notify, { globalPosition: 'top right', className: "error" });
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
    if (url == 1) {
        url = $('#requestid').data('request-url') + "/" + id;
    }
    else if (url == 2) {
        url = $('#request').data('request-url') + "/" + id;
    }
    else if (url == 3) {
        url = $('#requestCategory').data('request-url') + "/" + id;
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
function showLoading() {
    document.getElementById('loading').style.display = 'block';
}
function hideLoading() {
    document.getElementById('loading').style.display = 'none';
}

function getSelectedValues() {

    var selectElement = document.getElementById("form-field-select-2");
    var getIdCongvan = $("#valIdCv").val();
    var getIdTieude = $("#idTieude").val();
    var selectedOptions = [];
    for (var i = 0; i < selectElement.options.length; i++) {
        var option = selectElement.options[i];
        if (option.selected) {
            selectedOptions.push(option.value);
        }
    }
    console.log(getIdCongvan, selectElement);
    showLoading();
    $.ajax({
        type: "POST",
        data: { id: getIdCongvan, tieude: getIdTieude, lstMail: selectedOptions },
        url: "/cvden/getById",
        success: function (res) {
            if (res.sussess) {
                $("#form-modal").modal('hide')
                $.notify(res.notify, { globalPosition: 'top right', className: "success" });
            } else {
                $.notify(res.notify, { globalPosition: 'top right', className: "error" });
            }
        },
        complete: function () {
            hideLoading();
        }
    })
}

function loadNhanVienCVDEN() {
    var boPhanId = $("#boPhanSelect").val();
    $.ajax({
        type: "GET",
        data: { emloy: boPhanId},
        url: "/CVDEN/GetEmployeesByDepartment",
        success: function (res) {
            if (res.sussess) {
                var nhanVienSelect = $("#nhanVienSelect");
                // Xóa tất cả các option hiện tại
                nhanVienSelect.empty();
                // Thêm các option mới
                var disabledOption = $("<option>");
                disabledOption.attr("disabled", true);
                disabledOption.text("-Nhân viên-");
                nhanVienSelect.append(disabledOption);

                $.each(res.data, function (index, employee) {
                    var option = $("<option>");
                    option.text(employee.hoten_NV);
                    option.val(employee.id);
                    nhanVienSelect.append(option);
                });
            } else {
                $.notify(res.notify, { globalPosition: 'top right', className: "error" });
            }
        },
        complete: function () {
            hideLoading();
        }
    })
}
function loadNhanVienCVDI() {
    var boPhanId = $("#boPhanSelect").val();
    $.ajax({
        type: "GET",
        data: { emloy: boPhanId },
        url: "/CVDI/GetEmployeesByDepartment",
        success: function (res) {
            if (res.sussess) {
                var nhanVienSelect = $("#nhanVienSelect");
                // Xóa tất cả các option hiện tại
                nhanVienSelect.empty();
                // Thêm các option mới
                var disabledOption = $("<option>");
                disabledOption.attr("disabled", true);
                disabledOption.text("-Nhân viên-");
                nhanVienSelect.append(disabledOption);

                $.each(res.data, function (index, employee) {
                    var option = $("<option>");
                    option.text(employee.hoten_NV);
                    option.val(employee.id);
                    nhanVienSelect.append(option);
                });
            } else {
                $.notify(res.notify, { globalPosition: 'top right', className: "error" });
            }
        },
        complete: function () {
            hideLoading();
        }
    })
}

