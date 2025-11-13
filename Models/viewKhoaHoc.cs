using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aznews.Models
{
    [Table("viewKhoaHoc")]
    public class viewKhoaHoc
    {
        [Key]

        public long IDKhoaHoc { get; set; }

        public string? TenKH { get; set; }

        public string? HocPhi { get; set; }

        public string? Mon { get; set; }

        public int Lop { get; set; }

        public string? ThoiGian { get; set; }

        public string? Anh { get; set; }
        public bool? HoatDong { get; set; }

        public string? HoTen { get; set; }

        public int MenuID { get; set; }

        public string? MenuName { get; set; }

        public int SoBaiGiang { get; set; }

        public long GvienID { get; set; }

        public long IDMon { get; set; }
    }
}