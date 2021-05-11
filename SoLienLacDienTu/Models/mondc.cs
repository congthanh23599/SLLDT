using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoLienLacDienTu.Models
{
    public class mondc
    {
   

        [StringLength(50)]
        public string MaSV { get; set; }

        [StringLength(10)]
        public string MaMon { get; set; }

        public bool? DaHoc { get; set; }

        [StringLength(50)]
        public string MaMonTienquyet { get; set; }

        public string MH { get; set; }

        public string SV { get; set; }
       
    }
}