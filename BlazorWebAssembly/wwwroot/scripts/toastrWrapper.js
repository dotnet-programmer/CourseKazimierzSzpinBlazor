// u�ywanie bibliotek JS w C#
// w�asny skrypt, w kt�rym zosta�y opakowane funkcje kt�re b�d� u�ywane

window.toastrFunctions = {
	showToastrInfo: function (message) {
		toastr.info(message);
	},
	showToastrError: function (message) {
		toastr.error(message);
	}
}