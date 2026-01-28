using TaskBr.Application.Repositories;
using TaskBr.Communication.Responses;

namespace TaskBr.Application.UseCases.Tasks.GetById;

public class GetByIdTasksUseCase {
    public ResponseTaskJson? Execute(Guid id, TasksRepository repository) {
        var task = repository.GetById(id);
        if(task == null) {
            return null;
        }
        var response = new ResponseTaskJson {
            Id = task.Id,
            Name = task.Name,
            Description = task.Description,
            Priority = task.Priority,
            DueDate = task.DueDate,
            Status = task.Status,
            
        };
        return response;
    }
}
