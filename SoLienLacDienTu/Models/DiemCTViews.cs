using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoLienLacDienTu.Models
{
    public class DiemCTViews
    {
        public int? IDD { get; set; }
        public string MaMon { get; set; }
        public string MaSV { get; set; }
        public Double? DiemGK { get; set; }
        public Double? DiemCK { get; set; }
        public Double? DTBMon { get; set; }
        public bool? KQ { get; set; }
        public string NamHoc { get; set; }

        public int HocKy { get; set; }
    }
}