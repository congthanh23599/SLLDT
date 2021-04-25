namespace SoLienLacDienTu.Models.LamQuenCodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SinhVien_LopMon
    {
        [Key]
        public int ST { get; set; }

        [Required]
        [StringLength(50)]
        public string MaSV { get; set; }

        [Required]
        [StringLength(10)]
        public string MaLM { get; set; }

        public virtual LopMon LopMon { get; set; }

        public virtual SinhVien SinhVien { get; set; }
    }
}
