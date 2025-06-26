namespace BlazorServer.Pages;

public partial class Counter
{
	private int _currentCount = 0;
	private int _anotherCount = 0;

	private void IncrementCount()
		=> _currentCount++;
}