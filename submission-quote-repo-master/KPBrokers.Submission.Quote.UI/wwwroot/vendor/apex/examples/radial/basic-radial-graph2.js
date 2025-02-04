var options = {
	chart: {
		height: 280,
		type: 'radialBar',
	},
	plotOptions: {
		radialBar: {
			dataLabels: {
				name: {
					fontSize: '13px',
				},
				value: {
					fontSize: '16px',
				},
				total: {
					show: true,
					label: 'Tasks',
					formatter: function (w) {
						// By default this function returns the average of all series. The below is just an example to show the use of custom formatter function
						return '45'
					}
				}
			}
		}
	},
	series: [90, 80, 70],
	labels: ['Completed', 'Ongoing', 'New'],
	colors: ['#007ae1', '#ff3e3e', '#00bb42', '#ffbf05'],
}

var chart = new ApexCharts(
	document.querySelector("#basic-radial-graph2"),
	options
);
chart.render();