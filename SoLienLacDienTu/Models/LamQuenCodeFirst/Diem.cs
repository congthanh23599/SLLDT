namespace SoLienLacDienTu.Models.LamQuenCodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Diem")]
    public partial class Diem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Diem()
        {
            DiemCTs = new HashSet<DiemCT>();
        }

        [Key]
        public int IDDiem { get; set; }

        [StringLength(50)]
        public string MaSV { get; set; }

        public double? DiemTBTichLuy { get; set; }

        public double? DiemTBHK { get; set; }

        public int? SoTinChiDat { get; set; }

        public int? SoTinChiTL { get; set; }

        [Required]
        [StringLength(10)]
        public string NamHoc { get; set; }

        public int HocKy { get; set; }

        public virtual SinhVien SinhVien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiemCT> DiemCTs { get; set; }
    }
}
