namespace SoLienLacDienTu.Models.LamQuenCodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SV_LOP
    {
        [Key]
        public int STT { get; set; }

        [StringLength(10)]
        public string Malop { get; set; }

        [StringLength(50)]
        public string MaSV { get; set; }

        public virtual Lop Lop { get; set; }

        public virtual SinhVien SinhVien { get; set; }
    }
}
