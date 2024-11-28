using System.ComponentModel.DataAnnotations;

namespace BlazorWebAppSSR;

public class Employee
{
	[Required]
	public string Name { get; set; }
}
