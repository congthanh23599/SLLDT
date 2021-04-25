namespace SoLienLacDienTu.Models.LamQuenCodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DiemCT")]
    public partial class DiemCT
    {
        [Key]
        public int IDDiemCT { get; set; }

        public int? IDDiem { get; set; }

        [Required]
        [StringLength(10)]
        public string MaMon { get; set; }

        [Required]
        [StringLength(10)]
        public string NamHoc { get; set; }

        public double? DiemGK { get; set; }

        public double? DiemCK { get; set; }

        public double? DiemTBM { get; set; }

        public int HocKy { get; set; }

        public bool? KQ { get; set; }

        public virtual Diem Diem { get; set; }

        public virtual MonHoc MonHoc { get; set; }
    }
}
