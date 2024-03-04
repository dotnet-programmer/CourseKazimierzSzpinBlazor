using BlazorWebAssembly.Models;

namespace BlazorWebAssembly.Services;

public interface IStudentRepo
{
	List<Student> Get();

	void Add();
}