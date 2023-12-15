let thispage = 1;
let limit = 20;
let list = document.querySelectorAll('.contaner__content--trangchu')

// phân trang

function loadItem() {
	let beginGet = limit * (thispage - 1);
	let endGet = limit * thispage - 1;
	list.forEach((item, key) => {
		if (key >= beginGet && key <= endGet) {
			item.style.display = 'block';
		}
		else {
			item.style.display = 'none'
		}
	})
	listpage();
}
loadItem()

function listpage() {
	let count = Math.ceil(list.length / limit)
	document.querySelector('.PhanTrang').innerHTML = '';
	for (let i = 1; i <= count; i++) {
		let newpage = document.createElement('li');
		newpage.innerText = i;
		if (i == thispage) {
			newpage.classList.add('active__phan--trang');
		}
		newpage.setAttribute('onclick', `changePage(${i})`)
		document.querySelector('.PhanTrang').appendChild(newpage);
	}
}
function changePage(i) {
	thispage = i;
	loadItem();
	window.scrollTo({
		top: 300,
		behavior: "smooth"
	});
}
//==================================================================

// tim kiếm có gợi ý


var timkiem = document.getElementById('timkiem')

var goi__y = document.querySelector('.goi__y')

timkiem.addEventListener('input', function (event) {
	goi__y.innerHTML = '';
	var search = event.target.value.toLowerCase();
	$.ajax({
		url: '/Home/Search',
		type: 'GET',
		data: { search: search },
		dataType: 'json',
		success: function (result) {
			if (result == null || result.length == 0) {
				goi__y.style.visibility = 'hidden'								
			}
			else {
				goi__y.style.visibility = 'visible'
				result.forEach(function (item) {
					console.log(item.ten)
					goi__yitem = document.createElement('li');
					goi__yitem.innerText = item.ten;
					goi__yitem.classList.add('goi__item')
					goi__y.appendChild(goi__yitem)
					goi__yitem.setAttribute('onclick', `onChange('${item.ten}')`)

					var list__item = document.querySelectorAll('.goi__item')
					if (list__item.length > 4) {
						goi__y.style.height = '10rem';
						goi__y.style.overflowY = 'auto'
					}
					else {
						goi__y.style.height = 'auto';
					}
				})
			}
		}
	}); 
});
function onChange(e) {
	timkiem.value = e;
	goi__y.style.visibility = 'hidden'
}