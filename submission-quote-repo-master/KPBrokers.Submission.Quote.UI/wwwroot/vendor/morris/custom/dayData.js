// Morris Days
var day_data = [
	{"period": "2016-10-01", "licensed": 3213, "Maxwell": 887},
	{"period": "2016-09-30", "licensed": 3321, "Maxwell": 776},
	{"period": "2016-09-29", "licensed": 3671, "Maxwell": 884},
	{"period": "2016-09-20", "licensed": 3176, "Maxwell": 448},
	{"period": "2016-09-19", "licensed": 3376, "Maxwell": 565},
	{"period": "2016-09-18", "licensed": 3976, "Maxwell": 627},
	{"period": "2016-09-17", "licensed": 2239, "Maxwell": 660},
	{"period": "2016-09-16", "licensed": 3871, "Maxwell": 676},
	{"period": "2016-09-15", "licensed": 3659, "Maxwell": 656},
	{"period": "2016-09-10", "licensed": 3380, "Maxwell": 663}
];
Morris.Line({
	element: 'dayData',
	data: day_data,
	xkey: 'period',
	ykeys: ['licensed', 'Maxwell'],
	labels: ['Licensed', 'Maxwell'],
	resize: true,
	hideHover: "auto",
	gridLineColor: "#ced1e0",
	pointFillColors:['#ffffff'],
	pointStrokeColors: ['#007ae1', '#ff3e3e', '#00bb42', '#ffbf05'],
	lineColors:['#007ae1', '#ff3e3e', '#00bb42', '#ffbf05'],
});