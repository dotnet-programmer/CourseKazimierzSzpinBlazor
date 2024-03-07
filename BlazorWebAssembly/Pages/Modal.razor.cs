using Microsoft.AspNetCore.Components.Web;

namespace BlazorWebAssembly.Pages;

public partial class Modal
{
	private bool _showModalWindow = false;

	public void ShowModalWindow()
	{
		_showModalWindow = true;
	}

	private void ModalAccept(MouseEventArgs e)
	{
        // jakaś logika
        Console.WriteLine("ModalAccept");
        _showModalWindow = false;
	}

	private void ModalCancel(MouseEventArgs e)
	{
		Console.WriteLine("ModalCancel");
		_showModalWindow = false;
	}
}
