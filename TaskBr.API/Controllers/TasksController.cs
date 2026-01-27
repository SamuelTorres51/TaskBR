using Microsoft.AspNetCore.Mvc;
using TaskBr.Communication.Requests;
using TaskBr.Communication.Responses;
using TaskBr.Application.Repositories;

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

        return Created();
    }
}
