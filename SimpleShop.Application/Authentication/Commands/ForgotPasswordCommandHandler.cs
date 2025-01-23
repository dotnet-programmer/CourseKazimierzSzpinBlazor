using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using SimpleShop.Application.Common.Exceptions;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Domain.Entities;
using SimpleShop.Shared.Authentication.Commands;

namespace SimpleShop.Application.Authentication.Commands;

// resetowanie hasła
public class ForgotPasswordCommandHandler(
	UserManager<ApplicationUser> userManager,
	IEmail emailSender,
	IApplicationDbContext context) : IRequestHandler<ForgotPasswordCommand>
{
	public async Task Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
	{
		var user = await userManager.FindByEmailAsync(request.Email);

		if (user == null)
		{
			throw new ValidationException("Nieprawidłowe dane.");
		}

		// token przesyłany w wiadomości email dla użytkownika
		var token = await userManager.GeneratePasswordResetTokenAsync(user);

		Dictionary<string, string> param = new()
			{
				{ "token", token },
				{ "email", request.Email }
			};

		var callback = QueryHelpers.AddQueryString(request.ClientURI, param);

		var body = $"<p><span style=\"font-size: 14px;\">Dzień dobry {user.Email}.</span></p><p><span style=\"font-size: 14px;\">W celu zrestowania hasła w aplikacji SimpleShop.pl kliknij w poniższy link:</span></p><p><span style=\"font-size: 14px;\"><a href='{callback}'>kliknij tutaj</a></span></p><p><span style=\"font-size: 14px;\">Pozdrawiam,</span><br /><span style=\"font-size: 14px;\">Kazimierz Szpin.</span><br /><span style=\"font-size: 14px;\">SimpleShop.pl</span>";

		await emailSender.Send(
			"Resetowanie hasła",
			body,
			user.Email);
	}
}