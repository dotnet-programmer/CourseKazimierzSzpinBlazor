using MediatR;
using Microsoft.AspNetCore.Http;

namespace TodoApp.Shared.Tasks.Commands;

public class UploadImageCommand : IRequest
{
	public IFormFile File { get; set; }
}