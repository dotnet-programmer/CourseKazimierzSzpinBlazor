using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Application.Common.Exceptions;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Domain.Entities;
using SimpleShop.Shared.Authentication.Commands;

namespace SimpleShop.Application.Authentication.Commands;

// rejestracja nowego użytkownika
public class RegisterUserCommandHandler(
	UserManager<ApplicationUser> userManager,
	IEmail emailSender,
	IApplicationDbContext context) : IRequestHandler<RegisterUserCommand>
{
	public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
	{
		ApplicationUser user = new()
		{
			UserName = request.Email,
			Email = request.Email
		};

		// sprawdzenie czy dany adres email jest wolny
		var userExists = await context.Users
			.AnyAsync(x => x.Email == user.Email, cancellationToken);

		// jeśli email już zajęty, to informowanie użytkownika
		if (userExists)
		{
			throw new ValidationException("Wybrany email jest już zajęty.");
		}
		// jeśli email jest dostępny to utwórz nowego użytkownika
		var result = await userManager.CreateAsync(user, request.Password);

		// jeśli coś jednak pójdzie nie tak to zwróć te informacje użytkownikowi
		if (!result.Succeeded)
		{
			var errors = result.Errors.Select(e => new ValidationFailure { PropertyName = e.Code, ErrorMessage = e.Description });
			throw new ValidationException(errors);
		}

		// jeśli wszystko pójdzie dobrze, to generowanie tokena wysyłanego w mailu potwierdzającym w celu aktywowania konta
		var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

		Dictionary<string, string> param = new()
			{
				{ "token", token },
				{ "email", request.Email }
			};

		var callback = QueryHelpers.AddQueryString(request.ClientURI, param);

		var body = $"<p><span style=\"font-size: 14px;\">Dzień dobry {user.Email}.</span></p><p><span style=\"font-size: 14px;\">Dziękujemy za założenie konta w aplikacji SimpleShop.pl.</span></p><p><span style=\"font-size: 14px;\">Aby aktywować swoje konto kliknij w poniższy link:</span></p><p><span style=\"font-size: 14px;\"><a href='{callback}'>kliknij tutaj</a></span></p><p><span style=\"font-size: 14px;\">Pozdrawiam,</span><br /><span style=\"font-size: 14px;\">Kazimierz Szpin.</span><br /><span style=\"font-size: 14px;\">SimpleShop.pl</span>";

		await emailSender.Send(
			"Aktywuj swoje konto",
			body,
			user.Email);
	}
}