using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace TodoApp.Client.Components.Common;

public partial class Modal
{
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