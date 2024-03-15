using Microsoft.AspNetCore.Components;
using SimpleShop.Shared.Products.Dtos;

namespace SimpleShop.Client.Shared;

public partial class Product
{
	private string _baseUrl = string.Empty;

	[Parameter]
	public ProductDto ProductModel { get; set; }

	[Inject]
	public NavigationManager NavigationManager { get; set; }

	[Inject]
	public IConfiguration Configuration { get; set; }

	protected override async Task OnInitializedAsync() 
		=> _baseUrl = Configuration["ApiConfiguration:BaseAddress"];

	private async Task OnAddProductToCart()
	{
		//
	
		NavigationManager.NavigateTo("/koszyk");
	}
}