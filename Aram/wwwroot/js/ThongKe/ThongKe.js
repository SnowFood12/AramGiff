﻿


var id = [];

var soLuong = []

var Huy = []

// lấy ngày trong tháng
function printAllDaysInMonth(month, year) {
    var date = new Date(year, month, 1);
    var daysInMonth = new Date(year, month + 1, 0).getDate();
    for (var day = 1; day <= daysInMonth; day++) {
        date.setDate(day);

        id.push(day)

        soLuong.push(0);

        Huy.push(0);

    }
}

// lấy dữ liệu vẽ biểu đồ
function loadRevenueData() {
    // lấy số ngày trong tháng
    var currentDate = new Date();
    var month = currentDate.getMonth() + 1;
    printAllDaysInMonth(month, 2023);
    // =====================================

    // lấy đơn đã giao 
    $.ajax({
        url: '/DonHang/ThongKeDoanhThu',
        method: 'GET',
        success: function (response) {
            response.forEach(function (item) {
                item.day.forEach(function (i) {
                    console.log(i.trangThai)
                    var date = new Date(i.thoiGianTaoDon);
                    var day = date.getDate();
                    for (var i = 0; i < id.length; i++) {
                        if (i === day) {
                            soLuong[i - 1] = item.count;
                        }
                    }
                });

            })
        }
    })


    //lấy đơn đã huỷ
    $.ajax({
        url: '/DonHang/DonHangDaHuy',
        method: 'GET',
        success: function (response) {
            response.forEach(function (item) {
                item.day.forEach(function (i) {
                    console.log(i.trangThai)
                    var date = new Date(i.thoiGianTaoDon);
                    var day = date.getDate();
                    for (var i = 0; i < id.length; i++) {
                        if (i === day) {
                            Huy[i - 1] = item.count;
                        }
                    }
                });

            })
        }
    })

    var ctx = document.getElementById('myChart').getContext('2d');
    var data = {
        labels: id,
        datasets: [{
            label: 'Đơn hàng đã giao',
            data: soLuong,
            backgroundColor: 'rgba(54, 162, 235, 0.5)',
            borderColor: 'rgba(54, 162, 235, 1)',
            borderWidth: 1
        }, {
            label: 'Đơn hàng đã huỷ',
            data: Huy,
            backgroundColor: 'rgba(255, 99, 132, 0.2)',
            borderColor: 'rgb(255, 99, 132)',
            borderWidth: 1
        }]
    };

    // Cấu hình biểu đồ
    var options = {
        responsive: true,
        scales: {
            y: {
                beginAtZero: true
            }
        }
    };
    // Tạo biểu đồ cột
    var myChart = new Chart(ctx, {
        type: 'bar',
        data: data,
        options: options
    });
}


$(document).ready(function () {
    loadRevenueData();
});

// Tạo biểu đồ cột


// Lấy tham chiếu đến thẻ canvas

// ['Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6', 'Tháng 7', 'Tháng 8', 'Tháng 9', 'Tháng 10', 'Tháng 11', 'Tháng 12',],
//[500, 800, 700, 300, 600, 600, 600, 600, 600, 600, 600, 600],
// Dữ liệu cho biểu đồ
