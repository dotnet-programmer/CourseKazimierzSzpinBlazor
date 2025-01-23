using MediatR;
using TodoApp.Application.Common.Interfaces;
using TodoApp.Shared.Tasks.Commands;

namespace TodoApp.Application.Tasks.Commands;

public class UploadImageCommandHandler(IFileService fileService) : IRequestHandler<UploadImageCommand>
{
	public async Task Handle(UploadImageCommand request, CancellationToken cancellationToken)
		=> await fileService.Upload(request.File);
}