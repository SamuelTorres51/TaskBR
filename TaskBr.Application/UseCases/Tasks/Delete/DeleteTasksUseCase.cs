using TaskBr.Application.Repositories;

namespace TaskBr.Application.UseCases.Tasks.Delete;

public class DeleteTasksUseCase {

    public bool Execute(Guid id, TasksRepository repository) {
        var task = repository.GetById(id);
        if(task == null) {
            return false;
        }

        repository.Remove(task);
        return true;
    }
}
