using FilmPool.DbModels;
using FilmPool.ResponseModels;
using FilmPool.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmPool.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : Controller
    {
      IUserService _userService;

      public UserController(IUserService userService)
      {
      _userService = userService;
      }

      [HttpGet]
      [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Get()
      {
       var users = await _userService.Get();
       return Ok(users);
      }

      /*[HttpGet("{id}", Name = "GetTodoItem")]
      public IActionResult Get(int Id)
      {
        TodoItem todoItem = TodoRepository.Get(Id); 

        if (todoItem == null)
        {
          return NotFound();
        }

        return new ObjectResult(todoItem);
      }

      [HttpPost]
      public IActionResult Create([FromBody] TodoItem todoItem)
      {
        if (todoItem == null)
        {
          return BadRequest();
        }
        TodoRepository.Create(todoItem);
        return CreatedAtRoute("GetTodoItem", new { id = todoItem.Id }, todoItem);
      }

      [HttpPut("{id}")]
      public IActionResult Update(int Id, [FromBody] TodoItem updatedTodoItem)
      {
        if (updatedTodoItem == null || updatedTodoItem.Id != Id)
        {
          return BadRequest();
        }

        var todoItem = TodoRepository.Get(Id);
        if (todoItem == null)
        {
          return NotFound();
        }

        TodoRepository.Update(updatedTodoItem);
        return RedirectToRoute("GetAllItems");
      }

      [HttpDelete("{id}")]
      public IActionResult Delete(int Id)
      {
        var deletedTodoItem = TodoRepository.Delete(Id);

        if (deletedTodoItem == null)
        {
          return BadRequest();
        }

        return new ObjectResult(deletedTodoItem);
      }
    }*/
  }
}
