using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ASP.Models
{
    public class Student
    {
        public int Id { get; set; } //ContextId пользователя
        public string Name { get; set; } //дублирование (for DeBug)
        [DisplayName("Группа")]
        public int? GroupStudId { get; set; }
        public virtual GroupStud GroupStud { get; set; }
    }
}