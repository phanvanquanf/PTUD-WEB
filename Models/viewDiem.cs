using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aznews.Models
{
    [Table("viewDiem")]
    public class viewDiem()
    {
        [Key]

        public long IDDiem { get; set; }

        public long IDHocVien { get; set; }

        public long GvienID { get; set; }

        public long IDBaiTap { get; set; }

        public long IDKhoaHoc { get; set; }
        public string? HoTen { get; set; }

        public string? HoTenHV { get; set; }
        public double Diem { get; set; }

        public int XepLoai { get; set; }

        public string? TenBaiTap { get; set; }

        public int MenuID { get; set; }

        public string? TenKH { get; set; }

        public string? TenChuong { get; set; }

        public string? Mon { get; set; }

        public long IDMon { get; set; }


    }
}