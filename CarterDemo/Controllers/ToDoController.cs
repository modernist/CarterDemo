using CarterDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CarterDemo.Controllers
{
    public class ToDoController : Controller
    {
        private IToDoItemProvider _provider;

        public ToDoController(IToDoItemProvider provider)
        {
            _provider = provider;
        }

        public ActionResult<IEnumerable<ToDoItem>> Index()
        {
            var items = _provider.GetItems();
            return Ok(items);
        }
    }
}
