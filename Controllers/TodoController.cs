// using Microsoft.AspNetCore.Mvc;
// using TodoApi.Interface;

// namespace TodoApi.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class TodoController : ControllerBase
//     {
//         private readonly ITodoServices _todoServices;

//         public TodoController(ITodoServices todoServices)
//         {
//             _todoServices = todoServices;
//         }

//     }
// }
using Microsoft.AspNetCore.Mvc;
using TodoApi.Contracts;
using TodoApi.Interface;

namespace TodoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoServices _todoServices;

        public TodoController(ITodoServices todoServices)
        {
            _todoServices = todoServices;
        }



// Creating new Todo Item
[HttpPost]
  public async Task<IActionResult> CreateTodoAsync(CreateTodoRequest request)
  {
      if (!ModelState.IsValid)
      {
          return BadRequest(ModelState);
      }


      try
      {

          await _todoServices.CreateTodoAsync(request);
          return Ok(new { message = "Blog post successfully created" });

      }
      catch (Exception ex)
      {
          return StatusCode(500, new { message = "An error occurred while creating the  crating Todo Item", error = ex.Message });

      }
  }

    // Get all Todo Items

      [HttpGet]
  public async Task<IActionResult> GetAllAsync()
  {
      try
      {
          var todo = await _todoServices.GetAllAsync();
          if (todo == null || !todo.Any())
          {
              return Ok(new { message = "No Todo Items  found" });
          }
          return Ok(new { message = "Successfully retrieved all blog posts", data = todo });

      }
      catch (Exception ex)
      {
          return StatusCode(500, new { message = "An error occurred while retrieving all Tood it posts", error = ex.Message });


      }
  }

    }
}