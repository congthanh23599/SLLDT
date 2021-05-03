namespace SoLienLacDienTu.Models.LamQuenCodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GV_LM
    {
        [Key]
        public int STT { get; set; }

        [StringLength(10)]
        public string MaLM { get; set; }

        [StringLength(50)]
        public string MaGV { get; set; }

        public virtual GiangVien GiangVien { get; set; }

        public virtual LopMon LopMon { get; set; }
    }
}
