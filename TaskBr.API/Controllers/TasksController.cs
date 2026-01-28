using Microsoft.AspNetCore.Mvc;
using TaskBr.Communication.Requests;
using TaskBr.Communication.Responses;
using TaskBr.Application.Repositories;
using TaskBr.Application.UseCases.Register;
using TaskBr.Application.UseCases.Tasks.GetAll;
using TaskBr.Application.UseCases.Tasks.GetById;
using TaskBr.Application.UseCases.Tasks.Update;
using TaskBr.Application.UseCases.Tasks.Delete;

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

    // GET Obter todas as tasks
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

    // Get Obter task por id
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseTaskJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetByIdTasks([FromRoute] Guid id) {
        var UseCase = new GetByIdTasksUseCase();
        var response = UseCase.Execute(id, _repository);

        if (response == null)
            return NotFound();

        return Ok(response);
    }

    // Put alterar task por id
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseUpdatedTaskJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorsTaskJson), StatusCodes.Status400BadRequest)]
    public IActionResult UpdateTask([FromRoute] Guid id, [FromBody] RequestUpdateTaskJson request) {
        var UseCase = new UpdateTasksUseCase();
        var response = UseCase.Execute(id, request, _repository);

        if(response.Success == null && response.Errors == null) {
            return NotFound();
        }

        if (response.Errors is not null) {
            return BadRequest(response.Errors);
        }

        return Ok(response.Success);
    }


    //Delete uma task
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public IActionResult DeleteTasks([FromRoute] Guid id) {
        var UseCase = new DeleteTasksUseCase();
        var response = UseCase.Execute(id, _repository);
        if (!response) {
            return NotFound();
        }
        return NoContent();
    }
}
