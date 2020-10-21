using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoListWeb.Models
{
    public class TodoListModel
    {
        public string listId { get; set; }

        public string title { get; set; }

        public string content { get; set; }

        public string listType { get; set; }

        public DateTime createDateTime { get; set; }

        public DateTime updateDateTime { get; set; }
    }
}
