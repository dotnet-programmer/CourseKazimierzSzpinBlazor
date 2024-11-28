using Microsoft.AspNetCore.Mvc;
using TodoApp.Shared.Tasks.Commands;
using TodoApp.Shared.Tasks.Queries;

namespace TodoApp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController : BaseApiController
{
	[HttpGet("edit/{id}")]
	public async Task<IActionResult> GetEditTask(int id)
	{
		var task = await Mediator.Send(new GetEditTaskQuery { Id = id });
		return task != null ? Ok(task) : NotFound();
	}

	[HttpPost]
	public async Task<IActionResult> Add(AddTaskCommand command)
	{
		await Mediator.Send(command);
		return Ok();
	}

	[HttpGet]
	public async Task<IActionResult> GetTasks()
		=> Ok(await Mediator.Send(new GetTasksQuery()));

	[HttpPut]
	public async Task<IActionResult> Edit(EditTaskCommand command)
	{
		await Mediator.Send(command);
		return NoContent();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		await Mediator.Send(new DeleteTaskCommand { Id = id });
		return NoContent();
	}

	[HttpPost("upload-image")]
	public async Task<IActionResult> UploadImage()
	{
		var file = Request.Form.Files.FirstOrDefault();

		if (file == null)
		{
			return BadRequest();
		}

		await Mediator.Send(new UploadImageCommand { File = file });
		return Ok();
	}
}