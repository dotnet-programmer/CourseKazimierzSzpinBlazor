using MediatR;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Domain.Entities;
using SimpleShop.Shared.Orders.Commands;

namespace SimpleShop.Application.Orders.Commands;

internal class AddOrderCommandHandler(IApplicationDbContext context) : IRequestHandler<AddOrderCommand>
{
	public async Task Handle(AddOrderCommand request, CancellationToken cancellationToken)
	{
		Order order = new()
		{
			IsPaid = false,
			SessionId = request.SessionId,
			UserId = request.UserId,
			Value = request.Value
		};

		context.Orders.Add(order);
		await context.SaveChangesAsync(cancellationToken);
	}
}