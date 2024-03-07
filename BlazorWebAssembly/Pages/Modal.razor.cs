using Microsoft.AspNetCore.Components.Web;

namespace BlazorWebAssembly.Pages;

public partial class Modal
{
	private bool _showModalWindow = false;
	private bool _showModalWindowCourse = false;

	public void ShowModalWindow()
		=> _showModalWindow = true;

	public void ShowModalWindowCourse()
		=> _showModalWindowCourse = true;

	private void ModalAccept(MouseEventArgs e)
	{
		// jakaś logika
		Console.WriteLine("ModalAccept");
		_showModalWindow = _showModalWindowCourse = false;
	}

	private void ModalCancel(MouseEventArgs e)
	{
		Console.WriteLine("ModalCancel");
		_showModalWindow = _showModalWindowCourse = false;
	}
}