namespace BlazorWebAssembly.Pages;

public partial class Counter
{
	private int _currentCount = 0;
	private bool _isActive = false;

	private void IncrementCount()
		=> _currentCount++;

	private void ToggleActive()
		=> _isActive = !_isActive;
}