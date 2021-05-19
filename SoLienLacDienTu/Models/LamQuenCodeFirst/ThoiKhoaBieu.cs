namespace SoLienLacDienTu.Models.LamQuenCodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThoiKhoaBieu")]
    public partial class ThoiKhoaBieu
    {
        [Key]
        public int IDTKB { get; set; }

        [Required]
        [StringLength(10)]
        public string MaMon { get; set; }

        [StringLength(10)]
        public string MaLM { get; set; }

        [Required]
        [StringLength(50)]
        public string TenMon { get; set; }

        [Required]
        [StringLength(50)]
        public string MaGV { get; set; }

        [Required]
        [StringLength(10)]
        public string Phong { get; set; }

        public int TietBD { get; set; }

        public int Thu { get; set; }

        public int? TH { get; set; }

        public int ST { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ThoiGianBD { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ThoiGianKT { get; set; }

        public int? Nam { get; set; }

        public int HocKy { get; set; }

        public int? TongNgayHoc { get; set; }

        public virtual MonHoc MonHoc { get; set; }
    }
}
