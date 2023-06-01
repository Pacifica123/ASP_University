using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ASP.Models
{
    public class Rasp
    {
        public int Id { get; set; }
        [DisplayName("Группа")]
        public int? GroupStudId { get; set; }
        public virtual GroupStud GroupStud { get; set; }
        [DisplayName("Предмет")]
        public int? ItemLessonId { get; set; }
        public virtual ItemLesson ItemLesson { get; set; }
        [DisplayName("Аудитория")]
        public int? CabinetId { get; set; }
        public virtual Cabinet Cabinet { get; set; }
        [DisplayName("Дата занятия")]
        public DateTime Date { get; set; }
    }
}