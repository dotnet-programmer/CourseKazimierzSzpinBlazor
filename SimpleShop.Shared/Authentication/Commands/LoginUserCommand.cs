using System.ComponentModel.DataAnnotations;
using MediatR;
using SimpleShop.Shared.Authentication.Dtos;

namespace SimpleShop.Shared.Authentication.Commands;

public class LoginUserCommand : IRequest<LoginUserDto>
{
	[Required(ErrorMessage = "Pole 'E-mail' jest wymagane.")]
	[EmailAddress(ErrorMessage = "Pole 'E-mail' musi być prawidłowym adresem e-mail.")]
	public string Email { get; set; }

	[Required(ErrorMessage = "Pole 'Hasło' jest wymagane.")]
	public string Password { get; set; }
}