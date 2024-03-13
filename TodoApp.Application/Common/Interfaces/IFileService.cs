using Microsoft.AspNetCore.Http;

namespace TodoApp.Application.Common.Interfaces;

public interface IFileService
{
	Task Upload(IFormFile file);
}