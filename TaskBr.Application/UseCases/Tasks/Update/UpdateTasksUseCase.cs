using TaskBr.Application.Repositories;
using TaskBr.Communication.Enums;
using TaskBr.Communication.Requests;
using TaskBr.Communication.Responses;

namespace TaskBr.Application.UseCases.Tasks.Update;

public class UpdateTasksUseCase {
    public (ResponseErrorsTaskJson? Errors, ResponseUpdatedTaskJson? Success) Execute(Guid id, RequestUpdateTaskJson request, TasksRepository repository) {
        var task = repository.GetById(id);
        if(task == null) {
            return (null, null);
        }

        var errors = new ResponseErrorsTaskJson();

        if (string.IsNullOrWhiteSpace(request.Name)) {
            errors.Errors.Add("O nome da tarefa é obrigatório");
        } else if (request.Name.Length > 100) {
            errors.Errors.Add("O nome da tarefa deve ter no máximo 100 caracteres.");
        }

        if (string.IsNullOrWhiteSpace(request.Description)) {
            errors.Errors.Add("O nome da descrição é obrigatório");
        }

        if (!Enum.IsDefined(typeof(PriorityTask), request.Priority)) {
            errors.Errors.Add("A prioridade da tarefa é inválida");
        }

        if (!Enum.IsDefined(typeof(StatusTask), request.Status)) {
            errors.Errors.Add("O status da tarefa é inválido");
        }

        if (request.DueDate.Date < DateTime.Today) {
            errors.Errors.Add("A data limite não pode ser no passado");
        }

        if (errors.Errors.Count > 0) {
            return (errors, null);
        }

        var novo = new Models.Entity.Tasks();
        novo.Id = id;
        novo.Name = request.Name;
        novo.Description = request.Description;
        novo.Status = request.Status;
        novo.DueDate = request.DueDate;
        novo.Priority = request.Priority;

        repository.Update(novo);

        var response = new ResponseUpdatedTaskJson() {
            Id = id,
            Name = request.Name
        };

        return (null, response);
    }
}
