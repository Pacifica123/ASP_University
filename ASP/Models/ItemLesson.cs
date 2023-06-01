using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ASP.Models
{
    public class ItemLesson
    {
        public int Id { get; set; }
        [DisplayName("Предмет")]
        public string Name { get; set; }
        [DisplayName("Преподователь")]
        public int? TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}