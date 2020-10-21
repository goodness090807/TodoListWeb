using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using TodoListWeb.Models;
using TodoListWeb.Shared;
using TodoListWeb.ViewModels.TodoList;

namespace TodoListWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApiRequest apiRequest = new ApiRequest();
        private string _token = string.Empty;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                _token = User.FindFirst("Token").Value;
            }
            List<TodoListModel> todoList = apiRequest.GetTodoLists(User.Identity.Name, _token);

            return View(todoList);
        }

        public IActionResult TodoDetail(string Id)
        {
            if (User.Identity.IsAuthenticated)
            {
                _token = User.FindFirst("Token").Value;
            }
            TodoListModel todoList = apiRequest.GetTodoListById(Id, _token);

            return View(todoList);
        }

        public IActionResult TodoCreate()
        {
            TodoListsViewModel todoListsViewModel = new TodoListsViewModel();


            todoListsViewModel.listTypes = new List<SelectListItem>
            {
                new SelectListItem {Text = "普通", Value = "普通"},
                new SelectListItem {Text = "急件", Value = "急件"}
            };

            return View(todoListsViewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult TodoCreate(TodoListsViewModel todoListsViewModel)
        {
            if (!ModelState.IsValid)
            {
                todoListsViewModel.listTypes = new List<SelectListItem>
                {
                    new SelectListItem {Text = "普通", Value = "普通"},
                    new SelectListItem {Text = "急件", Value = "急件"}
                };
                return View(todoListsViewModel);
            }
            if (User.Identity.IsAuthenticated)
            {
                _token = User.FindFirst("Token").Value;
            }
            TodoListModel todoList = apiRequest.TodoCreate(todoListsViewModel, _token);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult TodoDelete(string Id)
        {
            if (User.Identity.IsAuthenticated)
            {
                _token = User.FindFirst("Token").Value;
            }
            bool result = apiRequest.TodDelete(Id, _token);

            return RedirectToAction("Index", "Home");
        }


        public IActionResult TodoUpdate(string Id)
        {
            if (User.Identity.IsAuthenticated)
            {
                _token = User.FindFirst("Token").Value;
            }
            TodoListModel todoList = apiRequest.GetTodoListById(Id, _token);

            TodoListsViewModel todoListsViewModel = new TodoListsViewModel();

            todoListsViewModel.title = todoList.title;
            todoListsViewModel.content = todoList.content;
            todoListsViewModel.listType = todoList.listType;

            ViewBag.listId = todoList.listId;

            todoListsViewModel.listTypes = new List<SelectListItem>
            {
                new SelectListItem {Text = "普通", Value = "普通"},
                new SelectListItem {Text = "急件", Value = "急件"}
            };

            return View(todoListsViewModel);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult TodoUpdateInfo(string Id, TodoListsViewModel todoListsViewModel)
        {
            if (!ModelState.IsValid)
            {
                todoListsViewModel.listTypes = new List<SelectListItem>
                {
                    new SelectListItem {Text = "普通", Value = "普通"},
                    new SelectListItem {Text = "急件", Value = "急件"}
                };
                return View(todoListsViewModel);
            }
            if (User.Identity.IsAuthenticated)
            {
                _token = User.FindFirst("Token").Value;
            }
            TodoListModel todoList = apiRequest.TodoUpdate(Id, todoListsViewModel, _token);

            return RedirectToAction("TodoDetail", "Home", new { Id = Id });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
