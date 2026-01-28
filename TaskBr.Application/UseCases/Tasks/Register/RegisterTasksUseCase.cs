using TaskBr.Application.Repositories;
using TaskBr.Communication.Enums;
using TaskBr.Communication.Requests;
using TaskBr.Communication.Responses;

namespace TaskBr.Application.UseCases.Register;

public class RegisterTasksUseCase {

    public (ResponseRegistedTaskJson? Success, ResponseErrorsTaskJson? Errors) Execute(RequestRegisterTaskJson request, TasksRepository repository) {
        var errors = new ResponseErrorsTaskJson();

        if (string.IsNullOrWhiteSpace(request.Name)) {
            errors.Errors.Add("O nome da tarefa é obrigatório");
        } else if (request.Name.Length > 100) {
            errors.Errors.Add("O nome da tarefa deve ter no máximo 100 caracteres.");
        }

        if (string.IsNullOrWhiteSpace(request.Description)) {
            errors.Errors.Add("O nome da descrição é obrigatório");
        }

        if(!Enum.IsDefined(typeof(PriorityTask), request.Priority)) {
            errors.Errors.Add("A prioridade da tarefa é inválida");
        }

        if(!Enum.IsDefined(typeof(StatusTask), request.Status)) {
            errors.Errors.Add("O status da tarefa é inválido");
        }

        if (request.DueDate.Date < DateTime.Today) {
            errors.Errors.Add("A data limite não pode ser no passado");
        }

        if (errors.Errors.Count > 0) {
            return (null, errors);
        }

        var task = new Models.Entity.Tasks();
        task.Id = Guid.NewGuid();
        task.Name = request.Name;
        task.Description = request.Description;
        task.Priority = request.Priority;
        task.DueDate = request.DueDate;
        task.Status = request.Status;

        repository.Add(task);

        var response = new ResponseRegistedTaskJson() {
            Id = task.Id,
            Name = task.Name
        };

        return (response, null);
    }
}
