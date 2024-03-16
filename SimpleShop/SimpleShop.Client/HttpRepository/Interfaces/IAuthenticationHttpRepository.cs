namespace SimpleShop.Client.HttpRepository.Interfaces;

public interface IAuthenticationHttpRepository
{
	Task<string> RefreshToken();
}