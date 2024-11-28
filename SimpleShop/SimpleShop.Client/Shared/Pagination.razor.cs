using Microsoft.AspNetCore.Components;
using SimpleShop.Client.Models;

namespace SimpleShop.Client.Shared;

public partial class Pagination
{
	private List<PaginationLink> _links;

	// ilość przycisków w górę i w dół od aktualnej pozycji
	private readonly int _linkCount = 2;

	[Parameter]
	public PaginationInfo PaginationInfo { get; set; }

	[Parameter]
	public EventCallback<int> SelectedPage { get; set; }

	// wywołuje się zawsze gdy parametry zostaną zmienione
	protected override void OnParametersSet()
	{
		_links =
		[
			//poprzednia
			new PaginationLink { Text = "Poprzednia", PageIndex = PaginationInfo.PageIndex - 1, Enabled = PaginationInfo.HasPreviousPage },
		];

		//1 2 3 - konkretny przycisk dla każdej z podstron
		for (int i = 1; i <= PaginationInfo.TotalPages; i++)
		{
			if (PaginationInfo.PageIndex - _linkCount <= i && PaginationInfo.PageIndex + _linkCount >= i)
			{
				_links.Add(new PaginationLink { Text = i.ToString(), PageIndex = i, Enabled = true, Active = PaginationInfo.PageIndex == i });
			}
		}

		//następna
		_links.Add(new PaginationLink { Text = "Następna", PageIndex = PaginationInfo.PageIndex + 1, Enabled = PaginationInfo.HasNextPage });
	}

	private async Task OnSelectedPage(PaginationLink item)
	{
		if (item.PageIndex == PaginationInfo.PageIndex || !item.Enabled)
		{
			return;
		}

		PaginationInfo.PageIndex = item.PageIndex;

		await SelectedPage.InvokeAsync(item.PageIndex);
	}
}