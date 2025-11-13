using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aznews.Models
{
    [Table("AdminUser")]
    public class AdminUser
    {
        [Key]
        public int MaNguoiDung { get; set; }
        public string? HoVaTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? TenDangNhap { get; set; }
        public string? MatKhau { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public int? VaiTro { get; set; }
        public bool TrangThai { get; set; }
    }
}