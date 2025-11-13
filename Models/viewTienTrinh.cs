using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aznews.Models
{
    [Table("viewTienTrinh")]
    public class viewTienTrinh
    {
        [Key]

        public long IDTienTrinh { get; set; }

        public long IDKhoaHoc { get; set; }

        public long IDBaiGiang { get; set; }

        public int TrangThai { get; set; }

        public DateTime NgayBatDau { get; set; }

        public DateTime NgayHoanThanh { get; set; }

        public int MenuID { get; set; }

        public string? MenuName { get; set; }

        public string? TenKH { get; set; }

        public string? TenBaiHoc { get; set; }

        public string? TenChuong { get; set; }

        public string? Link { get; set; }

        public double TienTrinh { get; set; }

        public long GvienID { get; set; }

        public long IDHocVien { get; set; }
    }
}