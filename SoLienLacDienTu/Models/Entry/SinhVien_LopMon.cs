//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SoLienLacDienTu.Models.Entry
{
    using System;
    using System.Collections.Generic;
    
    public partial class SinhVien_LopMon
    {
        public int ST { get; set; }
        public string MaSV { get; set; }
        public string MaLM { get; set; }
    
        public virtual LopMon LopMon { get; set; }
        public virtual SinhVien SinhVien { get; set; }
    }
}
