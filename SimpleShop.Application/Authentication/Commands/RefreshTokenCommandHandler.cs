using MediatR;
using Microsoft.AspNetCore.Identity;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Domain.Entities;
using SimpleShop.Shared.Authentication.Commands;
using SimpleShop.Shared.Authentication.Dtos;

namespace SimpleShop.Application.Authentication.Commands;

// komenda pobierająca nowy token dostępu i refresh token i aktualizuje bazę danych i zwraca te dane do użytkownika,
// tak żeby można było z aplikacji po stronie przegladarki zaktualizować wartości dla użytkownika 
public class RefreshTokenCommandHandler(UserManager<ApplicationUser> userManager, IAuthenticationService authenticationService) : IRequestHandler<RefreshTokenCommand, LoginUserDto>
{
	public async Task<LoginUserDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
	{
		var principal = authenticationService.GetPrincipalFromExpiredToken(request.Token);
		var name = principal.Identity.Name;
		var user = await userManager.FindByEmailAsync(name);

		if (user == null || user.Email != name)
		{
			return new LoginUserDto { ErrorMessage = "Nieprawidłowe żądanie." };
		}

		if (user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
		{
			return new LoginUserDto { ErrorMessage = "Nieprawidłowe żądanie." };
		}

		var token = await authenticationService.GetToken(user);
		user.RefreshToken = authenticationService.GenerateRefreshToken();
		await userManager.UpdateAsync(user);

		return new LoginUserDto
		{
			Token = token,
			RefreshToken = user.RefreshToken,
			IsAuthSuccessful = true
		};
	}
}