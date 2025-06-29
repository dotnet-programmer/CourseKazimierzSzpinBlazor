namespace SimpleShop.Client.Models;

// każdy przycisk na paginacji (poprzednia, następna, 1, 2, 3 itp) będzie modelem PaginationLink
public class PaginationLink
{
	public string Text { get; set; }
	public int PageIndex { get; set; }
	public bool Enabled { get; set; }
	public bool Active { get; set; }
}