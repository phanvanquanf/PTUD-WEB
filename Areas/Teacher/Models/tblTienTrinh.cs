using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aznews.Models
{
    [Table("tblTienTrinh")]
    public class tblTienTrinh
    {
        [Key]

        public long IDTienTrinh { get; set; }

        public int TrangThai { get; set; }

        public DateTime NgayBatDau { get; set; }

        public DateTime NgayHoanThanh { get; set; }

        public double TienTrinh { get; set; }

        [ForeignKey("Menu")]
        public int MenuID { get; set; }
        public virtual tblMenu? Menu { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn khóa học.")]

        [ForeignKey("KhoaHoc")]
        public long IDKhoaHoc { get; set; }
        public virtual tblKhoaHoc? KhoaHoc { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn giáo viên.")]

        [ForeignKey("GiaoVien")]
        public long GVienID { get; set; }
        public virtual tblGiaoVien? GiaoVien { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn bài giảng.")]

        [ForeignKey("BaiGiang")]
        public long IDBaiGiang { get; set; }
        public virtual tblBaiGiang? BaiGiang { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn học viên.")]

        [ForeignKey("HocVien")]
        public long IDHocVien { get; set; }
        public virtual tblHocVien? HocVien { get; set; }

    }
}