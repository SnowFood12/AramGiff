


var id = [];
var soLuong = []

$.ajax({
    url: '/DonHang/ThongKeDoanhThu', // Đường dẫn API của bạn
    method: 'GET',
    success: function (response) {
        response.forEach(function (item) {
            id.push(item.id);
            soLuong.push(item.soLuong);
        })

        var ctx = document.getElementById('myChart').getContext('2d');

        // Dữ liệu cho biểu đồ
        var data = {
            labels: id ,
            datasets: [{
                label: 'Thu nhập',
                data: soLuong,
                backgroundColor: 'rgba(54, 162, 235, 0.5)',
                borderColor: 'rgba(54, 162, 235, 1)',
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
            type: 'line',
            data: data,
            options: options
        });
    }
})


// Tạo biểu đồ cột


// Lấy tham chiếu đến thẻ canvas

// ['Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6', 'Tháng 7', 'Tháng 8', 'Tháng 9', 'Tháng 10', 'Tháng 11', 'Tháng 12',],
//[500, 800, 700, 300, 600, 600, 600, 600, 600, 600, 600, 600],
// Dữ liệu cho biểu đồ
