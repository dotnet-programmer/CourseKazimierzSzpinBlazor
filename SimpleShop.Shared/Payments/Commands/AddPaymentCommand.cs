using MediatR;

namespace SimpleShop.Shared.Payments.Commands;

public class AddPaymentCommand : IRequest<string>
{
	// cena, która ma zostać zapłacona
	public decimal Value { get; set; }

	// adres Url klienta, czyli adres Url aplikacji Blazor
	public string ClientUrl { get; set; }
}