using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aznews.Models
{
    [Table("tblHocVien")]
    public class tblHocVien
    {
        [Key]

        public long IDHocVien { get; set; }

        [Required(ErrorMessage = "Vui lòng không được để trống.")]
        public string? HoTenHV { get; set; }

        public DateOnly NgaySinh { get; set; }

        public string? GioiTinh { get; set; }

        [Required(ErrorMessage = "Vui lòng không được để trống.")]
        public string? SoDienThoai { get; set; }

        [Required(ErrorMessage = "Vui lòng không được để trống.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ảnh.")]
        public string? AnhDaiDien { get; set; }

        [Required(ErrorMessage = "Vui lòng không được để trống.")]
        public string? DiaChi { get; set; }

        public int Lop { get; set; }

        public DateOnly NgayDangKy { get; set; }

        public int TrangThai { get; set; }

        [ForeignKey("Menu")] // Liên kết với Navigation Property
        public int MenuID { get; set; }
    }
}