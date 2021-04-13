using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoLienLacDienTu.Models
{
    public class SVXemDiem
    {
       

        public Nganh NganhDetail { get; set; }
        public SinhVien SinhVienDetail { get; set; }
        public MonHoc MonHocDetail { get; set; }
        public Diem DiemDetail { get; set; }
        public DiemCT DiemCTDetail { get; set; }

        public LichThi LichThiDetail { get; set; }

        public ThoiKhoaBieu TKBDetail { get; set; }
        public ChiTietHocPhi HocPhiDetail { get; set; }
        public LopMon LopMonDetail { get; set; }
        public SinhVien_LopMon SV_LopMonDetail { get; set; }
        public Lop LopDetail { get; set; }
        public GiangVien GVDetail { get; set; }
        public GV_Mon GV_MonDetail { get; set; }
        public GV_LM GV_LMDetail { get; set; }
        public SinhVien check { get; set; }
        public SV_LOP SV_lopDetail { get; set; }
        public SinhVien_Nganh SV_nganhDetail { get; set; }
        public ThongBao TBdetail { get; set; }
        public string Ischecked { get; set; }

        public int TongTinchi { get; set; }
        public int TongTinchiHP { get; set; }
        public string error { get; set; }
        public Int32 TongHP { get; set; }
        public Int32 Tonghp { get; set; }
        public Int32 TongPTL { get; set; }
        public Int32 TongHocPhi { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? NgayBD { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? NgayKT { get; set; }
        public string Test { get; set; }

    }


    public class taomodel
    {
        public List<ThoiKhoaBieu> tkb { get; set; }
    }
    //public class CitiesViewModel
    //{
    //    public IEnumerable<string> Selectedmhoc { get; set; }
    //    public IEnumerable<MonHoc> mhoc { get; set; }
    //}
}