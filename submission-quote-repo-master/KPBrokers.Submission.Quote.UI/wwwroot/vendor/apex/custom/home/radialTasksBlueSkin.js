var options = {
	chart: {
		height: 218,
		type: 'radialBar',
		toolbar: {
			show: false,
		},
		dropShadow: {
			enabled: true,
			opacity: 0.2,
			blur: 9,
			left: 10,
			top: 20
		},
	},
	plotOptions: {
		radialBar: {
			track: {
		    background: '#4b546f',
			},
			dataLabels: {
				name: {
					fontSize: '12px',
					fontColor: 'black',
          fontFamily: 'Open Sans, sans-serif',
				},
				value: {
					fontSize: '21px',
					fontFamily: 'Open Sans, sans-serif',
				},
				total: {
					show: true,
					label: 'Tasks',
					formatter: function (w) {
						// By default this function returns the average of all series. The below is just an example to show the use of custom formatter function
						return '21'
					}
				}
			}
		},
	},
	series: [85, 60, 45],
	labels: ['New', 'Completed', 'Pending'],
	colors: ['#0047b1', '#008e18', '#f1ae07'],
}

var chart = new ApexCharts(
	document.querySelector("#radialTasksBlueSkin"),
	options
);
chart.render();