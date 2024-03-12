using System.ComponentModel.DataAnnotations;

namespace BlazorWebAssembly.Models;

// statusy notatek
public enum State
{
	[Display(Name = "Nowy")]
	New,

	[Display(Name = "Aktywny")]
	Active,

	[Display(Name = "Zakończony")]
	Closed
}
