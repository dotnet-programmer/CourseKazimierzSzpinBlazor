namespace SimpleShop.Application.Common.Interfaces;

public interface IPaymentService
{
	string Create(string clientUrl, decimal value);
	bool IsPaid(string sessionId);
}