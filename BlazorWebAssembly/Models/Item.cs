using System.ComponentModel.DataAnnotations;

namespace BlazorWebAssembly.Models;

public class Item
{
	[Required(ErrorMessage = "Pole 'Opis' jest wymagane")]
    public string Description { get; set; }
    public State State { get; set; }
}
