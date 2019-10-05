using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BmeTodo.Api.Models;
using BmeTodo.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BmeTodo.Api.Controllers
{
    /// <summary>
    /// Teendőkkel kapcsolatos funkciók
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoService _todoService;

        public TodoController(TodoService todoService)
        {
            _todoService = todoService;
        }

        /// <summary>
        /// Az összes teendő lekérdezése
        /// </summary>
        /// <returns>Visszatér a teendők listájával</returns>
        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> Get()
        {
            return _todoService.GetTodos().ToList();
        }

        /// <summary>
        /// Egy teendő lekérdezése a megadott azonosítóval
        /// </summary>
        /// <param name="id">Lekérdezendő teendő azonosítója</param>
        /// <returns>Visszatér a megadott azonosítóval rendelkező teendővel</returns>
        [HttpGet("{id}")]
        public ActionResult<TodoItem> Get(int id)
        {
            return _todoService.GetTodo(id);
        }

        /// <summary>
        /// Új teendő felvétele a listába
        /// </summary>
        /// <param name="todo">Az felvenni kívánt új teendő adatai</param>
        /// <returns>Visszatér a létrehozott új teendővel</returns>
        [HttpPost]
        public IActionResult Post([FromBody] TodoItem todo)
        {
            var id = _todoService.AddTodo(todo);
            return CreatedAtAction(nameof(Get), new { id }, todo);
        }

        /// <summary>
        /// Megadott azonosítóval rendelkező teendő módosítása
        /// </summary>
        /// <param name="id">Módosítandó teendő azonosítója</param>
        /// <param name="todo">Módosítandó teendő adatai</param>
        /// <returns>Visszatér a módosítptt teendővel</returns>
        [HttpPut("{id}")]
        public ActionResult<TodoItem> Put(int id, [FromBody] TodoItem todo)
        {
            return _todoService.UpdateTodo(id, todo);
        }

        /// <summary>
        /// Megadott azonosítóval rendelkező teendő törlése
        /// </summary>
        /// <param name="id">Törlendő teendő azonosítója</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _todoService.DeleteTodo(id);
            return NoContent();
        }
    }
}
