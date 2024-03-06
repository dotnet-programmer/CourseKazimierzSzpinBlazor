using Microsoft.AspNetCore.Components;

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
}