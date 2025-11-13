using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aznews.Models
{
    [Table("viewHocVien")]
    public class viewHocVien
    {
        [Key]

        public long IDHocVien { get; set; }

        public string? HoTenHV { get; set; }

        public DateOnly NgaySinh { get; set; }

        public string? GioiTinh { get; set; }

        public string? SoDienThoai { get; set; }

        public string? Email { get; set; }

        public string? DiaChi { get; set; }

        public DateOnly NgayDangKy { get; set; }

        public int Lop { get; set; }
        public string? AnhDaiDien { get; set; }

        public int TrangThai { get; set; }

        public int MenuID { get; set; }

        public string? MenuName { get; set; }
    }
}