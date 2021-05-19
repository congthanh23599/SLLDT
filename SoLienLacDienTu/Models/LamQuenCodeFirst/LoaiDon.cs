namespace SoLienLacDienTu.Models.LamQuenCodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoaiDon")]
    public partial class LoaiDon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoaiDon()
        {
            Dons = new HashSet<Don>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int STT { get; set; }

        [Key]
        [StringLength(10)]
        public string IDLD { get; set; }

        [StringLength(50)]
        public string TenLoai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Don> Dons { get; set; }
    }
}
