using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aznews.Models
{
    [Table("viewTopBaiViet")]
    public class viewTopBaiViet
    {
        [Key]

        public long IDBaiViet { get; set; }

        public string? TenBaiViet { get; set; }

        public string? TieuDe { get; set; }

        public string? NoiDung { get; set; }

        public string? Anh { get; set; }

        public string? Link { get; set; }

        public DateTime? NgayTao { get; set; }

        public bool? HoatDong { get; set; }

        public int ThuTu { get; set; }

        public int MenuID { get; set; }

        public string? MenuName { get; set; }

    }
}