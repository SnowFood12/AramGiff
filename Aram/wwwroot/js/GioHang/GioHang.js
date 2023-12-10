var tang = document.querySelectorAll('.tang')

var giam = document.querySelectorAll('.giam')


var tamtinh = document.getElementById('TamTinh')

var tongtien = document.getElementById('TongTien')

for (var i = 0; i < tang.length; i++) {
    tang[i].addEventListener('click', function () {
        var id = this.dataset.id;
        var soluong = document.getElementById(`${id}`)
        var sl = soluong.value
        var slTang = ++sl
        soluong.value = slTang;
        var tongTien = document.getElementById(`t ${id}`)
        $.ajax({
            url: '/GioHang/TangSoLuong',
            type: 'GET',
            data: { id: id },
            dataType: 'json',
            success: function (result) {
                let vndString = result.tongTien.toLocaleString().replace(/,/g, ',') + ',000 ₫';

                let tamTinh = result.tamTinh.toLocaleString().replace(/,/g, ',') + ',000 ₫';

                var tongTienGioHang = result.tamTinh + 20;
                let Tong = tongTienGioHang.toLocaleString().replace(/,/g, ',') + ',000 ₫';

                tongTien.innerHTML = vndString;
                tamtinh.innerHTML = tamTinh;
                tongtien.innerHTML = Tong;
            }
        })
    }); 
}

for (var i = 0; i < giam.length; i++) {
    giam[i].addEventListener('click', function () {
        var id = this.dataset.id;
        var soluong = document.getElementById(`${id}`)
        var sl = soluong.value
        if (sl == 1) {

        }
        else {
            var slTang = --sl
            soluong.value = slTang;
            var tongTien = document.getElementById(`t ${id}`)
            $.ajax({
                url: '/GioHang/GiamSoLuong',
                type: 'GET',
                data: { id: id },
                dataType: 'json',
                success: function (result) {
                    console.log(result)
                    let vndString = result.tongTien.toLocaleString().replace(/,/g, ',') + ',000 ₫';

                    let tamTinh = result.tamTinh.toLocaleString().replace(/,/g, ',') + ',000 ₫';

                    var tongTienGioHang = result.tamTinh + 20;
                    let Tong = tongTienGioHang.toLocaleString().replace(/,/g, ',') + ',000 ₫';

                    tongTien.innerHTML = vndString;
                    tamtinh.innerHTML = tamTinh;
                    tongtien.innerHTML = Tong;
                }
            })
        }
    });
}