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
	// ta właściwość z takm atrybutem służy do przekazywania parametrów przez adres doklejając po znaku zapytania jego nazwę z przypisaniem wartości:
	// localhost/counter-par?arg1=1
	[SupplyParameterFromQuery(Name = "Header2")]
	public string Header2 { get; set; }

	// atrybut DI [Inject] - oznacza wstrzyknięcie/użycie implementacji serwisu zarejestrowanego w Program.cs
	[Inject]
	public IStudentRepo StudentRepo { get; set; }
	private void AddStudent()
		=> StudentRepo.Add();


	private void IncrementCount()
		=> _currentCount++;

	private void ToggleActive()
		=> _isActive = !_isActive;

	// Navigation Manager - przemieszczanie się w kodzie między stronami
	// atrybut [Inject] - używanie DI
	[Inject]
	public NavigationManager NavigationManager { get; set; }
	private void GoToIndex()
		// powrót na stronę główną
		=> NavigationManager.NavigateTo("/");
	private void GoToSiteWithParameters()
		// przejdź na stronę "counter-par" podając 2 argumenty: 999 i true
		=> NavigationManager.NavigateTo("/counter-par/666/false");
}