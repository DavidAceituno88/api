using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            return await _context.TodoItems
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }
        
        //Seed
        [HttpGet("seed")]
        
        public string Seed()
       {

            if (!_context.TodoItems.Any())
            {
                List<TodoItem>? todoItems = new List<TodoItem> 
                {
                new TodoItem { Id = 2, Name = "snort cocaine", IsComplete = true },
                new TodoItem { Id = 3, Name = "code", IsComplete = true },
                new TodoItem { Id = 4, Name = "snort another line", IsComplete = true },
                new TodoItem { Id = 5, Name = "dog walk", IsComplete = true },
                new TodoItem { Id = 6, Name = "strip club", IsComplete = true },
                new TodoItem { Id = 7, Name = "drag race", IsComplete = true },
                new TodoItem { Id = 8, Name = "snort another line", IsComplete = true },
                new TodoItem { Id = 9, Name = "get paid or busting a cap on them niggas", IsComplete = false },
                new TodoItem { Id = 10, Name = "snort another line", IsComplete = true },
                new TodoItem { Id = 11, Name = "snort another line", IsComplete = true }
                };
    
                _context.TodoItems.AddRange(todoItems);
                _context.SaveChanges();
            }
        
            string itemsAdded = _context.TodoItems.Count() + " items have been seeded";
            return itemsAdded;

        }
        
        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(todoItem);
        }
        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        
        public async Task<ActionResult> UpdateTodoItem (long id, TodoItem todoItems)
        {
            
            
              
            
            if (id != todoItems.Id)
            {
                return BadRequest();
            }

            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Name = todoItems.Name;
            todoItem.IsComplete = todoItems.IsComplete;
            todoItem.UserId = todoItems.UserId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
            
        }
        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //Here i modified the post method to accept a list of todoItems from the user.
        public async Task<ActionResult<TodoItem>> CreateTodoItem(List<TodoItem> items)
        {

            //This is a list i created so that later i can return the list added.
            _context.TodoItems.AddRange(items);
            await _context.SaveChangesAsync();

            List<String> uris = new List<string>();
           foreach(var temp in items)
                {
                    uris.Add("https://localhost:7296/api/todoitems/"+temp.Id);
                }
        
            return Ok();
          /*  var todoItem = new TodoItem
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                ItemToDTO(todoItem));*/
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
    }
    
}
