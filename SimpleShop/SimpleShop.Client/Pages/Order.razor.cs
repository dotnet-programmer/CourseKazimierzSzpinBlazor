﻿using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using SimpleShop.Client.HttpInterceptor;
using SimpleShop.Client.HttpRepository.Interfaces;
using SimpleShop.Shared.Orders.Commands;
using SimpleShop.Shared.Products.Dtos;

namespace SimpleShop.Client.Pages;

public partial class Order : IDisposable
{
	private readonly AddOrderCommand _command = new() { UserId = "1" };

	[Inject]
	public IOrderHttpRepository OrderRepo { get; set; }

	[Inject]
	public NavigationManager NavigationManager { get; set; }

	[Inject]
	public ILocalStorageService LocalStorage { get; set; }

	[Inject]
	public HttpInterceptorService Interceptor { get; set; }

	// przechwycenie wszystkich requestów do API
	protected override async Task OnInitializedAsync()
		=> Interceptor.RegisterEvent();

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
		}
	}

	private async Task Save()
	{
		if (_command.Value <= 0)
		{
			return;
		}

		await OrderRepo.Add(_command);
	}

	public void Dispose()
		=> Interceptor.DisposeEvent();
}