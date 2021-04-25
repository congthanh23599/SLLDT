using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SoLienLacDienTu.Models.LamQuenCodeFirst
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<ChiTietHocPhi> ChiTietHocPhis { get; set; }
        public virtual DbSet<Diem> Diems { get; set; }
        public virtual DbSet<DiemCT> DiemCTs { get; set; }
        public virtual DbSet<GiangVien> GiangViens { get; set; }
        public virtual DbSet<GiangVien_Nganh> GiangVien_Nganh { get; set; }
        public virtual DbSet<LichThi> LichThis { get; set; }
        public virtual DbSet<Lop> Lops { get; set; }
        public virtual DbSet<LopMon> LopMons { get; set; }
        public virtual DbSet<MonHoc> MonHocs { get; set; }
        public virtual DbSet<Nganh> Nganhs { get; set; }
        public virtual DbSet<PH> PHs { get; set; }
        public virtual DbSet<SinhVien> SinhViens { get; set; }
        public virtual DbSet<SinhVien_LopMon> SinhVien_LopMon { get; set; }
        public virtual DbSet<SV_MON> SV_MON { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<ThoiKhoaBieu> ThoiKhoaBieux { get; set; }
        public virtual DbSet<ThongBao> ThongBaos { get; set; }
        public virtual DbSet<ThongBaoChung> ThongBaoChungs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>()
                .Property(e => e.TaiKhoan)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietHocPhi>()
                .Property(e => e.MaMon)
                .IsUnicode(false);

            modelBuilder.Entity<Diem>()
                .Property(e => e.MaSV)
                .IsUnicode(false);

            modelBuilder.Entity<Diem>()
                .Property(e => e.NamHoc)
                .IsUnicode(false);

            modelBuilder.Entity<DiemCT>()
                .Property(e => e.MaMon)
                .IsUnicode(false);

            modelBuilder.Entity<DiemCT>()
                .Property(e => e.NamHoc)
                .IsUnicode(false);

            modelBuilder.Entity<GiangVien>()
                .Property(e => e.MaGV)
                .IsUnicode(false);

            modelBuilder.Entity<GiangVien>()
                .Property(e => e.TenGV)
                .IsFixedLength();

            modelBuilder.Entity<GiangVien>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<GiangVien>()
                .HasMany(e => e.ThongBaos)
                .WithRequired(e => e.GiangVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GiangVien>()
                .HasMany(e => e.MonHocs)
                .WithMany(e => e.GiangViens)
                .Map(m => m.ToTable("GV_Mon").MapLeftKey("MaGV").MapRightKey("MaMon"));

            modelBuilder.Entity<GiangVien_Nganh>()
                .Property(e => e.MaGV)
                .IsUnicode(false);

            modelBuilder.Entity<GiangVien_Nganh>()
                .Property(e => e.MaNganh)
                .IsUnicode(false);

            modelBuilder.Entity<LichThi>()
                .Property(e => e.MaMon)
                .IsUnicode(false);

            modelBuilder.Entity<LichThi>()
                .Property(e => e.MaSV)
                .IsUnicode(false);

            modelBuilder.Entity<LichThi>()
                .Property(e => e.NhomThi)
                .IsUnicode(false);

            modelBuilder.Entity<LichThi>()
                .Property(e => e.GioBD)
                .HasPrecision(5);

            modelBuilder.Entity<LichThi>()
                .Property(e => e.Phong)
                .IsUnicode(false);

            modelBuilder.Entity<LichThi>()
                .Property(e => e.NamHoc)
                .IsUnicode(false);

            modelBuilder.Entity<Lop>()
                .Property(e => e.Malop)
                .IsUnicode(false);

            modelBuilder.Entity<Lop>()
                .Property(e => e.MaGV)
                .IsUnicode(false);

            modelBuilder.Entity<LopMon>()
                .Property(e => e.MaLM)
                .IsUnicode(false);

            modelBuilder.Entity<LopMon>()
                .Property(e => e.Lop)
                .IsUnicode(false);

            modelBuilder.Entity<LopMon>()
                .Property(e => e.MaMon)
                .IsUnicode(false);

            modelBuilder.Entity<LopMon>()
                .HasMany(e => e.SinhVien_LopMon)
                .WithRequired(e => e.LopMon)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MonHoc>()
                .Property(e => e.MaMon)
                .IsUnicode(false);

            modelBuilder.Entity<MonHoc>()
                .Property(e => e.MaMonTienQuyet)
                .IsUnicode(false);

            modelBuilder.Entity<MonHoc>()
                .HasOptional(e => e.ChiTietHocPhi)
                .WithRequired(e => e.MonHoc);

            modelBuilder.Entity<MonHoc>()
                .HasMany(e => e.DiemCTs)
                .WithRequired(e => e.MonHoc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MonHoc>()
                .HasMany(e => e.LichThis)
                .WithRequired(e => e.MonHoc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MonHoc>()
                .HasMany(e => e.LopMons)
                .WithRequired(e => e.MonHoc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MonHoc>()
                .HasMany(e => e.ThoiKhoaBieux)
                .WithRequired(e => e.MonHoc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MonHoc>()
                .HasMany(e => e.Nganhs)
                .WithMany(e => e.MonHocs)
                .Map(m => m.ToTable("Nganh_Mon").MapLeftKey("MaMon").MapRightKey("MaNganh"));

            modelBuilder.Entity<Nganh>()
                .Property(e => e.MaNganh)
                .IsUnicode(false);

            modelBuilder.Entity<Nganh>()
                .Property(e => e.TenNganh)
                .IsFixedLength();

            modelBuilder.Entity<Nganh>()
                .HasMany(e => e.SinhViens)
                .WithMany(e => e.Nganhs)
                .Map(m => m.ToTable("SinhVien_Nganh").MapLeftKey("MaNganh").MapRightKey("MaSV"));

            modelBuilder.Entity<PH>()
                .Property(e => e.MaPH)
                .IsUnicode(false);

            modelBuilder.Entity<PH>()
                .Property(e => e.SDTPH)
                .IsUnicode(false);

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.MaSV)
                .IsUnicode(false);

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.TenSV)
                .IsFixedLength();

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.GioiTinh)
                .IsFixedLength();

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.MaPH)
                .IsUnicode(false);

            modelBuilder.Entity<SinhVien>()
                .HasMany(e => e.LichThis)
                .WithRequired(e => e.SinhVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SinhVien>()
                .HasMany(e => e.SinhVien_LopMon)
                .WithRequired(e => e.SinhVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SinhVien_LopMon>()
                .Property(e => e.MaSV)
                .IsUnicode(false);

            modelBuilder.Entity<SinhVien_LopMon>()
                .Property(e => e.MaLM)
                .IsUnicode(false);

            modelBuilder.Entity<SV_MON>()
                .Property(e => e.MaSV)
                .IsUnicode(false);

            modelBuilder.Entity<SV_MON>()
                .Property(e => e.MaMon)
                .IsUnicode(false);

            modelBuilder.Entity<SV_MON>()
                .Property(e => e.MaMonTienquyet)
                .IsUnicode(false);

            modelBuilder.Entity<ThoiKhoaBieu>()
                .Property(e => e.MaMon)
                .IsUnicode(false);

            modelBuilder.Entity<ThoiKhoaBieu>()
                .Property(e => e.MaGV)
                .IsUnicode(false);

            modelBuilder.Entity<ThoiKhoaBieu>()
                .Property(e => e.Phong)
                .IsUnicode(false);

            modelBuilder.Entity<ThoiKhoaBieu>()
                .Property(e => e.MaLM)
                .IsUnicode(false);

            modelBuilder.Entity<ThongBao>()
                .Property(e => e.MaGV)
                .IsUnicode(false);

            modelBuilder.Entity<ThongBao>()
                .Property(e => e.NoiDung)
                .IsUnicode(false);

            modelBuilder.Entity<ThongBao>()
                .Property(e => e.MaLop)
                .IsUnicode(false);

            modelBuilder.Entity<ThongBao>()
                .Property(e => e.MaLM)
                .IsUnicode(false);

            modelBuilder.Entity<ThongBao>()
                .Property(e => e.MaSV)
                .IsUnicode(false);
        }
    }
}
