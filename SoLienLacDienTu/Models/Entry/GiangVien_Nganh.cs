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
    
    public partial class GiangVien_Nganh
    {
        public int Stt { get; set; }
        public string MaGV { get; set; }
        public string MaNganh { get; set; }
    
        public virtual GiangVien GiangVien { get; set; }
        public virtual Nganh Nganh { get; set; }
    }
}