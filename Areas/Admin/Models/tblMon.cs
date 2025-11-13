using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aznews.Models
{
    [Table("tblMon")]
    public class tblMon()
    {
        [Key]
        public long? IDMon { get; set; }

        public string? Mon { get; set; }

        public string? MoTa { get; set; }

        public bool? TrangThai { get; set; }
    }
}