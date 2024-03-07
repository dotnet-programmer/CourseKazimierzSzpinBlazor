using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorWebAssembly.Components;

public partial class ModalWindow
{
	//[Parameter]
	//public string Title { get; set; }

	//[Parameter]
	//public string Content { get; set; }

	//[Parameter]
	//public EventCallback OnCloseWindow { get; set; }
	//[Parameter]
	//public EventCallback OnConfirm { get; set; }
	//[Parameter]
	//public EventCallback OnCancel { get; set; }

	//private void CloseWindow(MouseEventArgs e)
	//{

	//}

	//private void Confirm(MouseEventArgs e)
	//{
	//	OnConfirm.InvokeAsync();
	//}

	//private void Cancel(MouseEventArgs e)
	//{
	//	OnCancel.InvokeAsync();
	//}

	[Parameter]
	public bool Show { get; set; }

	[Parameter]
	public RenderFragment Title { get; set; }

	[Parameter]
	public RenderFragment Body { get; set; }

	[Parameter]
	public EventCallback<MouseEventArgs> OnCancel { get; set; }

	[Parameter]
	public EventCallback<MouseEventArgs> OnAccept { get; set; }
}
