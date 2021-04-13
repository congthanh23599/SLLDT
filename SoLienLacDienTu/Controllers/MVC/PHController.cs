
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SoLienLacDienTu.Models;

namespace test.Controllers
{
    public class PHController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        // GET: PH
        
        public ActionResult IndexPH()
        {
            return View();
           
        }
        [HttpPost]
        public ActionResult Login(SoLienLacDienTu.Models.SinhVien PHModel)
        {
            var PHDetail = db.SinhViens.Where(x => x.MaSV == PHModel.MaSV ).FirstOrDefault();
            if (PHDetail == null)
            {
                ViewBag.mess = "không tìm thấy masv";

                return View("IndexPH", PHModel);

            }
            else
            {
                Session["MaSV"] = PHModel.MaSV;
                Session["idsv"] = PHDetail.MaSV;
                return RedirectToAction("ChucNangvaTTSV", "PH");
            }
        }
        public ActionResult LogOut()
        {

            Session.Abandon();
            return RedirectToAction("IndexPH", "PH");
        }
        [HttpGet]
        public ActionResult XemTKBcuaSV()
        {
            List<SinhVien> SinhViennames = db.SinhViens.ToList();
            List<LopMon> LopMonnames = db.LopMons.ToList();
            List<SinhVien_LopMon> SV_LopMonnames = db.SinhVien_LopMons.ToList();
            List<MonHoc> MonHocnames = db.MonHocs.ToList();
            List<ThoiKhoaBieu> TKNnames = db.ThoiKhoaBieus.ToList();
            var idsv = Session["idsv"];
            var XemTKB = (from s in SinhViennames
                          where s.MaSV == Convert.ToString(idsv)
                          join sv in SV_LopMonnames on s.MaSV equals sv.MaSV

                          join x in LopMonnames on sv.MaLM equals x.MaLM
                          where sv.MaLM == x.MaLM
                          join m in MonHocnames on x.MaMon equals m.MaMon
                          join t in TKNnames on x.MaLM equals t.MaLM
                          where x.MaLM == t.MaLM
                          select new SVXemDiem
                          {
                              SinhVienDetail = s,
                              SV_LopMonDetail = sv,
                              LopMonDetail = x,
                              MonHocDetail = m,
                              TKBDetail = t,
                              NgayBD = t.ThoiGianBD,
                              NgayKT = t.ThoiGianKT,
                          }).ToList();
            return View(XemTKB);

        }
        [HttpPost]
        public ActionResult XemTKBcuaSV(int idhk)
        {
            if (idhk != null)
            {
                Session["hkid"] = idhk;
                List<SinhVien> SinhViennames = db.SinhViens.ToList();
                List<LopMon> LopMonnames = db.LopMons.ToList();
                List<SinhVien_LopMon> SV_LopMonnames = db.SinhVien_LopMons.ToList();
                List<MonHoc> MonHocnames = db.MonHocs.ToList();
                List<ThoiKhoaBieu> TKNnames = db.ThoiKhoaBieus.ToList();
                var idsv = Session["idsv"];
                var XemTKB = (from s in SinhViennames
                              where s.MaSV == Convert.ToString(idsv)
                              join sv in SV_LopMonnames on s.MaSV equals sv.MaSV

                              join x in LopMonnames on sv.MaLM equals x.MaLM
                              where sv.MaLM == x.MaLM
                              join m in MonHocnames on x.MaMon equals m.MaMon
                              join t in TKNnames on x.MaLM equals t.MaLM
                              where x.MaLM == t.MaLM
                              select new SVXemDiem
                              {
                                  SinhVienDetail = s,
                                  SV_LopMonDetail = sv,
                                  LopMonDetail = x,
                                  MonHocDetail = m,
                                  TKBDetail = t,
                                  NgayBD = t.ThoiGianBD,
                                  NgayKT = t.ThoiGianKT,
                              }).ToList();

                return View(XemTKB);
            }
            else
            {
                return View();
            }

        }
        //public ActionResult XemTKBcuaSV(SoLienLacDienTu.Models.SVXemDiem msssv, SoLienLacDienTu.Models.SinhVien SinhVienModel)
        //{   List<SinhVien> SinhViennames = db.SinhViens.ToList();
        //    List<LopMon> LopMonnames = db.LopMons.ToList();
        //    List<SinhVien_LopMon> SV_LopMonnames = db.SinhVien_LopMons.ToList();

