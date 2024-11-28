using BlazorWebAssembly.Models;
using Microsoft.AspNetCore.Components;

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

	protected override void OnInitialized() =>
		//switch (State)
		//{
		//	case State.New:
		//		_classes = "alert-warning";
		//		break;
		//	case State.Active:
		//		_classes = "alert-primary";
		//		break;
		//	case State.Closed:
		//		_classes = "alert-success";
		//		break;
		//	default:
		//		break;
		//}

		_classes = State switch
		{
			State.New => "alert-warning",
			State.Active => "alert-primary",
			State.Closed => "alert-success",
			_ => string.Empty
		};

	// jeżeli zadanie zostanie upuszczone
	private void OnDropHandler() =>
		// przekazanie na jakiej kolumnie zostało upuszczone, czyli z jakim statusem,
		// wtedy komponent nadrzędny będzie mógł dla tego itema ustawić taki status
		OnDrop.InvokeAsync(State);

	// złapanie danego zadania
	private void OnStartDragHandler(Item item) =>
		// przekazanie do komponentu nadrzędnego, które zadanie zostało złapane
		OnStartDrag.InvokeAsync(item);
}
