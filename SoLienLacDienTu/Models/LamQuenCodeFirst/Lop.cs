namespace SoLienLacDienTu.Models.LamQuenCodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Lop")]
    public partial class Lop
    {
        [Key]
        [StringLength(10)]
        public string Malop { get; set; }

        [StringLength(50)]
        public string MaGV { get; set; }

        public virtual GiangVien GiangVien { get; set; }
    }
}
