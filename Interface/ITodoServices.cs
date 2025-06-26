using TodoApi.Contracts;
using TodoApi.Models;

namespace TodoApi.Interface
{
    public interface ITodoServices
    {
        Task<IEnumerable<Todo>> GetAllAsync();
        Task<Todo> GetByIdAsync(Guid id);
        Task CreateTodoAsync(CreateTodoRequest request);
        Task UpdateTodoAsync(Guid id, UpdateTodoRequest request);
        //  Task DeleteTodoAsync(Guid id);
        Task<bool> DeleteTodoAsync(Guid id);
        Task<bool> MarkTodoAsCompleteAsync(Guid id);

    }
}