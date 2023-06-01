using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ASP.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        [DisplayName("Фамилия Преподавателя")]
        public string Name { get; set; }
    }
}