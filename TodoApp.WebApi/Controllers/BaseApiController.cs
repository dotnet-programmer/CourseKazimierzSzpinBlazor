using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TodoApp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseApiController : ControllerBase
{
	private ISender _mediator;
	public ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
}