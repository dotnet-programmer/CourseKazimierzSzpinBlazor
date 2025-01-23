using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SimpleShop.Application.Common.Exceptions;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Domain.Entities;
using SimpleShop.Shared.Authentication.Commands;

namespace SimpleShop.Application.Authentication.Commands;

// resetowanie hasła użytkownika i ustawienie nowego
public class ResetPasswordCommandHandler(UserManager<ApplicationUser> userManager, IApplicationDbContext context) : IRequestHandler<ResetPasswordCommand>
{
	public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
	{
		var user = await userManager.FindByEmailAsync(request.Email);

		if (user == null)
		{
			throw new ValidationException("Nieprawidłowe dane.");
		}

		var resetPassResult = await userManager.ResetPasswordAsync(user, request.Token, request.Password);

		if (!resetPassResult.Succeeded)
		{
			var errors = resetPassResult.Errors.Select(e => new ValidationFailure { PropertyName = e.Code, ErrorMessage = e.Description });
			throw new ValidationException(errors);
		}

		await userManager.SetLockoutEndDateAsync(user, null);
	}
}