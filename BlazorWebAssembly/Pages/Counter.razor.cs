using BlazorWebAssembly.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorWebAssembly.Pages;

public partial class Counter
{
	private int _currentCount = 0;
	private bool _isActive = false;

	// Parametr przekazywanego do strony poprzez ścieżkę:
	[Parameter]
	public int Header { get; set; }

	[Parameter]
	public bool? IsDeleted { get; set; }

	// ta właściwość z takm atrybutem służy do przekazywania parametrów przez adres
	// doklejając po znaku zapytania jego nazwę z przypisaniem wartości:
	// localhost/counter?arg1=1
	[SupplyParameterFromQuery(Name = "Header2")]
	public string Header2 { get; set; }

	// atrybut Dependency Injection [Inject] - dodać aby użyć w klasie implementacji interfejsu/serwisu
	// Oznacza wstrzyknięcie/użycie implementacji serwisu zarejestrowanego w Program.cs:
	//		builder.Services.AddScoped<IStudentRepo, StudentRepo>();
	[Inject]
	public IStudentRepo StudentRepo { get; set; }

	private void AddStudent()
		=> StudentRepo.Add();

	private void IncrementCount()
	{
		try
		{
			_currentCount++;
			throw new Exception("Error 123");
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}

	private void ToggleActive()
		=> _isActive = !_isActive;

	#region Navigation Manager - przemieszczanie się w kodzie między stronami

	// atrybut [Inject] - używanie DI
	// nie trzeba było rejestrować tego serwisu w Program.cs bo jest on wbudowany w Blazora i od razu dostępny
	[Inject]
	public NavigationManager NavigationManager { get; set; }

	// powrót na stronę główną
	private void GoToIndex()
		=> NavigationManager.NavigateTo("/");

	// przejdź na stronę "counter-parameter" podając 2 argumenty: 999 i true
	private void GoToSiteWithParameters()
		=> NavigationManager.NavigateTo("/counter-parameter/666/false");

	private void GoToSiteWithParametersFromQuery()
		=> NavigationManager.NavigateTo("counter?header2=69");

	#endregion Navigation Manager - przemieszczanie się w kodzie między stronami
}