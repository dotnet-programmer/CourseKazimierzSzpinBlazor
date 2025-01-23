function addNumberJS(number1, number2) {
	var sum = number1 + number2;
	alert(sum);
}

// wywo�anie funkcji C# w JS
function addNumberCSharp(number1, number2) {
	// pierwszy argument to namespace aplikacji, 
	// drugi to nazwa metody C# do wywo�ania, 
	// kolejne to argumenty tej metody
	DotNet.invokeMethodAsync("BlazorWebAssembly", "Add", parseInt(number1), parseInt(number2))
		// je�li zostanie wykonane, to wy�wietl alert z wynikiem
		.then(result => { alert(result); });
}

function GetCurrentDateCSharp() {
	return DotNet.invokeMethodAsync("BlazorWebAssembly", "GetCurrentDate");
}