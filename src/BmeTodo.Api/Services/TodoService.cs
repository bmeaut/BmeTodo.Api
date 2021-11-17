using System;
using System.Collections.Generic;
using System.Linq;

using BmeTodo.Api.Exceptions;
using BmeTodo.Api.Models;

namespace BmeTodo.Api.Services
{
    public class TodoService
    {
        private List<TodoItem> _todos = new List<TodoItem>
        {
            new TodoItem
            {
                Id = 1,
                Deadline = DateTimeOffset.Now.AddDays(2),
                Description = "Tejet vennik, 2,8%-os zsírtartalmút!",
                IsDone = true,
                Priority = Priority.Normal,
                Title = "Tejet venni",
            },
            new TodoItem
            {
                Id = 2,
                Deadline = new DateTime(2019, 12, 13, 12, 0, 0),
                Description = "Megírni a szakdolgozatot, ne feledd a határidő szorgalmi időszak 14. hetének péntekének dele.",
                IsDone = true,
                Priority = Priority.Normal,
                Title = "Megírni a szakdolgozatot",
            }
        };

        public IEnumerable<TodoItem> GetTodos()
        {
            return _todos;
        }

        public TodoItem GetTodo(int id)
        {
            return _todos.FirstOrDefault(t => t.Id == id)
                ?? throw new EntityNotFoundException("A megadott azonosítóval nem taláható TodoItem");
        }

        public TodoItem AddTodo(TodoItem todoItem)
        {
            // nem szálbiztos, de ez most mind1
            var nextId = _todos.OrderByDescending(t => t.Id).FirstOrDefault()?.Id + 1 ?? 1;
            _todos.Add(todoItem);
            todoItem.Id = nextId;
            return todoItem;
        }

        public TodoItem UpdateTodo(int id, TodoItem todoItem)
        {
            var oldTodo = GetTodo(id);
            oldTodo.IsDone = todoItem.IsDone;
            oldTodo.Priority = todoItem.Priority;
            oldTodo.Description = todoItem.Description;
            oldTodo.Title = todoItem.Title;
            oldTodo.Deadline = todoItem.Deadline;
            return oldTodo;
        }

        public void DeleteTodo(int id)
        {
            // nem szálbiztos, de ez most mind1
            var todo = GetTodo(id);
            _todos.Remove(todo);
        }
    }
}
