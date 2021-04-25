namespace SoLienLacDienTu.Models.LamQuenCodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThongBao")]
    public partial class ThongBao
    {
        [Key]
        public int IDTB { get; set; }

        [Required]
        [StringLength(50)]
        public string MaGV { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string NoiDung { get; set; }

        public DateTime? NgayDang { get; set; }

        [StringLength(10)]
        public string MaLop { get; set; }

        [StringLength(10)]
        public string MaLM { get; set; }

        [StringLength(50)]
        public string MaSV { get; set; }

        public virtual GiangVien GiangVien { get; set; }

        public virtual LopMon LopMon { get; set; }
    }
}
