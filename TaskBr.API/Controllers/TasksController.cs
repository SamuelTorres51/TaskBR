using Microsoft.AspNetCore.Mvc;
using TaskBr.Communication.Requests;
using TaskBr.Communication.Responses;
using TaskBr.Application.Repositories;
using TaskBr.Application.UseCases.Register;
using TaskBr.Application.UseCases.Tasks.GetAll;

namespace TaskBr.API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class TasksController : ControllerBase {

    private readonly TasksRepository _repository;

    public TasksController(TasksRepository repository) {
        _repository = repository;
    }


    // POST Criar task
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegistedTaskJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsTaskJson), StatusCodes.Status400BadRequest)]
    public IActionResult RegisterTask([FromBody] RequestRegisterTaskJson request) {
        var UseCase = new RegisterTasksUseCase();
        var result = UseCase.Execute(request, _repository);

        if (result.Errors is not null) {
            return BadRequest(result.Errors);
        }

        return Created(string.Empty, result.Success);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseTasksJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult GetAllTasks() {
        var UseCase = new GetAllTasksUseCase();
        var response = UseCase.Execute(_repository);
        

        if (response.Tasks.Count == 0) {
            return NoContent();
        }
        

        return Ok(response);
    }
}
