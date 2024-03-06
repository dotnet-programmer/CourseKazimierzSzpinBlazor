using BlazorWebAssembly.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorWebAssembly.Pages;

public partial class MyComponents
{
	// utworzenie listy autorów - normalnie pobieranie z bazy danych,
	// potrzebne do pokazania użycia przekazania parametrów do komponentu w pętli
	private readonly List<CardModel> _authors =
	[
		new CardModel { Title = "Jan Kowalski", Content = "Programista C#/.NET z 20 letnim doświadczeniem.Specjalizacje: Blazor i ASP.NET Core", Image = "/files/kowalski.png", BtnText = "Więcej"},
		new CardModel { Title = "Anna Nowak", Content = "Programista C#/.NET z 10 letnim doświadczeniem.Specjalizacje: WPF", Image = "/files/nowak.png", BtnText = "Więcej"},
		new CardModel { Title = "Błażej Kwiatkowski", Content = "Programista C#/.NET z 3 letnim doświadczeniem.Specjalizacje: Frontend", Image = "/files/kwiatkowski.png", BtnText = "Więcej" }
	];

	// parametr kaskadowy
	private string _info = "Komunikat - parametr kaskadowy";
	// więcej niż 1 parametr kaskadowy 
	private string _title = "Tytuł - kolejny parametr kaskadowy";

	// zdarzenia w komponentach
	[Inject]
	public NavigationManager NavigationManager { get; set; }
	// metoda która ma zostać wykonana w komponencie nadrzędnym po kliknięciu przycisku w komponencie podrzędnym
	private void MyClickMore()
	{
		// po kliknięciu przekierowanie na stronę główną
		NavigationManager.NavigateTo("/");
	}
	// metoda z parametrem, która ma zostać wywołana w komponencie podrzędnym
	private void MyClickMoreString(string message)
	{
		// po kliknięciu przekierowanie na stronę główną
		NavigationManager.NavigateTo($"/{message}");
	}
	// jeżeli są parametry a w komponencie podrzędnym nie ma metody tylko bezpośrednie przypisanie właściwości wywołującej metodę, 
	// to trzeba tu dodać parametr odpowiadający typowi konkretnego zdarzenia
	private void MyClickMoreWithoutMethodWithParameter(MouseEventArgs e)
	{
		// po kliknięciu przekierowanie na stronę główną
		NavigationManager.NavigateTo("/");
	}

	// przekazanie atrybutów HTML
	// wywołanie w HTML: @attributes="_cardAttributes"
	private Dictionary<string, object> _cardAttributes = new()
	{
		{ "BtnClass", "btn btn-success" },
		{ "BtnTitle", "Więcej..." },
		{ "BtnDisabled", false },
		{ "Style", "" },
	};

	// można też pozwolić na przekazanie dowolnych atrybutów komponentu nadrzędnego, nawet takich, które nie są zaimplementowane w komponencie
	// dzięki temu wszystkie przekazane tutaj atrybuty zostaną wstawione do odpowiednio oznaczonego buttona
	private Dictionary<string, object> _cardBtnAttributes = new()
	{
		{ "class", "btn btn-success" },
		{ "title", "Więcej..." },
		{ "disabled", false },
		{ "type", "button" },
	};
}