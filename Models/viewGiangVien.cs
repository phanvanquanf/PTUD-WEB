using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aznews.Models
{
    [Table("viewGiangVien")]
    public class viewGiangVien
    {
        [Key]
        public long GvienID { get; set; }

        public string? HoTen { get; set; }

        public string? NoiCongTac { get; set; }

        public string? GioiThieu { get; set; }

        public string? AnhGThieu { get; set; }

        public string? AnhTheoDoi { get; set; }

        public string? AnhCaNhan { get; set; }

        public string? AnhBia { get; set; }

        public bool? HoatDong { get; set; }

        public string? HocVi { get; set; }

        public int MenuID { get; set; }

        public long? IDMon { get; set; }

        public string? Mon { get; set; }

        public string? PhongCach { get; set; }

        public string? DoiNet { get; set; }

        public string? ThanhTich { get; set; }

        public string? Email { get; set; }

        public string? Facebook { get; set; }

        public string? MenuName { get; set; }
    }
}