using System.ComponentModel;
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorWebAssembly.Components;

public partial class Card
{
	//żeby przekazać parametry do komponentu trzeba zrobić właściwość z atrybutem [Parameter]
	[Parameter]
    public string Image { get; set; }

	[Parameter]
	public string Title { get; set; }

	[Parameter]
	public string Content { get; set; }

	[Parameter]
	public string BtnText { get; set; }

	// parametr kaskadowy
	private string _info = "Komunikat z CARD";

	// zdarzenie w komponencie
	// komponent wywołujący jako parametr może przekazać metodę, która ma zostać wywołana po kliknięciu danego przycisku
	// tutaj właściwość typu EventCallback na tą metodę
	[Parameter]
	public EventCallback OnClickMore { get; set; }
	// zazwyczaj potrzeba poinformować komponent nadrzędny, czyli ten wywołujący dany komponent, o tym że dany przycisk został kliknięty
	private void ClickMore(MouseEventArgs e)
	{
		// logika

		// poinformowanie elementu nadrzędnego że ten przycisk został kliknięty
		OnClickMore.InvokeAsync();
	}
	// jeżeli komponent wywołujący przekazuje jakieś parametry do metody, to trzeba użyć typa generycznego
	[Parameter]
	public EventCallback<string> OnClickMoreString { get; set; }
	private void ClickMoreString(MouseEventArgs e)
	{
		// przekazanie argumentu do wywoływanej metody
		OnClickMoreString.InvokeAsync("ABC");
	}
	// jeżeli logika komponentu niczego nie robi w wywoływanej metodzie, to można pominąć tworzenie metody i przypisać tu bezpośrednio właściwość typu EventCallback
	[Parameter]
	public EventCallback JustParameterWithoutMethod { get; set; }
	// jeżeli logika komponentu niczego nie robi w wywoływanej metodzie, to można pominąć tworzenie metody i przypisać tu bezpośrednio właściwość typu EventCallback
	// dodatkowo jeśli są przekazywane parametry wywołania,
	// trzeba dodać w typie generycznym parametry które są przekazywane dla konkretnego zdarzenia
	// w nadrzędnym komponenie trzeba też dodać takie sam parametr do metody
	public EventCallback<MouseEventArgs> JustParameterWithoutMethodWithParameter { get; set; }
}