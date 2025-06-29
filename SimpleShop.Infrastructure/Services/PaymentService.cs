using SimpleShop.Application.Common.Interfaces;
using Stripe.Checkout;

namespace SimpleShop.Infrastructure.Services;

internal class PaymentService : IPaymentService
{
	public string Create(string clientUrl, decimal value)
	{
		SessionCreateOptions options = new()
		{
			// adres Url, który ma zostać wywołany po dokonaniu płatności (tutaj adres Blazora + doklejone "potwierdzenie")
			SuccessUrl = $"{clientUrl}potwierdzenie",

			// produkty, za które będzie realizowana płatność
			LineItems =
			[
				// to powinno być pobierane z koszyka, na razie na sztywno wpisany 1 produkt
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

		SessionService service = new();
		var session = service.Create(options);

		// identyfikator sesji, czyli płatności
		return session.Id;
	}

	// na podstawie przekazanego identyfikatora sesji zwraca info czy zamówienie jest zapłacone 
	public bool IsPaid(string sessionId)
	{
		SessionService service = new();
		var sessionDetails = service.Get(sessionId);
		return sessionDetails.PaymentStatus == "paid";
	}
}