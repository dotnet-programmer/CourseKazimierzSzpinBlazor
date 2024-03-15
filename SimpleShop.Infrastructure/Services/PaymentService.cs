using SimpleShop.Application.Common.Interfaces;
using Stripe.Checkout;

namespace SimpleShop.Infrastructure.Services;

internal class PaymentService : IPaymentService
{
	public string Create(string clientUrl, decimal value)
	{
		var options = new SessionCreateOptions
		{
			SuccessUrl = $"{clientUrl}potwierdzenie",
			LineItems =
			[
				new SessionLineItemOptions
				{
					Quantity = 1,
					PriceData = new SessionLineItemPriceDataOptions
					{
						Currency = "pln",
						UnitAmount = (long)(value * 100),
						ProductData = new SessionLineItemPriceDataProductDataOptions
						{
							Name = "Rower 1"
						}
					}
				},
			],
			Mode = "payment"
		};

		var service = new SessionService();
		var session = service.Create(options);

		return session.Id;
	}

	public bool IsPaid(string sessionId)
	{
		var service = new SessionService();
		var sessionDetails = service.Get(sessionId);
		return sessionDetails.PaymentStatus == "paid";
	}
}