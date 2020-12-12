using System;
using System.Collections.Generic;

namespace CarterDemo.Models
{
    public interface IToDoItemProvider
    {
        IEnumerable<ToDoItem> GetItems(Func<ToDoItem, bool> filter = null);

        ToDoItem GetItem(int id);

        bool AddItem(ToDoItem item);

        (bool success, ToDoItem item) UpdateItem(int id, ToDoItem item);

        bool DeleteItem(int id);
    }
}
