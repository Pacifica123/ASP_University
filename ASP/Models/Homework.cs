using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ASP.Models
{
    public class Homework
    {
        public int Id { get; set; }
        [DisplayName("Задача")]
        public string Name { get; set; }
        [DisplayName("Предмет")]
        public int? ItemLessonId { get; set; }
        public ItemLesson ItemLesson { get; set; }
        [DisplayName("Группа")]
        public int? GroupStudId { get; set; }
        public GroupStud GroupStud { get; set; }
    }
}