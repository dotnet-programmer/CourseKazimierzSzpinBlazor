using MediatR;
using TodoApp.Application.Common.Interfaces;
using TodoApp.Shared.Tasks.Commands;

namespace TodoApp.Application.Tasks.Commands;

public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand>
{
	private readonly IFileService _fileService;

	public UploadImageCommandHandler(IFileService fileService)
		=> _fileService = fileService;

	public async Task Handle(UploadImageCommand request, CancellationToken cancellationToken)
		=> await _fileService.Upload(request.File);
}