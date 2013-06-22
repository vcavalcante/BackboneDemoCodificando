using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoAppBackboneJS.Models
{
    public class Todo
    {
        public int id { get; set; }
        public string description { get; set; }
        public string status { get; set; }
    }
}