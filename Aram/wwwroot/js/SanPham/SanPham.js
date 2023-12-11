setTimeout(function () {
    var alertMessage = document.querySelector('.alert__thongbao');
    alertMessage.style.display = 'none';
    alertMessage.style.transform = 'translateX(100%)';
}, 4000);

var cbk = document.querySelectorAll('.check')

function LoadTrangThai() {
    $.ajax({
        url: '/SanPham/Check',
        type: 'GET',
        dataType: 'json',
        success: function (result) {
            result.forEach(function (item) {
                for (var i = 0; i < cbk.length; i++) {
                    var dataId = cbk[i].dataset.id;
                    if (item.id == dataId) {
                        cbk[i].checked = item.trangThai
                    }
                }
            });
        }
    });
}
LoadTrangThai()
function DoiTrangThai(i) {
    alert('Bạn có muốn đổi trạng thái của phẩm sản phẩm này không?')
    $.ajax({
        url: '/SanPham/CapNhatTrangThai',
        type: 'GET',
        data: { id: i },
        dataType: 'json',
        success: function (result) {
            result.forEach(function (item) {
                for (var i = 0; i < cbk.length; i++) {
                    var dataId = cbk[i].dataset.id;
                    if (item.id == dataId) {
                        cbk[i].checked = item.trangThai
                    }
                }
            });
        }
    });

    var alertMessage = document.querySelector('.alert__thongbao1');

    alertMessage.innerText = "Cập nhật trạng thái sản phẩm thành công"
    alertMessage.style.display = 'block';
    alertMessage.style.transform = 'translateX(0)';

    setTimeout(function () {
        alertMessage.style.display = 'none';
        alertMessage.style.transform = 'translateX(100%)';
    }, 4000);   
}
