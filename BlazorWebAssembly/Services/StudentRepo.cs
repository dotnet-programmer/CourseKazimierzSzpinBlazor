using BlazorWebAssembly.Models;

namespace BlazorWebAssembly.Services;

public class StudentRepo : IStudentRepo
{
	private readonly List<Student> _students = [];

	public List<Student> GetAllStudents()
		=> _students;

	public void AddStudent()
		=> _students.Add(new Student { Name = Guid.NewGuid().ToString() });
}