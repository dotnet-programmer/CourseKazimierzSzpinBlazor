﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SimpleShop.Client.HttpRepository.Interfaces;

namespace SimpleShop.Client.Pages.Authorization;

public partial class Logout
{
	private static readonly IComponentRenderMode _renderMode = new InteractiveAutoRenderMode(prerender: false);

	[Inject]
	public IAuthenticationHttpRepository AuthenticationHttpRepository { get; set; }

	[Inject]
	public NavigationManager NavigationManager { get; set; }

	protected override async Task OnInitializedAsync()
	{
		await AuthenticationHttpRepository.Logout();
		NavigationManager.NavigateTo("/logowanie");
	}
}
