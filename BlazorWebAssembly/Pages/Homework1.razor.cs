using Microsoft.AspNetCore.Components;

namespace BlazorWebAssembly.Pages;

public partial class Homework1
{
	[Parameter]
	public string MyParameter { get; set; }

	[Inject]
	public NavigationManager NavigationManager { get; set; }

	private void GoToIndex()
		=> NavigationManager.NavigateTo("/");
}