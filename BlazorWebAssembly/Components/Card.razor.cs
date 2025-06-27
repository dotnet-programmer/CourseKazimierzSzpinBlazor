using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorWebAssembly.Components;

public partial class Card
{
	// żeby przekazać parametry do komponentu trzeba zrobić właściwość z atrybutem [Parameter]
	// jako parametr do komponentu można przekazać szablony czyli kod w HTML,
	// czyli lista, tabela, nagłówek itp jako parametr, który zostanie wyrenderowany wewnątrz komponentu
	// w tym przypadku trzeba użyć typu RenderFragment zamiast np. string
	[Parameter]
	public string Image { get; set; }
	[Parameter]
	public RenderFragment Title { get; set; }
	[Parameter]
	public RenderFragment Content { get; set; }
	[Parameter]
	public string BtnText { get; set; }

	// do komponentów można przekazywać różne atrybuty HTML i jest na to kilka sposobów
	// pierwszy - najprostszy - dodanie nowych parametrów, wada tego rozwiązania że trzeba później każdy taki atrybut przypisywać osobno
	[Parameter]
	public string Style { get; set; }
	[Parameter]
	public string BtnClass { get; set; } = "btn btn-primary";
	[Parameter]
	public bool BtnDisabled { get; set; }
	// atrybut title w THML oznacza dymek po najechaniu kursorem
	[Parameter]
	public string BtnTitle { get; set; }

	// druga metoda to przekazanie słownika C# z parami klucz-wartość, gdzie klucz to string, a wartość to object
	// taki słownik jest utworzony w komponencie nadrzędnym w jego code-behind

	// można też pozwolić na przekazanie dowolnych atrybutów komponentu nadrzędnego, nawet takich, które nie są zaimplementowane w komponencie
	// dzięki temu wszystkie przekazane tutaj atrybuty zostaną wstawione do odpowiednio oznaczonego buttona
	[Parameter(CaptureUnmatchedValues = true)]
	public Dictionary<string, object> BtnAttributes { get; set; }

	// parametr kaskadowy
	private readonly string _info = "Komunikat z CARD";
	private readonly string _title = "Tytuł z CARD";

	// zdarzenie w komponencie
	// komponent wywołujący (nadrzędny) jako parametr może przekazać metodę, która ma zostać wywołana po kliknięciu danego przycisku
	// tutaj właściwość typu EventCallback na tą metodę
	[Parameter]
	public EventCallback OnClickMore { get; set; }
	// zazwyczaj potrzeba poinformować komponent nadrzędny, czyli ten wywołujący dany komponent, o tym że dany przycisk został kliknięty
	private void ClickMore(MouseEventArgs e)
		// poinformowanie elementu nadrzędnego że ten przycisk został kliknięty,
		// umożliwia to też komponentowi nadrzędnemu obsługę swojej metody po kliknięciu na ten przycisk
		=> OnClickMore.InvokeAsync();

	// jeżeli komponent wywołujący przekazuje jakieś parametry do metody, to trzeba użyć typa generycznego
	[Parameter]
	public EventCallback<string> OnClickMoreString { get; set; }
	private void ClickMoreString(MouseEventArgs e)
		// przekazanie argumentu do wywoływanej metody
		=> OnClickMoreString.InvokeAsync("ABC");

	// jeżeli logika komponentu niczego nie robi w wywoływanej metodzie, to można pominąć tworzenie metody i przypisać tu bezpośrednio właściwość typu EventCallback
	[Parameter]
	public EventCallback JustParameterWithoutMethod { get; set; }
	// jeżeli logika komponentu niczego nie robi w wywoływanej metodzie, to można pominąć tworzenie metody i przypisać tu bezpośrednio właściwość typu EventCallback
	// dodatkowo jeśli są przekazywane parametry wywołania,
	// trzeba dodać w typie generycznym parametry które są przekazywane dla konkretnego zdarzenia
	// w nadrzędnym komponenie trzeba też dodać takie sam parametr do metody
	public EventCallback<MouseEventArgs> JustParameterWithoutMethodWithParameter { get; set; }



	// cykl życia komponentu
	// kolejność metod wywoływanych podczas cyklu życia komponentu
	// ta metoda ustawia parametry, które zostały przekazane przez nadrzędny komponent
	public override async Task SetParametersAsync(ParameterView parameters)
	{
		Console.WriteLine("SetParametersAsync");
		await base.SetParametersAsync(parameters);
	}
	// ta metoda jest wywoływana po tym gdy parametry zostały już ustawione, a komponent został prawidłowo zainicjalizowany
	// tutaj robiona jest inicjalizacja moich obiektów, pobieranie danych z bazy danych itd
	protected override async Task OnInitializedAsync()
	{
		Console.WriteLine("OnInitializedAsync");
		await base.OnInitializedAsync();
	}
	// ta metoda jest wywoływana po tym jak komponent został zainicjalizowany i za każdym razem gdy parametry zostaną zmienione i komponent zostanie ponownie przerenderowany
	protected override async Task OnParametersSetAsync()
	{
		Console.WriteLine("OnParametersSetAsync");
		await base.OnParametersSetAsync();
	}
	// ta metoda zostanie wywołana gdy komponent kończy renderowanie,
	// dodatkowo na podstawie wartości parmetru firstRender można zweryfikować czy było to pierwsze renderowanie (firstRender=true) czy kolejne (firstRender=false)
	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			Console.WriteLine("OnAfterRenderAsync - firstRender");
			// żeby wymusić odświeżenie komponentu wystarczy wywołaś funkcję StateHasChanged();
		}
		Console.WriteLine("OnAfterRenderAsync");
		await base.OnAfterRenderAsync(firstRender);
	}



	// odwołanie do podrzędnego komponentu
	private string _additionalCardClasses = string.Empty;
	public void AddCardBorderFromCard()
		=> _additionalCardClasses = "border-3 border-success";
}