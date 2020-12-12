using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CarterDemo.Models
{
    public class ToDoItemProvider : IToDoItemProvider
    {
        private readonly List<ToDoItem> _items = new List<ToDoItem>();
        private int _nextIdentity = 0;

        public ToDoItemProvider()
        {
            AddItem(new ToDoItem() { Category = "Shopping", Description = "Get milk" });
            AddItem(new ToDoItem() { Category = "Chores", Description = "Do laundry" });
            AddItem(new ToDoItem() { Category = "Work", Description = "Create demo" });
        }

        public IEnumerable<ToDoItem> GetItems(Func<ToDoItem, bool> filter = null)
        {
            return filter != null ? _items.Where(filter) : _items;
        }

        public ToDoItem GetItem(int id)
        {
            return _items.FirstOrDefault(item => item.Id == id);
        }

        public bool AddItem(ToDoItem item)
        {
            if (_items.Contains(item))
            {
                return false;
            }

            item.Id = Interlocked.Increment(ref _nextIdentity);
            _items.Add(item);
            return true;
        }

        public (bool success, ToDoItem item) UpdateItem(int id, ToDoItem item)
        {
            var existingItem = _items.FirstOrDefault(i => i.Id == id);
            if (existingItem == null)
            {
                return (false, null);
            }

            existingItem.Category = item.Category;
            existingItem.Description = item.Description;
            return (true, existingItem);
        }

        public bool DeleteItem(int id)
        {
            return _items.RemoveAll(item => item.Id == id) > 0;
        }
    }
}
