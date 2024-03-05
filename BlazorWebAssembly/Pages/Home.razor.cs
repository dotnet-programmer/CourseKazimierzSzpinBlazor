using Microsoft.AspNetCore.Components;

namespace BlazorWebAssembly.Pages;

public partial class Home
{
	[Inject]
	public NavigationManager NavigationManager { get; set; }

	private void GoToHomework1()
		=> NavigationManager.NavigateTo("/homework1/ParametrHome");
}