using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SimpleShop.Client.HttpInterceptor;
using SimpleShop.Client.HttpRepository.Interfaces;
using SimpleShop.Client.Models;
using SimpleShop.Shared.Products.Dtos;

namespace SimpleShop.Client.Pages;

// jeśli używany Interceptor to klasa musi implementować IDisposable
public partial class Home : IDisposable
{
	// wyłączenie prerenderowania
	//static IComponentRenderMode _renderMode = new InteractiveAutoRenderMode(prerender: false);

	private PaginationInfo _paginationInfo = new();

	private IEnumerable<ProductDto> _products;

	private bool _isLoading = false;

	//private string _userName;

	// do pobierania danych przez WebApi
	[Inject]
	public IProductHttpRepository ProductRepo { get; set; }

	[Inject]
	public HttpInterceptorService Interceptor { get; set; }

	// potrzebne żeby dostać się do danych użytkownika w kodzie C#
	// żeby używać tego parametru CascadingParameter, musi być dodany znacznik CascadingAuthenticationState w DependencyInjection.cs lub na widoku
	[CascadingParameter]
	public Task<AuthenticationState> AuthState { get; set; }

	// parametry przekazywane do WebApi
	// numer wyświetlanej strony
	public int PageNumber { get; set; } = 1;
	// kolejność sortowania
	public string OrderInfo { get; set; }
	// filtrowanie danych
	public string SearchValue { get; set; }

	protected override async Task OnInitializedAsync()
	{
		Interceptor.RegisterAfterSendEvent();
		await RefreshProducts();

		//var authState = await AuthState;
		//_userName = authState.User.FindFirst(ClaimTypes.Name).Value;
	}

	private async Task OnSelectedPage(int pageNumber)
	{
		PageNumber = pageNumber;
		await RefreshProducts();
	}

	private async Task RefreshProducts()
	{
		_isLoading = true;

		try
		{
			var paginatedList = await ProductRepo.GetAll(PageNumber, OrderInfo, SearchValue);

			_products = paginatedList.Items;
			_paginationInfo = new PaginationInfo
			{
				PageIndex = paginatedList.PageIndex,
				TotalCount = paginatedList.TotalCount,
				TotalPages = paginatedList.TotalPages
			};
		}
		finally
		{
			_isLoading = false;
		}
	}

	private async Task OnSearchValueChanged(string searchValue)
	{
		SearchValue = searchValue;
		PageNumber = 1;
		await RefreshProducts();
	}

	private async Task OnSortChanged(string orderInfo)
	{
		OrderInfo = orderInfo;
		PageNumber = 1;
		await RefreshProducts();
	}

	public void Dispose()
		=> Interceptor.DisposeEvent();
}