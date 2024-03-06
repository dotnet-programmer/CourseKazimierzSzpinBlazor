using Microsoft.AspNetCore.Components;

namespace BlazorWebAssembly.Components;

public partial class AdditionalInfo
{
	// parametr kaskadowy, można go przekazać z dowolnego elementu nadrzędnego do tego komponentu
	[CascadingParameter(Name = "Info")]
    public string Info { get; set; }

	// jeżeli trzeba przekazać więcej niż 1 parametr to nie wystarczy dodać nowej właściwości:
	//[CascadingParameter]
	//public string Title { get; set; }
	// w taki sposób nie zadziała
	// trzeba dodać we wszystkich atrybutach parametr Name i jawnie określić jego nazwę
	[CascadingParameter(Name = "Title")]
	public string Title { get; set; }
}