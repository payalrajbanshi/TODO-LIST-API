// using AutoMapper;
// using Microsoft.EntityFrameworkCore;
// using TodoApi.AppDataContext;
// using TodoApi.Contracts;
// using TodoApi.Interface;
// using TodoApi.Models;

// namespace TodoApi.Services
// {
//     public class TodoServices : ITodoServices
//     {
//         private readonly TodoDbContext _context;
//         private readonly ILogger<TodoServices> _logger;
//         private readonly IMapper _mapper;

//         public TodoServices(TodoDbContext context, ILogger<TodoServices> logger, IMapper mapper)
//         {
//             _context = context;
//             _logger = logger;
//             _mapper = mapper;
//         }

//         public async Task CreateTodoAsync(CreateTodoRequest request)
//         {
//             try
//             {
//                 var todo = _mapper.Map<Todo>(request);
//                 todo.CreatedAt = DateTime.Now;
//                 _context.Todos.Add(todo);
//                 await _context.SaveChangesAsync();
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "An error occurred while creating the todo item.");
//                 throw new Exception("An error occurred while creating the todo item.");
//             }
//         }

//         public async Task<IEnumerable<Todo>> GetAllAsync()
//         {
//             var todo = await _context.Todos.ToListAsync();
//             if (todo == null)
//             {
//                 throw new Exception(" No Todo items found");
//             }
//             return todo;

//         }
//         // public Task DeleteTodoAsync(Guid id)
//         // {
//         //     throw new NotImplementedException();
//         // }
//         public async Task<bool> DeleteTodoAsync(Guid id)
//         {
//             try
//             {
//                 var todo = await _context.Todos.FindAsync(id);
//                 if (todo == null)
//                 {
//                     return false;
//                 }

//                 _context.Todos.Remove(todo);
//                 await _context.SaveChangesAsync();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "An error occurred while deleting the todo item.");
//                 throw new Exception("An error occurred while deleting the todo item.");
//             }
//         }



//         // public Task<Todo> GetByIdAsync(Guid id)
//         // {
//         //     throw new NotImplementedException();
//         // }

//         // public Task UpdateTodoAsync(Guid id, UpdateTodoRequest request)
//         // {
//         //     throw new NotImplementedException();
//         // }
//         public async Task<bool> MarkTodoAsCompleteAsync(Guid id)
//         {
//             try
//             {
//                 var todo = await _context.Todos.FindAsync(id);
//                 if (todo == null)
//                 {
//                     return false;
//                 }

//                 todo.IsComplete = true;
//                 await _context.SaveChangesAsync();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "An error occurred while marking the todo item as complete.");
//                 throw new Exception("An error occurred while marking the todo item as complete.");
//             }
//         }
//         public async Task<Todo> GetByIdAsync(Guid id)
//         {
//             var todo = await _context.Todos.FindAsync(id);
//             if (todo == null)
//             {
//                 throw new Exception("Todo not found");
//             }
//             return todo;
//         }
//         public async Task UpdateTodoAsync(Guid id, UpdateTodoRequest request)
//         {
//             var todo = await _context.Todos.FindAsync(id);
//             if (todo == null)
//             {
//                 throw new Exception("Todo not found");
//             }

//             todo.Title = request.Title;
//             todo.Description = request.Description;
//             todo.IsComplete = request.IsComplete;

//             await _context.SaveChangesAsync();
//         }


//     }
// }
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoApi.AppDataContext;
using TodoApi.Contracts;
using TodoApi.Interface;
using TodoApi.Models;

namespace TodoApi.Services
{
    public class TodoServices : ITodoServices
    {
        private readonly TodoDbContext _context;
        private readonly ILogger<TodoServices> _logger;
        private readonly IMapper _mapper;

        public TodoServices(TodoDbContext context, ILogger<TodoServices> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task CreateTodoAsync(CreateTodoRequest request)
        {
            try
            {
                var todo = _mapper.Map<Todo>(request);
                todo.CreatedAt = DateTime.Now;
                todo.UpdatedAt = DateTime.Now;
                _context.Todos.Add(todo);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the todo item.");
                throw new Exception("An error occurred while creating the todo item.");
            }
        }

        public async Task<IEnumerable<Todo>> GetAllAsync()
        {
            var todos = await _context.Todos.ToListAsync();
            if (todos == null || !todos.Any())
            {
                throw new Exception("No Todo items found");
            }
            return todos;
        }

        public async Task<Todo> GetByIdAsync(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                throw new Exception("Todo not found");
            }
            return todo;
        }

        public async Task UpdateTodoAsync(Guid id, UpdateTodoRequest request)
        {
            try
            {
                var todo = await _context.Todos.FindAsync(id);
                if (todo == null)
                {
                    throw new Exception("Todo not found");
                }

                if (!string.IsNullOrWhiteSpace(request.Title))
                    todo.Title = request.Title;

                if (!string.IsNullOrWhiteSpace(request.Description))
                    todo.Description = request.Description;

                if (request.IsComplete.HasValue)
                    todo.IsComplete = request.IsComplete.Value;

                todo.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the todo item.");
                throw new Exception("An error occurred while updating the todo item.");
            }
        }

        public async Task<bool> DeleteTodoAsync(Guid id)
        {
            try
            {
                var todo = await _context.Todos.FindAsync(id);
                if (todo == null)
                {
                    return false;
                }

                _context.Todos.Remove(todo);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the todo item.");
                throw new Exception("An error occurred while deleting the todo item.");
            }
        }

        public async Task<bool> MarkTodoAsCompleteAsync(Guid id)
        {
            try
            {
                var todo = await _context.Todos.FindAsync(id);
                if (todo == null)
                {
                    return false;
                }

                todo.IsComplete = true;
                todo.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while marking the todo item as complete.");
                throw new Exception("An error occurred while marking the todo item as complete.");
            }
        }
    }
}
