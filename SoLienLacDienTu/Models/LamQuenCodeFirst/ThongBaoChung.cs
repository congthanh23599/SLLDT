namespace SoLienLacDienTu.Models.LamQuenCodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThongBaoChung")]
    public partial class ThongBaoChung
    {
        [Key]
        public int MaTB { get; set; }

        [Required]
        [StringLength(50)]
        public string TieuDe { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string NoiDung { get; set; }

        public DateTime NgayDang { get; set; }
    }
}
