using MediatR;
using Microsoft.AspNetCore.Identity;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Domain.Entities;
using SimpleShop.Shared.Authentication.Commands;
using SimpleShop.Shared.Authentication.Dtos;

namespace SimpleShop.Application.Authentication.Commands;

// logowanie do aplikacji
public class LoginUserCommandHandler(
	UserManager<ApplicationUser> userManager,
	IEmail emailSender,
	IAuthenticationService authenticationService,
	IApplicationDbContext context) : IRequestHandler<LoginUserCommand, LoginUserDto>
{
	public async Task<LoginUserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
	{
		LoginUserDto badResult = new();

		var user = await userManager.FindByEmailAsync(request.Email);

		if (user == null)
		{
			badResult.ErrorMessage = "Nieprawidłowe dane.";
			return badResult;
		}

		if (!await userManager.IsEmailConfirmedAsync(user))
		{
			badResult.ErrorMessage = "E-mail nie został jeszcze potwierdzony.";
			return badResult;
		}

		if (await userManager.IsLockedOutAsync(user))
		{
			badResult.ErrorMessage = "Konto zostało zablokowane.";
			return badResult;
		}

		if (!await userManager.CheckPasswordAsync(user, request.Password))
		{
			await userManager.AccessFailedAsync(user);

			// po kilku nieudanych próbach logowania konto zostaje zablokowane
			if (await userManager.IsLockedOutAsync(user))
			{
				var body = $"<p><span style=\"font-size: 14px;\">W związku z niewłaściwymi próbami zalogowania na Twoje konto, zostało ono tymczasowo zablokowane (na 15 minut). Jeżeli chcesz zresetować hasło, użyj funkcji 'zapomniałem hasła' na stronie logowania.</span></p><p><span style=\"font-size: 14px;\">Pozdrawiam,</span><br /><span style=\"font-size: 14px;\">Kazimierz Szpin.</span><br /><span style=\"font-size: 14px;\">SimpleShop.pl</span>";

				await emailSender.Send(
					"Informacje dotyczące zablokowanego konta.",
					body,
					request.Email);

				badResult.ErrorMessage = "Konto jest zablokowane.";
				return badResult;
			}

			badResult.ErrorMessage = "Nieprawidłowe dane.";
			return badResult;
		}

		var token = await authenticationService.GetToken(user);

		user.RefreshToken = authenticationService.GenerateRefreshToken();
		user.RefreshTokenExpiryTime = DateTime.Now.AddDays(14);
		await userManager.UpdateAsync(user);
		await userManager.ResetAccessFailedCountAsync(user);

		return new LoginUserDto
		{
			IsAuthSuccessful = true,
			Token = token,
			RefreshToken = user.RefreshToken
		};
	}
}