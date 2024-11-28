using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace SimpleShop.Client.Shared;

public partial class Search
{
	//private Timer _timer;

	[Parameter]
	public EventCallback<string> SearchValueChanged { get; set; }

	public string SearchValue { get; set; }

	private void OnSearchValueChanged(KeyboardEventArgs e) => SearchValueChanged.InvokeAsync(SearchValue);

	// TODO - wersja z timerem powoduje błąd:
	// The current thread is not associated with the Dispatcher. Use InvokeAsync() to switch execution to the Dispatcher when triggering rendering or component state.
	//private void OnSearchValueChanged(KeyboardEventArgs e)
	//{
	//	if (_timer != null)
	//	{
	//		_timer.Dispose();
	//	}

	//	_timer = new Timer(OnTimerCallback, null, 500, 0);
	//}

	//private void OnTimerCallback(object sender)
	//{
	//	SearchValueChanged.InvokeAsync(SearchValue);

	//	_timer.Dispose();
	//}
}