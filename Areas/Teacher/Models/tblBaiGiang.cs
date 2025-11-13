using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aznews.Models
{
    [Table("tblBaiGiang")]
    public class tblBaiGiang
    {
        [Key]
        public long IDBaiGiang { get; set; }

        [Required(ErrorMessage = "Vui lòng không được để trống.")]

        public string? TenBaiHoc { get; set; }
        [Required(ErrorMessage = "Vui lòng không được để trống.")]

        public string? NDung { get; set; }

        public string? MoTa { get; set; }

        [Required(ErrorMessage = "Vui lòng không được để trống.")]

        public int SoLuotHoc { get; set; }

        [Required(ErrorMessage = "Vui lòng không được để trống.")]

        public string? Link { get; set; }

        [Required(ErrorMessage = "Vui lòng không được để trống.")]

        public int ThoiLuong { get; set; }

        public bool? HoatDong { get; set; }

        public int ThuTu { get; set; }

        [ForeignKey("Menu")]
        public int MenuID { get; set; }
        public virtual tblMenu? Menu { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn giáo viên.")]

        [ForeignKey("GiaoVien")]
        public long GVienID { get; set; }
        public virtual tblGiaoVien? GiaoVien { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn khóa học.")]

        [ForeignKey("KhoaHoc")]
        public long IDKhoaHoc { get; set; }
        public virtual tblKhoaHoc? KhoaHoc { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn chương.")]

        [ForeignKey("Chuong")]
        public long IDChuong { get; set; }
        public virtual tblChuong? Chuong { get; set; }
    }

}