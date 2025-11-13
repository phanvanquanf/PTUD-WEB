using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace aznews.Models
{
    [Table("tblBaiTap")]
    public class tblBaiTap
    {
        [Key]
        public long IDBaiTap { get; set; }
        public bool? TrangThai { get; set; }

        [Required(ErrorMessage = "Vui lòng không được để trống.")]
        public string? TenBaiTap { get; set; }

        public string? Link { get; set; }

        public int HoatDong { get; set; }

        [ForeignKey("Menu")]
        public int MenuID { get; set; }
        public virtual tblMenu? Menu { get; set; }


        [Required(ErrorMessage = "Vui lòng chọn khóa học.")]

        [ForeignKey("KhoaHoc")]
        public long IDKhoaHoc { get; set; }
        public virtual tblKhoaHoc? KhoaHoc { get; set; }


        [Required(ErrorMessage = "Vui lòng chọn bài giảng.")]

        [ForeignKey("BaiGiang")]
        public long IDBaiGiang { get; set; }

        public virtual tblBaiGiang? BaiGiang { get; set; }


        [Required(ErrorMessage = "Vui lòng chọn giáo viên.")]

        [ForeignKey("GiaoVien")]
        public long GVienID { get; set; }
        public virtual tblGiaoVien? GiaoVien { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn chương.")]

        [ForeignKey("Chuong")]
        public long IDChuong { get; set; }
        public virtual tblChuong? Chuong { get; set; }
    }
}