using MediatR;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Shared.Payments.Commands;

namespace SimpleShop.Application.Payments.Commands;

internal class AddPaymentCommandHandler(IPaymentService paymentService) : IRequestHandler<AddPaymentCommand, string>
{
	// metoda Create zwraca SessionId
	public async Task<string> Handle(AddPaymentCommand request, CancellationToken cancellationToken)
		=> paymentService.Create(request.ClientUrl, request.Value);
}