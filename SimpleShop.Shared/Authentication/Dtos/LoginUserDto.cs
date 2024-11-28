namespace SimpleShop.Shared.Authentication.Dtos;

public class LoginUserDto
{
	// czy wszystko zakończyło się sukcesem
	public bool IsAuthSuccessful { get; set; }
	public string ErrorMessage { get; set; }

	// token dostępu
	public string Token { get; set; }

	public string RefreshToken { get; set; }
}