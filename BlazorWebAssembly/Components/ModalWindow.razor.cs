﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorWebAssembly.Components;

public partial class ModalWindow
{
	[Parameter]
	public bool Show { get; set; }

	[Parameter]
	public string Title { get; set; }

	[Parameter]
	public string Content { get; set; }

	[Parameter]
	public EventCallback<MouseEventArgs> OnCancel { get; set; }

	[Parameter]
	public EventCallback<MouseEventArgs> OnConfirm { get; set; }
}