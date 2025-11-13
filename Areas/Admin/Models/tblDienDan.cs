using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aznews.Models
{
    [Table("tblDienDan")]
    public class tblDienDan
    {
        [Key]
        public long IDBaiViet { get; set; }

        [Required(ErrorMessage = "Tên bài viết là bắt buộc.")]
        public string? TenBaiViet { get; set; }

        [Required(ErrorMessage = "Tiêu đề là bắt buộc.")]
        public string? TieuDe { get; set; }

        [Required(ErrorMessage = "Nội dung bài viết là bắt buộc.")]
        public string? NoiDung { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ảnh.")]
        public string? Anh { get; set; }

        public int ThuTu { get; set; }

        public DateTime? NgayTao { get; set; }

        public bool? HoatDong { get; set; }

        [ForeignKey("MenuID")]
        public int? MenuID { get; set; }

        [NotMapped]
        public string? MenuName { get; set; }
    }
}
