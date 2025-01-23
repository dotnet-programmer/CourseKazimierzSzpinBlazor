// u¿ywanie bibliotek JS w C#
// w³asny skrypt, w którym zosta³y opakowane funkcje które bêd¹ u¿ywane

window.toastrFunctions = {
	showToastrInfo: function (message) {
		toastr.info(message);
	},
	showToastrError: function (message) {
		toastr.error(message);
	}
}