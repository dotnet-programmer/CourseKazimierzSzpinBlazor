using BlazorWebAssembly.Models;

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
}