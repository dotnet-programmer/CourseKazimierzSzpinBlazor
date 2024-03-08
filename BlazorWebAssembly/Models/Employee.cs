using System.ComponentModel.DataAnnotations;

namespace BlazorWebAssembly.Models;

public class Employee
{
	public int Id { get; set; }
	[Required(ErrorMessage = "Pole 'Nazwa' jest wymagane")]
	public string Name { get; set; }
	public string Notes { get; set; }
	public DateTime DateOfEmployment { get; set; }
	public bool IsNew { get; set; }
	public decimal Salary { get; set; }

	[Range(1, int.MaxValue, ErrorMessage = "Pole 'Stanowisko' jest wymagane")]
	public int PositionId { get; set; }
}