using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }

        public List<TodoItem> TodoItems { get; set; }
    }
}
