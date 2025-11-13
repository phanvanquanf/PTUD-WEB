using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aznews.Areas.Teacher.Models
{
    [Table("TeacherMenu")]
    public class TeacherMenu
    {
        [Key]

        public long? TeacherMenuID { get; set; }
        public string? ItemName { get; set; }
        public int ItemLevel { get; set; }
        public int ParentLevel { get; set; }
        public int ItemOrder { get; set; }
        public bool? isActive { get; set; }
        public string? ItemTarget { get; set; }
        public string? AreaName { get; set; }
        public string? ControllerName { get; set; }
        public string? ActionName { get; set; }
        public string? Icon { get; set; }
        public string? IdName { get; set; }

    }
}