using System.Security.Claims;
using SimpleShop.Domain.Entities;

namespace SimpleShop.Application.Common.Interfaces;

// metody związane z uwierzytelnieniem
public interface IAuthenticationService
{
	Task<string> GetToken(ApplicationUser user);

	string GenerateRefreshToken();

	ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}