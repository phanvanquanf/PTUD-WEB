using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aznews.Models
{
    [Table("tblDiem")]
    public class tblDiem
    {

        [Key]
        public long IDDiem { get; set; }
        public double Diem { get; set; }
        public int XepLoai { get; set; }

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

        [Required(ErrorMessage = "Vui lòng chọn bài tập.")]

        [ForeignKey("BaiTap")]
        public long IDBaiTap { get; set; }
        public virtual tblBaiTap? BaiTap { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn học viên.")]

        [ForeignKey("HocVien")]
        public long IDHocVien { get; set; }
        public virtual tblHocVien? HocVien { get; set; }

        [ForeignKey("Mon")]
        public long IDMon { get; set; }
        public virtual tblMon? Mon { get; set; }
    }
}