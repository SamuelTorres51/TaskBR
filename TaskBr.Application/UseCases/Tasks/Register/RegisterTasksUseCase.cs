using TaskBr.Communication.Requests;
using TaskBr.Communication.Responses;

namespace TaskBr.Application.UseCases.Register;

public class RegisterTasksUseCase {
    public ResponseRegistedTaskJson Execute(RequestRegisterTaskJson request) {
        var errors = new ResponseErrorsTaskJson();
        if (string.IsNullOrEmpty(request.Name)) {
            errors.Errors.Add("O nome da tarefa é obrigatório");
        }

        if(request.DueDate < DateTime.Today) {
            errors.Errors.Add("A data não pode ser no passado");
        }


    }
}
