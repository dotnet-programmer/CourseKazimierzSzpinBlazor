using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Shared.Orders.Commands;

namespace SimpleShop.Application.Orders.Commands;

internal class ConfirmOrderCommandHandler(IApplicationDbContext context, IPaymentService paymentService) : IRequestHandler<ConfirmOrderCommand>
{
	public async Task Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
	{
		if (!paymentService.IsPaid(request.SessionId))
		{
			throw new Exception("Payment pending...");
		}

		var orderToConfirm = await context.Orders.FirstOrDefaultAsync(x => x.SessionId == request.SessionId, cancellationToken);

		if (orderToConfirm == null)
		{
			return;
		}

		orderToConfirm.IsPaid = true;

		await context.SaveChangesAsync(cancellationToken);
	}
}