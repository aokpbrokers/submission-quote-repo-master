var options = {
	chart: {
		height: 120,
		type: 'bar',
		toolbar: {
			show: false,
		},
		dropShadow: {
			enabled: true,
			opacity: 0.1,
			blur: 5,
			left: -10,
			top: 20
		},
	},
	dataLabels: {
		enabled: false,
	},
	plotOptions: {
		bar: {
			horizontal: false,
			endingShape: 'rounded',
			columnWidth: '20px',
		},
	},
	stroke: {
		show: false,
	},
	series: [{
		name: 'Sales',
		data: [3, 4, 6, 7, 5, 8, 4]
	}],
	xaxis: {
		axisBorder: {
			show: true
		},
		labels: {
			show: true
		},
		categories: ["S", "M", "T", "W", "T", "F", "S"],
	},
	yaxis: {
		labels: {
			show: false
		}
	},
	fill: {
		opacity: 1
	},
	tooltip: {
		y: {
			formatter: function(val) {
				return "$" + val + "k"
			}
		}
	},
	grid: {
		borderColor: '#ced1e0',
		strokeDashArray: 5,
		padding: {
	    left: 0,
	    right: 10
	  }
	},
	colors: ['#5a8dee'],
}
var chart = new ApexCharts(
	document.querySelector("#salesColumnGraph"),
	options
);
chart.render();
