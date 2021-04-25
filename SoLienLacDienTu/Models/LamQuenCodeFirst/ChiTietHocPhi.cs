namespace SoLienLacDienTu.Models.LamQuenCodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietHocPhi")]
    public partial class ChiTietHocPhi
    {
        [Key]
        [StringLength(10)]
        public string MaMon { get; set; }

        public double? TCHP { get; set; }

        public int? PhiTL { get; set; }

        public long? HocPhi { get; set; }

        public long? SoTienPhaiDong { get; set; }

        public virtual MonHoc MonHoc { get; set; }
    }
}
