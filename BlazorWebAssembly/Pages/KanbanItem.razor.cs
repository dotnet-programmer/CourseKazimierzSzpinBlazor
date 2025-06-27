using BlazorWebAssembly.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorWebAssembly.Pages;

public partial class KanbanItem
{
	private string _classes = string.Empty;

	[Parameter]
	public State State { get; set; }

	[Parameter]
	public List<Item> Items { get; set; } = [];

	[Parameter]
	public EventCallback<State> OnDrop { get; set; }

	[Parameter]
	public EventCallback<Item> OnStartDrag { get; set; }

	protected override void OnInitialized()
		=> _classes = State switch
		{
			State.New => "alert-warning",
			State.Active => "alert-primary",
			State.Closed => "alert-success",
			_ => string.Empty
		};

	// jeżeli zadanie zostanie upuszczone
	// przekazanie na jakiej kolumnie zostało upuszczone, czyli z jakim statusem,
	// wtedy komponent nadrzędny będzie mógł dla tego itema ustawić taki status
	private void OnDropHandler()
		=> OnDrop.InvokeAsync(State);

	// złapanie danego zadania
	// przekazanie do komponentu nadrzędnego, które zadanie zostało złapane
	private void OnStartDragHandler(Item item)
		=> OnStartDrag.InvokeAsync(item);
}