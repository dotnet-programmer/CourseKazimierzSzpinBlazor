using BlazorServer.Data;

namespace BlazorServer.Pages;

public partial class FetchData
{
	private WeatherForecast[]? _forecasts;

	protected override async Task OnInitializedAsync()
		=> _forecasts = await ForecastService.GetForecastAsync(DateOnly.FromDateTime(DateTime.Now));
}