using Microsoft.AspNetCore.Components.Forms;
using TodoApp.Shared.Tasks.Commands;
using TodoApp.Shared.Tasks.Dtos;

namespace TodoApp.Client.HttpRepository.Interfaces;

// można wstrzykiwać implementację HttpCient bezpośrednio do komponentów,
// ale najlepszą praktyką jest stworzenie osobnej warstwy, nowego serwisu,
// w którym będą wszystkie wywołania endpointów z WebApi z wykorzystaniem HttpClient
// dzięki temu jest porządek w kodzie, a samo użycie będzie prostsze, można też operować na interfejsach
// a serwisy można używać w dowolnych komponentach
// tutaj są wszystkie metody, które będą pracować z WebApi
public interface ITaskHttpRepository
{
	Task Add(AddTaskCommand command);

	Task Edit(EditTaskCommand command);

	Task Delete(int id);

	Task<IList<TaskDto>> GetAll();

	Task<EditTaskCommand> GetEdit(int id);

	Task UploadImage(IBrowserFile file);
}