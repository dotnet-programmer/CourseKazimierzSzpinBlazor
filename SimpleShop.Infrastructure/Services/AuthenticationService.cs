using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Domain.Entities;

namespace SimpleShop.Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
	private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly IConfiguration _configuration;

	public AuthenticationService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
	{
		_jwtSecurityTokenHandler = new();
		_userManager = userManager;
		_configuration = configuration;
	}

	// logika pobierająca token dla użytkownika
	public async Task<string> GetToken(ApplicationUser user)
	{
		var signingCredentials = GetSigningCredentials();
		var claims = await GetClaims(user);
		var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
		return _jwtSecurityTokenHandler.WriteToken(tokenOptions);
	}

	private SigningCredentials GetSigningCredentials()
	{
		var key = Encoding.UTF8.GetBytes(_configuration["JWTSettings:SecurityKey"]);
		SymmetricSecurityKey secret = new(key);
		return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
	}

	private async Task<IEnumerable<Claim>> GetClaims(ApplicationUser user)
	{
		List<Claim> claims =
			[
				new(ClaimTypes.Name, user.Email),
				new(ClaimTypes.NameIdentifier, user.Id)
			];

		var roles = await _userManager.GetRolesAsync(user);
		foreach (var role in roles)
		{
			claims.Add(new Claim(ClaimTypes.Role, role));
		}

		return claims;
	}

	private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
	{
		JwtSecurityToken tokenOptions = new (
			issuer: _configuration["JWTSettings:ValidIssuer"],
			audience: _configuration["JWTSettings:ValidAudience"],
			claims: claims,
			expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JWTSettings:ExpiryInMinutes"])),
			signingCredentials: signingCredentials);

		return tokenOptions;
	}

	public string GenerateRefreshToken()
	{
		var randomNumber = new byte[32];
		using (var rng = RandomNumberGenerator.Create())
		{
			rng.GetBytes(randomNumber);
			return Convert.ToBase64String(randomNumber);
		}
	}

	// pobieranie danych z tokena i walidacja danych
	public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
	{
		TokenValidationParameters tokenValidationParameters = new()
		{
			ValidateAudience = true,
			ValidateIssuer = true,
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:SecurityKey"])),
			ValidateLifetime = false,
			ValidIssuer = _configuration["JWTSettings:ValidIssuer"],
			ValidAudience = _configuration["JWTSettings:ValidAudience"],
		};

		JwtSecurityTokenHandler tokenHandler = new();
		SecurityToken securityToken;
		var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

		if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
		{
			throw new SecurityTokenException("Invalid token");
		}

		return principal;
	}
}