using System.ComponentModel.DataAnnotations;
using MediatR;

namespace SimpleShop.Shared.Orders.Commands;

public class ConfirmOrderCommand : IRequest
{
	[Required]
	public string SessionId { get; set; }
}