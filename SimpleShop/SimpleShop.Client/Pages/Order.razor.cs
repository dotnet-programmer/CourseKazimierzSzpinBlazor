using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using SimpleShop.Client.HttpInterceptor;
using SimpleShop.Client.HttpRepository.Interfaces;
using SimpleShop.Shared.Orders.Commands;
using SimpleShop.Shared.Payments.Commands;
using SimpleShop.Shared.Products.Dtos;

namespace SimpleShop.Client.Pages;

public partial class Order : IDisposable
{
	// wyłączenie prerenderingu żeby można było używać zalogowanych użytkowników
	static IComponentRenderMode _renderMode = new InteractiveAutoRenderMode(prerender: false);

	private readonly AddOrderCommand _command = new() { UserId = "1" };

	[Inject]
	public IOrderHttpRepository OrderRepo { get; set; }

	[Inject]
	public NavigationManager NavigationManager { get; set; }

	[Inject]
	public ILocalStorageService LocalStorage { get; set; }

	[Inject]
	public HttpInterceptorService Interceptor { get; set; }

	[Inject]
	public IPaymentHttpRepository PaymentRepo { get; set; }

	[Inject]
	public IJSRuntime JS { get; set; }

	[CascadingParameter]
	public Task<AuthenticationState> AuthState { get; set; }

	// przechwycenie wszystkich requestów do API
	protected override async Task OnInitializedAsync()
	{
		Interceptor.RegisterEvent();
		Interceptor.RegisterBeforeSendEvent();
	}

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			var products = await LocalStorage.GetItemAsync<List<ProductDto>>("cart");

			if (products == null && !products.Any())
			{
				NavigationManager.NavigateTo("/");
			}

			_command.Value = products.Select(x => x.Price).Sum();

			// wstawienie adresu email zalogowanego użytkownika w formularzu
			var authState = await AuthState;
			var user = authState.User;
			if (user.Identity.IsAuthenticated)
			{
				_command.UserEmail = user.FindFirst(ClaimTypes.Name).Value;
				StateHasChanged();
			}
		}
	}

	private async Task Save()
	{
		if (_command.Value <= 0)
		{
			return;
		}

		var sesionId = await PaymentRepo.Add(new AddPaymentCommand { Value = _command.Value, ClientUrl = NavigationManager.BaseUri });
		_command.SessionId = sesionId;
		await OrderRepo.Add(_command);
		await LocalStorage.SetItemAsync("sessionId", sesionId);
		await JS.InvokeVoidAsync("redirectToCheckout", sesionId);
	}

	public void Dispose()
		=> Interceptor.DisposeEvent();
}