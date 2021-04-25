namespace SoLienLacDienTu.Models.LamQuenCodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LichThi")]
    public partial class LichThi
    {
        [Key]
        public int IDLichThi { get; set; }

        [Required]
        [StringLength(10)]
        public string MaMon { get; set; }

        [Required]
        [StringLength(50)]
        public string MaSV { get; set; }

        [Required]
        [StringLength(50)]
        public string TenMon { get; set; }

        [Required]
        [StringLength(5)]
        public string NhomThi { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayThi { get; set; }

        public TimeSpan GioBD { get; set; }

        public int SoPhut { get; set; }

        [Required]
        [StringLength(10)]
        public string Phong { get; set; }

        [Required]
        [StringLength(20)]
        public string GhiChu { get; set; }

        [Required]
        [StringLength(10)]
        public string NamHoc { get; set; }

        public int HocKy { get; set; }

        public virtual MonHoc MonHoc { get; set; }

        public virtual SinhVien SinhVien { get; set; }
    }
}
