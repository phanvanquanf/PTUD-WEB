using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aznews.Models
{
    [Table("tblGiaoVien")]
    public class tblGiaoVien
    {
        [Key]
        public long GvienID { get; set; }

        [Required(ErrorMessage = "Vui lòng không được để trống.")]
        public string? HoTen { get; set; }

        [Required(ErrorMessage = "Vui lòng không được để trống.")]
        public string? NoiCongTac { get; set; }

        [Required(ErrorMessage = "Vui lòng không được để trống.")]
        public string? GioiThieu { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ảnh.")]
        public string? AnhGThieu { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ảnh.")]
        public string? AnhTheoDoi { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ảnh.")]
        public string? AnhCaNhan { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ảnh.")]
        public string? AnhBia { get; set; }
        public bool? HoatDong { get; set; }

        [Required(ErrorMessage = "Vui lòng không được để trống.")]
        public string? HocVi { get; set; }
        public string? PhongCach { get; set; }
        public string? DoiNet { get; set; }
        public string? ThanhTich { get; set; }

        [Required(ErrorMessage = "Vui lòng không được để trống.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Vui lòng không được để trống.")]
        public string? Facebook { get; set; }

        [ForeignKey("MenuID")]
        public int MenuID { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn môn.")]

        [ForeignKey("Mon")]
        public long IDMon { get; set; }
        public virtual tblMon? Mon { get; set; }
    }
}
