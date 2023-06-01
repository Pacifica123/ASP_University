using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ASP.Models
{
    public class GroupStud
    {
        public int Id { get; set; }
        [DisplayName("Группа")]
        public string Name { get; set; }
    }
}