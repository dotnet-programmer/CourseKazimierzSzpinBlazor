namespace SimpleShop.Client.Models;

// każdy przycisk na paginacji będzie modelem PaginationLink
public class PaginationLink
{
	public string Text { get; set; }
	public int PageIndex { get; set; }
	public bool Enabled { get; set; }
	public bool Active { get; set; }
}