using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aznews.Models
{
    [Table("tblKhoaHoc")]

    public class tblKhoaHoc
    {
        [Key]

        public long IDKhoaHoc { get; set; }

        [Required(ErrorMessage = "Vui lòng điền vào chỗ trống")]
        public string? TenKH { get; set; }

        [Required(ErrorMessage = "Vui lòng điền vào chỗ trống")]
        public string? HocPhi { get; set; }

        [Required(ErrorMessage = "Vui lòng điền vào chỗ trống")]
        public string? ThoiGian { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ảnh")]
        public string? Anh { get; set; }

        [Required(ErrorMessage = "Vui lòng điền vào chỗ trống")]
        public int SoBaiGiang { get; set; }

        public int Lop { get; set; }

        public bool? HoatDong { get; set; }

        [ForeignKey("Menu")]
        public int MenuID { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn giáo viên")]

        [ForeignKey("GiaoVien")]
        public long GvienID { get; set; }
        public virtual tblGiaoVien? GiaoVien { get; set; }


        [Required(ErrorMessage = "Vui lòng chọn môn")]

        [ForeignKey("Mon")]
        public long IDMon { get; set; }
        public virtual tblMon? Mon { get; set; }

    }
}