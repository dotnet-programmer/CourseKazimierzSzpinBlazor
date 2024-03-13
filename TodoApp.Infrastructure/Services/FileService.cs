using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using TodoApp.Application.Common.Interfaces;

namespace TodoApp.Infrastructure.Services;

public class FileService : IFileService
{
	//private readonly IHostingEnvironment _webHostEnvironment;

	//public FileService(IHostingEnvironment webHostEnvironment) 
	//	=> _webHostEnvironment = webHostEnvironment;

	private readonly IWebHostEnvironment _webHostEnvironment;

	public FileService(IWebHostEnvironment webHostEnvironment)
		=> _webHostEnvironment = webHostEnvironment;

	public async Task Upload(IFormFile file)
	{
		var folderRoot = Path.Combine(_webHostEnvironment.WebRootPath, "Content", "Files");

		if (!Directory.Exists(folderRoot))
		{
			Directory.CreateDirectory(folderRoot);
		}

		if (file.Length > 0)
		{
			var filePath = Path.Combine(folderRoot, file.FileName);
			using var stream = new FileStream(filePath, FileMode.Create);
			await file.CopyToAsync(stream);
		}
	}
}