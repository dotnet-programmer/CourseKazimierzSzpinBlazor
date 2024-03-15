﻿using System.ComponentModel.DataAnnotations;
using MediatR;

namespace TodoApp.Shared.Tasks.Commands;

public class EditTaskCommand : IRequest
{
	public int Id { get; set; }

	[Required(ErrorMessage = "Pole 'Tytuł' jest wymagane.")]
	public string Title { get; set; }

	public string Description { get; set; }
	public bool IsExecuted { get; set; }
	public DateTime? Term { get; set; }
}