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
        // PUT: api/todo/{id}/complete
        [HttpPut("{id}/complete")]
        public async Task<IActionResult> MarkComplete(Guid id)
        {
            try
            {
                var success = await _todoServices.MarkTodoAsCompleteAsync(id);
                if (!success)
                {
                    return NotFound(new { message = "Todo item not found" });
                }

                return Ok(new { message = "Todo marked as complete" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error marking todo as complete", error = ex.Message });
            }
        }

        // DELETE: api/todo/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(Guid id)
        {
            try
            {
                var success = await _todoServices.DeleteTodoAsync(id);
                if (!success)
                {
                    return NotFound(new { message = "Todo item not found" });
                }

                return Ok(new { message = "Todo item deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting todo", error = ex.Message });
            }
        }



    }
}