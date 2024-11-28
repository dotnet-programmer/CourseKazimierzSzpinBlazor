using BlazorWebAssembly.Models;

namespace BlazorWebAssembly.Pages;

public partial class KanbanBoard
{
	private Item _newItem = new();
	private Item _currentItem;
	private List<Item> _items = [];

	protected override void OnInitialized() => _items =
		[
			new Item
			{
				Description = "Logowanie użytkowników",
				State = State.New
			},
			new Item
			{
				Description = "Rejestracja użytkowników",
				State = State.Active
			},
			new Item
			{
				Description = "Fakturowanie",
				State = State.Closed
			},
			new Item
			{
				Description = "Płatności",
				State = State.Closed
			},
		];

	private void OnDrop(State state) => _currentItem.State = state;

	private void OnStartDrag(Item item) => _currentItem = item;

	private void Save()
	{
		_newItem.State = State.New;
		_items.Add(_newItem);

		_newItem = new();
	}
}
