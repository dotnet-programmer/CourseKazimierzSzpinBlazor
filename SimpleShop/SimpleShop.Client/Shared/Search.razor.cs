using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace SimpleShop.Client.Shared;

public partial class Search
{
	[Parameter]
	public EventCallback<string> SearchValueChanged { get; set; }

	public string SearchValue { get; set; }

	//private void OnSearchValueChanged(KeyboardEventArgs e) 
	//	=> SearchValueChanged.InvokeAsync(SearchValue);

	private Timer _timer;

	private void OnSearchValueChanged(KeyboardEventArgs e)
	{
		if (_timer != null)
		{
			_timer.Dispose();
		}

		_timer = new Timer(OnTimerCallback, null, 500, 0);
	}

	private void OnTimerCallback(object sender)
	{
		SearchValueChanged.InvokeAsync(SearchValue);
		_timer.Dispose();
	}
}