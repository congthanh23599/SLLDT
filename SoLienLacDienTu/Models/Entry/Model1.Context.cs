//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SoLienLacDienTu.Models.Entry
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SoLienLacDTEntities1 : DbContext
    {
        public SoLienLacDTEntities1()
            : base("name=SoLienLacDTEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<ChiTietHocPhi> ChiTietHocPhis { get; set; }
        public virtual DbSet<Diem> Diems { get; set; }
        public virtual DbSet<DiemCT> DiemCTs { get; set; }
        public virtual DbSet<Don> Dons { get; set; }
        public virtual DbSet<GiangVien> GiangViens { get; set; }
        public virtual DbSet<GiangVien_Nganh> GiangVien_Nganh { get; set; }
        public virtual DbSet<GV_LM> GV_LM { get; set; }
        public virtual DbSet<LichThi> LichThis { get; set; }
        public virtual DbSet<Lop> Lops { get; set; }
        public virtual DbSet<LopMon> LopMons { get; set; }
        public virtual DbSet<MonHoc> MonHocs { get; set; }
        public virtual DbSet<Nganh> Nganhs { get; set; }
        public virtual DbSet<PH> PHs { get; set; }
        public virtual DbSet<SinhVien> SinhViens { get; set; }
        public virtual DbSet<SinhVien_LopMon> SinhVien_LopMon { get; set; }
        public virtual DbSet<SV_LOP> SV_LOP { get; set; }
        public virtual DbSet<SV_MON> SV_MON { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<ThoiKhoaBieu> ThoiKhoaBieux { get; set; }
        public virtual DbSet<ThongBao> ThongBaos { get; set; }
        public virtual DbSet<ThongBaoChung> ThongBaoChungs { get; set; }
    }
}
