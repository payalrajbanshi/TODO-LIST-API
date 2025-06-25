// private readonly TodoDbContext _context;
// private readonly ILogger<TodoServices> _logger;
// private readonly IMapper _mapper;

// using TodoApi.Models;
// using TodoApi.Interface;

// namespace TodoAPI.Services
// {
//     public class TodoServices : ITodoServices
//     {
//         public Task CreateTodoAsync(CreateTodoRequest request)
//         {
//             throw new NotImplementedException();
//         }
//         public TodoServices(TodoDbContext context, ILogger<TodoServices> logger, IMapper mapper)
//         {
//             _context = context;
//             _logger = logger;
//             _mapper = mapper;
//         }

//         public Task DeleteTodoAsync(Guid id)
//         {
//             throw new NotImplementedException();
//         }

//         public Task<IEnumerable<Todo>> GetAllAsync()
//         {
//             throw new NotImplementedException();
//         }

//         public Task<Todo> GetByIdAsync(Guid id)
//         {
//             throw new NotImplementedException();
//         }

//         public Task UpdateTodoAsync(Guid id, UpdateTodoRequest request)
//         {
//             throw new NotImplementedException();
//         }
//         public async Task CreateTodoAsync(CreateTodoRequest request)
// {
//     try
//     {
//         var todo = _mapper.Map<Todo>(request);
//         todo.CreatedAt = DateTime.UtcNow;
//         _context.Todos.Add(todo);
//         await _context.SaveChangesAsync();
//     }
//     catch (Exception ex)
//     {
//         _logger.LogError(ex, "An error occurred while creating the Todo item.");
//         throw new Exception("An error occurred while creating the Todo item.");
//     }
// }
//     }
// }
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoApi.AppDataContext;
using TodoApi.Contracts;
using TodoApi.Interface;
using TodoApi.Models;

namespace TodoAPI.Services
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




        //  Create Todo for it be save in the datbase 

        public async Task CreateTodoAsync(CreateTodoRequest request)
        {
            try
            {
                var todo = _mapper.Map<Todo>(request);
                todo.CreatedAt = DateTime.Now;
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
            var todo = await _context.Todos.ToListAsync();
            if (todo == null)
            {
                throw new Exception(" No Todo items found");
            }
            return todo;

        }
        public Task DeleteTodoAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        // Get all TODO Items from the database 


        public Task<Todo> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTodoAsync(Guid id, UpdateTodoRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
