namespace SoLienLacDienTu.Models.LamQuenCodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Admin")]
    public partial class Admin
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string TaiKhoan { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(50)]
        public string TenAdmin { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string PasswordEmail { get; set; }
    }
}
