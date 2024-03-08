using System.Text.Json;
using BlazorWebAssembly.Models;
using BlazorWebAssembly.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorWebAssembly.Pages;

public partial class Forms
{
	private Employee _employee = new Employee { DateOfEmployment = DateTime.Now };

	private string _header = "Formularz";
	private string _name;

	private bool _isLoading = false;

	// to jest onew-way binding - zmiana w kodzie powoduje zmianę na widoku
	private void ChangeHeader()
	{
		_header = "Nowy nagłówek";
	}

	private IEnumerable<Position> _positions = new List<Position>()
	{
		new Position { Id = 1, Name = "It" },
		new Position { Id = 2, Name = "Magazynier" },
		new Position { Id = 3, Name = "Kierowca" },
	};

	[Inject]
	public IToastrService ToastrService { get; set; }
	private async Task Save()
	{
		try
		{
			_isLoading = true;
			await Task.Delay(3000);

			//logika zapisu obiektu _employee do bazy danych

			var json = JsonSerializer.Serialize(_employee);
			await ToastrService.ShowInfoMessage($"Dane zostały zapisane. Użytkownik: {json}");
			_employee = new Employee { DateOfEmployment = DateTime.Now };
		}
		//catch (Exception)
		//{
		//	// obsługa wyjątku
		//	throw;
		//}
		finally
		{
			_isLoading = false;

		}
	}
}