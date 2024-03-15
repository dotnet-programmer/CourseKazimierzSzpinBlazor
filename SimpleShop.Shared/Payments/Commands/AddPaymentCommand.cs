using MediatR;

namespace SimpleShop.Shared.Payments.Commands;

public class AddPaymentCommand : IRequest<string>
{
	public decimal Value { get; set; }
	public string ClientUrl { get; set; }
}