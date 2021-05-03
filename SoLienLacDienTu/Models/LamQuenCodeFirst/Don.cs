namespace SoLienLacDienTu.Models.LamQuenCodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Don")]
    public partial class Don
    {
        [Key]
        public int STT { get; set; }

        [StringLength(20)]
        public string MaSV { get; set; }

        [StringLength(50)]
        public string TenSV { get; set; }

        [StringLength(50)]
        public string LiDo { get; set; }

        [StringLength(100)]
        public string FileDon { get; set; }

        public DateTime? NgayDang { get; set; }

        public bool? TrangThai { get; set; }
    }
}
