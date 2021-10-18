using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace TodoApp
{
    public class TodoAppService : ApplicationService, ITodoAppService
    {
        private readonly IRepository<TodoItem, Guid> _todoItemRepository;

        public TodoAppService(IRepository<TodoItem, Guid> todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        // TODO: Implement the methods here...
        public async Task<List<TodoItemDto>> GetListAsync()
        {
            var items = await _todoItemRepository.GetListAsync();
            return items
                .Select(item => new TodoItemDto
                {
                    Id = item.Id,
                    Text = item.Text
                }).ToList();
        }

        public async Task<TodoItemDto> CreateAsync(string text)
        {
            var todoItem = await _todoItemRepository.InsertAsync(
                new TodoItem { Text = text }
            );

            return new TodoItemDto
            {
                Id = todoItem.Id,
                Text = todoItem.Text
            };
        }

        public async Task DeleteAsync(Guid id)
        {
            await _todoItemRepository.DeleteAsync(id);
        }

        public async Task<TodoItemDto> UpdateAsync(Guid id, string text)
        {
            var uid =  _todoItemRepository.FirstOrDefault(x => x.Id.Equals(id));

            if (uid == null)
                Console.WriteLine("Please select one record to update");

            return new TodoItemDto
            {
                Text = uid.Text
            };

        }
    }
}