using BlazorWebAssembly.Models;

namespace BlazorWebAssembly.Services;

public class StudentRepo : IStudentRepo
{
	private readonly List<Student> _students = [];

	public void Add() 
		=> _students.Add(new Student { Name = Guid.NewGuid().ToString() });

	public List<Student> Get() 
		=> _students;
}