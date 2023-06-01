using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ASP.Models
{
    public class Cabinet
    {
        public int Id { get; set; }
        [DisplayName("Аудитория")]
        public string Name { get; set; }
    }
}