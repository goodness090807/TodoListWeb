using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoListWeb.ViewModels.TodoList
{
    public class TodoListsViewModel
    {
        [Required(ErrorMessage = "標題為必填欄位")]
        [MaxLength(50, ErrorMessage = "標題不能超過50個字")]
        [Display(Name = "標題")]
        public string title { get; set; }

        [Required(ErrorMessage = "內文為必填欄位")]
        [MaxLength(1000, ErrorMessage = "標題不能超過1000個字")]
        [Display(Name = "內文")]
        public string content { get; set; }

        [Required(ErrorMessage = "類型必需選擇")]
        [Display(Name = "類型")]
        public string listType { get; set; }

        public List<SelectListItem> listTypes { get; set; }

    }
}
