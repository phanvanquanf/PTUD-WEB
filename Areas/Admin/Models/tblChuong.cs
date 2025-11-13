using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aznews.Models
{
    [Table("tblChuong")]
    public class tblChuong
    {

        [Key]

        public long IDChuong { get; set; }

        public string? TenChuong { get; set; }

        public int ThuTu { get; set; }

        [ForeignKey("KhoaHoc")]
        public long IDKhoaHoc { get; set; }
        public virtual tblKhoaHoc? KhoaHoc { get; set; }
    }
}