using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aznews.Models
{
    [Table("viewTaiKhoan")]
    public class viewTaiKhoan()
    {
        [Key]

        public long IDTaiKhoan { get; set; }

        public long IDHocVien { get; set; }

        public long GvienID { get; set; }

        public int TrangThai { get; set; }

        public string? TenDangNhap { get; set; }

        public string? MatKhau { get; set; }

        public string? Email { get; set; }

        public int MenuID { get; set; }

        public string? MenuName { get; set; }

        public string? SoDienThoai { get; set; }

        public int VaiTro { get; set; }

        public DateOnly NgayTao { get; set; }

    }
}