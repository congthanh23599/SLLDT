namespace SoLienLacDienTu.Models.LamQuenCodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GiangVien_Nganh
    {
        [Key]
        public int Stt { get; set; }

        [StringLength(50)]
        public string MaGV { get; set; }

        [StringLength(50)]
        public string MaNganh { get; set; }

        public virtual GiangVien GiangVien { get; set; }

        public virtual Nganh Nganh { get; set; }
    }
}
