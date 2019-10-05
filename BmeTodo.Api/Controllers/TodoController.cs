using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BmeTodo.Api.Models;
using BmeTodo.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BmeTodo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoService _todoService;

        public TodoController(TodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> Get()
        {
            return _todoService.GetTodos().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<TodoItem> Get(int id)
        {
            return _todoService.GetTodo(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TodoItem todo)
        {
            var id = _todoService.AddTodo(todo);
            return CreatedAtAction(nameof(Get), new { id }, todo);
        }


        [HttpPut("{id}")]
        public ActionResult<TodoItem> Put(int id, [FromBody] TodoItem todo)
        {
            return _todoService.UpdateTodo(id, todo);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _todoService.DeleteTodo(id);
            return NoContent();
        }
    }
}
