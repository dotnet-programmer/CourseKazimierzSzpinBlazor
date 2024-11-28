using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleShop.Shared.Payments.Commands;

namespace SimpleShop.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController : BaseApiController
{
	[HttpPost]
	[Authorize]
	public async Task<IActionResult> Add(AddPaymentCommand command)
		=> Ok(await Mediator.Send(command));
}