namespace SoLienLacDienTu.Models.LamQuenCodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SV_MON
    {
        [Key]
        public int STT { get; set; }

        [StringLength(50)]
        public string MaSV { get; set; }

        [StringLength(10)]
        public string MaMon { get; set; }

        public bool? DaHoc { get; set; }

        [StringLength(50)]
        public string MaMonTienquyet { get; set; }

        public virtual MonHoc MonHoc { get; set; }

        public virtual SinhVien SinhVien { get; set; }
    }
}
