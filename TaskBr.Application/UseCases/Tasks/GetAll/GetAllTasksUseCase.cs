using TaskBr.Communication.Responses;
using TaskBr.Application.Repositories;

namespace TaskBr.Application.UseCases.Tasks.GetAll;

public class GetAllTasksUseCase {
    public ResponseTasksJson Execute(TasksRepository repository) {
        var tasks = repository.GetAll();
        var response = new ResponseTasksJson {
            Tasks = tasks.Select(task => new ResponseShortTasksJson {
                Id = task.Id,
                Name = task.Name,
                Priority = task.Priority,
                Status = task.Status
            }).ToList()
        };
        return response;
    }
}