        //    List<MonHoc> MonHocnames = db.MonHocs.ToList();

        //    List<ThoiKhoaBieu> TKNnames = db.ThoiKhoaBieus.ToList();

        //    var idsv = Session["idsv"];
        //    var XemTKB = (from s in SinhViennames
        //                  where s.MaSV == Convert.ToString(idsv)
        //                  join sv in SV_LopMonnames on s.MaSV equals sv.MaSV
        //                  join x in LopMonnames on sv.MaLM equals x.MaLM
        //                  join m in MonHocnames on x.MaMon equals m.MaMon
        //                  join t in TKNnames on m.MaMon equals t.MaMon



        //                  select new SVXemDiem
        //                  {
        //                      SinhVienDetail = s,
        //                      LopMonDetail = x,
        //                      MonHocDetail = m,

        //                      TKBDetail = t,


        //                  }).ToList();
        //    return View(XemTKB);
        //}
        public ActionResult XemTKBTuancuaSV()
        {
            return View();
        }
        public ActionResult XemHocPhicuaSV(SoLienLacDienTu.Models.SVXemDiem msssv, SoLienLacDienTu.Models.SinhVien SinhVienModel)
        {
            var idsv = Session["idsv"];

            List<SinhVien> SinhViennames = db.SinhViens.ToList();
            List<SinhVien_LopMon> sv_lopmonnames = db.SinhVien_LopMons.ToList();
            List<LopMon> LopMonnames = db.LopMons.ToList();
            List<MonHoc> MonHocnames = db.MonHocs.ToList();
            List<ChiTietHocPhi> HocPhinames = db.ChiTietHocPhis.ToList();
            List<ThoiKhoaBieu> TKNnames = db.ThoiKhoaBieus.ToList();

            List<SinhVien_Nganh> sv_nganhnames = db.SinhVien_Nganhs.ToList();

            string msv = Convert.ToString(Session["idsv"]);
            var XemHocPhi = (from s in SinhViennames
                             where s.MaSV == Convert.ToString(idsv)
                             join sv in sv_lopmonnames on s.MaSV equals sv.MaSV
                             join x in LopMonnames on sv.MaLM equals x.MaLM
                             join m in MonHocnames on x.MaMon equals m.MaMon
                             join h in HocPhinames on x.MaMon equals h.MaMon
                             join t in TKNnames on m.MaMon equals t.MaMon
                             //join svl in sv_lopnames on s.MaSV equals svl.MaSV //
                             join svn in sv_nganhnames on s.MaSV equals svn.MaSV

                             where t.HocKy == 2/* && t.ThoiGianHoc == t.ThoiGianHoc *//*&& db.Tonghp(s.MaSV) == "1711061034"*/



                             select new SVXemDiem
                             {

                                 SinhVienDetail = s,
                                 SV_LopMonDetail = sv,
                                 LopMonDetail = x,
                                 MonHocDetail = m,
                                 HocPhiDetail = h,
                                 TKBDetail = t,
                                 //SV_lop =svl,
                                 SV_nganhDetail = svn,
                                 TongHP = 0,
                                 Tonghp = 0,
                             }).ToList();


            return View(XemHocPhi);
           

        }
        public ActionResult XemLichThicuaSV(SoLienLacDienTu.Models.SVXemDiem msssv, SoLienLacDienTu.Models.SinhVien SinhVienModel)
        {

            List<SinhVien> SinhViennames = db.SinhViens.ToList();
            List<MonHoc> MonHocnames = db.MonHocs.ToList();
            List<LichThi> LichThinames = db.LichThis.ToList();

            List<SV_LOP> sv_lopnames = db.SV_LOPs.ToList();
            List<SinhVien_Nganh> sv_nganhnames = db.SinhVien_Nganhs.ToList();

            var idsv = Session["idsv"];
            var XemLichThi = (from s in SinhViennames
                              where s.MaSV == Convert.ToString(idsv)
                              join x in LichThinames on s.MaSV equals x.MaSV

                              join svl in sv_lopnames on s.MaSV equals svl.MaSV
                              join svn in sv_nganhnames on s.MaSV equals svn.MaSV



                              select new SVXemDiem
                              {
                                  SinhVienDetail = s,
                                  LichThiDetail = x,
                                  SV_lopDetail = svl,
                                  SV_nganhDetail = svn,

                              }).ToList();
            return View(XemLichThi);
        }
        public ActionResult XemDiemcuaSV(SoLienLacDienTu.Models.SVXemDiem msssv, SoLienLacDienTu.Models.SinhVien SinhVienModel)
        {
            List<SinhVien> SinhViennames = db.SinhViens.ToList();
            List<MonHoc> MonHocnames = db.MonHocs.ToList();
            List<Diem> Diemnames = db.Diems.ToList();
            List<DiemCT> DiemCTnames = db.DiemCTs.ToList();


            List<SinhVien_Nganh> sv_nganhnames = db.SinhVien_Nganhs.ToList();
            List<Nganh> nganhnames = db.Nganhs.ToList();


            ////var Multipletable = from sv in SinhViennames

            ////                    join ct in DiemCTnames on sv.MaSV equals ct.MaSV into table1
            ////                    from ct in table1.DefaultIfEmpty()


            ////                    select new SVXemDiem { DiemCTDetail = ct, SinhVienDetail = sv};

            var idsv = Session["idsv"];
            var XemDiem = (from s in SinhViennames
                           where s.MaSV == Convert.ToString(idsv)
                           join x in Diemnames on s.MaSV equals x.MaSV

                           join y in DiemCTnames on x.IDDiem equals y.IDDiem

                           join m in MonHocnames on y.MaMon equals m.MaMon

                           join svn in sv_nganhnames on s.MaSV equals svn.MaSV
                           join nm in nganhnames on svn.MaNganh equals nm.MaNganh
                           select new SVXemDiem
                           {

                               SinhVienDetail = s,
                               DiemCTDetail = y,
                               DiemDetail = x,
                               MonHocDetail = m,

                               SV_nganhDetail = svn,
                               NganhDetail = nm,
                           }).ToList();





            return View(XemDiem);
        }
        public ActionResult ChucNangvaTTSV(SoLienLacDienTu.Models.SVXemDiem msssv, SoLienLacDienTu.Models.SinhVien SinhVienModel)
        {
            List<SinhVien> SinhViennames = db.SinhViens.ToList();
            List<SV_LOP> Sv_lopnames = db.SV_LOPs.ToList();
            List<SinhVien_Nganh> Sv_Nganhnames = db.SinhVien_Nganhs.ToList();




            ////var Multipletable = from sv in SinhViennames

            ////                    join ct in DiemCTnames on sv.MaSV equals ct.MaSV into table1
            ////                    from ct in table1.DefaultIfEmpty()


            ////                    select new SVXemDiem { DiemCTDetail = ct, SinhVienDetail = sv};

            var idsv = Session["idsv"];
            var TTSV = (from s in SinhViennames
                           where s.MaSV == Convert.ToString(idsv)
                        join x in Sv_lopnames on s.MaSV equals x.MaSV
                        join y in Sv_Nganhnames on s.MaSV equals y.MaSV
                       where s.MaSV==x.MaSV && s.MaSV==y.MaSV
                        select new SVXemDiem
                           {
                               SinhVienDetail = s,
                            SV_lopDetail = x,
                            SV_nganhDetail = y,
                             


                           }).ToList();



            return View(TTSV);
        }
    }
}