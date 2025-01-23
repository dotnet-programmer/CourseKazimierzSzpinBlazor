using MediatR;
using Microsoft.AspNetCore.Identity;
using SimpleShop.Application.Common.Exceptions;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Domain.Entities;
using SimpleShop.Shared.Authentication.Commands;

namespace SimpleShop.Application.Authentication.Commands;

// potwierdzenie adresu email
public class EmailConfirmationCommandHandler(UserManager<ApplicationUser> userManager, IApplicationDbContext context) : IRequestHandler<EmailConfirmationCommand>
{
	public async Task Handle(EmailConfirmationCommand request, CancellationToken cancellationToken)
	{
		var user = await userManager.FindByEmailAsync(request.Email);

		if (user == null)
		{
			throw new ValidationException();
		}

		if (user.EmailConfirmed)
		{
			return;
		}

		var confirmResult = await userManager.ConfirmEmailAsync(user, request.Token);

		if (!confirmResult.Succeeded)
		{
			throw new ValidationException();
		}
	}
}