using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aznews.Models
{
    [Table("viewBaiGiang")]
    public class viewBaiGiang
    {
        [Key]
        public long IDBaiGiang { get; set; }

        public string? TenBaiHoc { get; set; }

        public string? NDung { get; set; }

        public string? MoTa { get; set; }

        public int SoLuotHoc { get; set; }

        public string? Link { get; set; }

        public string? TenChuong { get; set; }

        public int ThoiLuong { get; set; }

        public bool? HoatDong { get; set; }

        public int ThuTu { get; set; }

        public int MenuID { get; set; }

        public string? TenKH { get; set; }

        public string? MenuName { get; set; }

        public long IDKhoaHoc { get; set; }

        public long GvienID { get; set; }

    }
}