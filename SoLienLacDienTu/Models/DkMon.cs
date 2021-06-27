using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoLienLacDienTu.Models
{
    public class DkMon
    {
     
        public string MaMon { get; set; }
        public string TenMon { get; set; }
        public int? SoTinChi { get; set; }
        public string MaLM { get; set; }
        public string MaGV { get; set; }
        public string Phong { get; set; }
        public int? TietBD { get; set; }
        public int? TH { get; set; }
        public int? Thu { get; set; }
        public int? ST { get; set; }

        //[DataType(DataType.Date)]
        public DateTime? ThoiGianBD { get; set; }
        //[DataType(DataType.Date)]
        public DateTime? ThoiGianKT { get; set; }
        public int? HocKy { get; set; }
     
        public string Montienquyet { get; set; }
        public int? Nam { get; set; }

        public IEnumerable<SV_MON> sV_MONs { get; set; }
        public IEnumerable<LopMon> LopMons { get; set; }


    }
}