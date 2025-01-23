namespace SimpleShop.Client.Models;

// informacje o paginacji, te same info jest zwracane z web api
public class PaginationInfo
{
	public int PageIndex { get; set; }
	public int TotalPages { get; set; }
	public int TotalCount { get; set; }
	public bool HasPreviousPage => PageIndex > 1;
	public bool HasNextPage => PageIndex < TotalPages;
}