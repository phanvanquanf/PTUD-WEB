using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aznews.Areas.Admin.Models;
using aznews.Areas.Teacher.Models;
using Microsoft.EntityFrameworkCore;

namespace aznews.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public required DbSet<tblMenu> Menus { get; set; }
        public required DbSet<viewPostMenu> viewPostMenus { get; set; }

        public required DbSet<AdminMenu> AdminMenus { get; set; }

        public required DbSet<TeacherMenu> TeacherMenus { get; set; }

        public required DbSet<tblDienDan> DienDans { get; set; }

        public required DbSet<tblChuong> Chuongs { get; set; }

        public required DbSet<tblGiaoVien> GiaoViens { get; set; }

        public required DbSet<tblMon> Mons { get; set; }

        public required DbSet<tblKhoaHoc> KhoaHocs { get; set; }

        public required DbSet<tblBaiGiang> BaiGiangs { get; set; }

        public required DbSet<tblHocVien> HocViens { get; set; }

        public required DbSet<tblBaiTap> BaiTaps { get; set; }

        public required DbSet<tblDiem> Diems { get; set; }
        public required DbSet<AdminUser> AdminUsers { get; set; }


        public required DbSet<tblTienTrinh> TienTrinhs { get; set; }

        public required DbSet<viewDienDan> viewDienDans { get; set; }

        public required DbSet<viewGiangVien> viewGiangViens { get; set; }

        public required DbSet<viewTopBaiViet> viewTopBaiViets { get; set; }

        public required DbSet<viewBaiGiang> viewBaiGiangs { get; set; }

        public required DbSet<viewLoiTriAn> viewLoiTriAns { get; set; }

        public required DbSet<viewKhoaHoc> viewKhoaHocs { get; set; }

        public required DbSet<viewTienTrinh> viewTienTrinhs { get; set; }

        public required DbSet<viewHocVien> viewHocViens { get; set; }

        public required DbSet<viewBaiTap> viewBaiTaps { get; set; }

        public required DbSet<viewDiem> viewDiems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblGiaoVien>().ToTable("tblGiaoVien");
            modelBuilder.Entity<tblMon>().ToTable("tblMon");
            modelBuilder.Entity<tblKhoaHoc>().ToTable("tblKhoaHoc");
            modelBuilder.Entity<tblGiaoVien>().ToTable("tblGiaoVien");
            modelBuilder.Entity<tblBaiGiang>().ToTable("tblBaiGiang");
            modelBuilder.Entity<tblHocVien>().ToTable("tblHocVien");
            modelBuilder.Entity<tblBaiTap>().ToTable("tblBaiTap");
            modelBuilder.Entity<tblChuong>().ToTable("tblChuong");
            modelBuilder.Entity<tblDiem>().ToTable("tblDiem");
            modelBuilder.Entity<tblTienTrinh>().ToTable("tblTienTrinh");
        }
    }
}