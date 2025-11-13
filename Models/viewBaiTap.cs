using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aznews.Models
{
    [Table("viewBaiTap")]
    public class viewBaiTap()
    {
        [Key]

        public long IDBaiTap { get; set; }

        public long IDKhoaHoc { get; set; }

        public long IDBaiGiang { get; set; }

        public bool? TrangThai { get; set; }

        public string? TenBaiTap { get; set; }

        public string? Link { get; set; }

        public int HoatDong { get; set; }

        public int MenuID { get; set; }

        public string? MenuName { get; set; }

        public string? TenKH { get; set; }

        public string? TenChuong { get; set; }

        public long GvienID { get; set; }

    }
}