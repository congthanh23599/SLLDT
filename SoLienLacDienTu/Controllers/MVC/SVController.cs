using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SoLienLacDienTu.Models;
using System.IO;
using SoLienLacDienTu.Helper;


namespace test.Controllers
{
    public class SVController : Controller
    {
        int a;
        public string o;
        public string p;
        string m;
        string mdc;
        //string xtq;
        public string[] ids;
        public string[] montq;
        DataClasses1DataContext db = new DataClasses1DataContext();

        public ActionResult IndexSV()
        {
            ViewBag.name = Session["idsv"];

            return View();
        }
        [HttpGet]
        public ActionResult XemTKB()
        {
            var tkuser = Convert.ToString(Session["idsv"]);
            if (string.IsNullOrWhiteSpace(tkuser))
            {
                return RedirectToAction("DangNhap");
            }
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
        public ActionResult XemTKB(int idhk)
        {
            var tkuser = Convert.ToString(Session["idsv"]);
            if (string.IsNullOrWhiteSpace(tkuser))
            {
                return RedirectToAction("DangNhap");
            }
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

        public ActionResult dangnhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult dangnhap(FormCollection collection)
        {

            var tendn = collection["username"];
            var matkhau = collection["pass"];
            if (string.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Vui Lòng Nhập Tên Đăng Nhập !";
            }
            else
            if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Vui Lòng Nhập Mật Khẩu !";
            }
            else
            {
                SinhVien ad = db.SinhViens.SingleOrDefault(n => n.MaSV == tendn && n.Password == matkhau);
                if (ad != null)
                {
                    Session["idsv"] = ad.MaSV;
                    return RedirectToAction("XemTKB", "SV");
                }
                else
                    ViewBag.ThongBao = "Tên Đăng Nhập Hoặc Mật Khẩu Không Đúng !";
            }
            return View();
        }
        /* public ActionResult Login(SoLienLacDienTu.Models.SinhVien SinhVienModel, SoLienLacDienTu.Models.SVXemDiem msssv)
         {
           *//*  var tendn = collection["username"];
             var matkhau = collection["pass"];
             if (string.IsNullOrEmpty(tendn))
             {
                 ViewData["Loi1"] = "Vui Lòng Nhập Tên Đăng Nhập !";
             }
             else
                 if (string.IsNullOrEmpty(matkhau))
             {
                 ViewData["Loi2"] = "Vui Lòng Nhập Mật Khẩu !";
             }
             else
             {
                 Admin ad = db.Admins.SingleOrDefault(n => n.TaiKhoan == tendn && n.Password == matkhau);
                 if (ad != null)
                 {
                     Session["TKadmin"] = ad.TenAdmin;
                     return RedirectToAction("Index", "Homeadmin");
                 }
                 else
                     ViewBag.ThongBao = "Tên Đăng Nhập Hoặc Mật Khẩu Không Đúng !";
             }
             return View();*//*

             var SinhvienDetail = db.SinhViens.Where(x => x.MaSV == SinhVienModel.MaSV && x.Password == SinhVienModel.Password).FirstOrDefault();
             if (SinhvienDetail == null)
             {
                 ViewBag.mess = "không đăng nhập dc";

                 return View("IndexSV", SinhVienModel);

             }
             else
             {
                 ViewBag.name = SinhvienDetail.TenSV;
                 Session["MaSV"] = SinhVienModel.MaSV;
                 Session["idsv"] = SinhvienDetail.MaSV;
                 return RedirectToAction("XemTKB", "SV");
             }*//*
         }*/
        /* public ActionResult LogOut()
         {

             Session.Abandon();
             return RedirectToAction("IndexSV", "SV");
         }*/
        // xử lý sự kiện bấm vào học kỳ
        //[HttpPost]
        //public ActionResult HocKyTKBTuanSV(int idhk)
        //{
        //    if (idhk != null)
        //    {
        //        Session["idhk"] = idhk;
        //        return RedirectToAction("TKBTuanSV", "SV");
        //    }
        //    else
        //    {
        //        return View("TKBTuanSV");
        //    }
        //}
        public ActionResult DangXuat()
        {
            var tkdangxuat = Convert.ToString(Session["idsv"]);
            if (string.IsNullOrWhiteSpace(tkdangxuat))
            {

                return RedirectToAction("DangNhap");
            }
            else
            {
                Session["idsv"] = "";

                return RedirectToAction("DangNhap");
            }
        }
        [HttpGet]
        public ActionResult TKBTuanSV(SoLienLacDienTu.Models.SVXemDiem msssv, SoLienLacDienTu.Models.SinhVien SinhVienModel)
        {
            var tkuser = Convert.ToString(Session["idsv"]);
            if (string.IsNullOrWhiteSpace(tkuser))
            {
                return RedirectToAction("DangNhap");
            }
            List<SinhVien> SinhViennames = db.SinhViens.ToList();
            List<LopMon> LopMonnames = db.LopMons.ToList();
            List<MonHoc> MonHocnames = db.MonHocs.ToList();
            List<SinhVien_LopMon> SVLM = db.SinhVien_LopMons.ToList();
            List<ThoiKhoaBieu> TKNnames = db.ThoiKhoaBieus.ToList();

            var idsv = Session["idsv"];

            var XemTKB = (from s in SinhViennames
                          where s.MaSV == Convert.ToString(idsv)
                          join z in SVLM on s.MaSV equals z.MaSV
                          join x in LopMonnames on z.MaLM equals x.MaLM
                          join m in MonHocnames on x.MaMon equals m.MaMon
                          join t in TKNnames on m.MaMon equals t.MaMon
                          where x.MaLM == t.MaLM && m.MaMon == x.MaMon
                          //where t.

                          select new SVXemDiem
                          {
                              SinhVienDetail = s,
                              LopMonDetail = x,
                              MonHocDetail = m,

                              TKBDetail = t,


                          }).ToList();
            //var PHDetail = db.SinhViens.Where(x => x.MaSV == PHModel.MaSV).FirstOrDefault();
            //if (PHDetail == null)
            //{

            //    return View("IndexPH", );

            //}
            //else
            //{
            //    Session["MaSV"] = PHModel.MaSV;
            //    Session["idsv"] = PHDetail.MaSV;
            //    return RedirectToAction("ChucNangvaTTSV", "PH");
            //}
            return View(XemTKB);//XemTKB
        }
        [HttpPost]
        public ActionResult TKBTuanSV(int ids, int idhk)
        {
            //int value = ids;
            List<SinhVien> SinhViennames = db.SinhViens.ToList();
            List<LopMon> LopMonnames = db.LopMons.ToList();
            List<MonHoc> MonHocnames = db.MonHocs.ToList();
            List<SinhVien_LopMon> SVLM = db.SinhVien_LopMons.ToList();
            List<ThoiKhoaBieu> TKNnames = db.ThoiKhoaBieus.ToList();

            var idsv = Session["idsv"];

            var XemTKB = (from s in SinhViennames
                          where s.MaSV == Convert.ToString(idsv)
                          join z in SVLM on s.MaSV equals z.MaSV
                          join x in LopMonnames on z.MaLM equals x.MaLM
                          join m in MonHocnames on x.MaMon equals m.MaMon
                          join t in TKNnames on m.MaMon equals t.MaMon
                          where x.MaLM == t.MaLM && m.MaMon == x.MaMon
                          //where t.

                          select new SVXemDiem
                          {
                              SinhVienDetail = s,
                              LopMonDetail = x,
                              MonHocDetail = m,

                              TKBDetail = t,


                          }).ToList();
            if (ids != null || idhk != null)
            {
                Session["i"] = ids;
                Session["idhk"] = idhk;
                return View(XemTKB);
            }
            else
            {
                return View();
            }
        }

        public ActionResult XemDiem(SoLienLacDienTu.Models.SVXemDiem msssv, SoLienLacDienTu.Models.SinhVien SinhVienModel)
        {
            var tkuser = Convert.ToString(Session["idsv"]);
            if (string.IsNullOrWhiteSpace(tkuser))
            {
                return RedirectToAction("DangNhap");
            }
            List<SinhVien> SinhViennames = db.SinhViens.ToList();
            List<MonHoc> MonHocnames = db.MonHocs.ToList();
            List<Diem> Diemnames = db.Diems.ToList();
            List<DiemCT> DiemCTnames = db.DiemCTs.ToList();


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
                           select new SVXemDiem
                           {

                               SinhVienDetail = s,
                               DiemCTDetail = y,
                               DiemDetail = x,
                               MonHocDetail = m,
                           }).ToList();
            return View(XemDiem);

        }

        public ActionResult XemLichThi(SoLienLacDienTu.Models.SVXemDiem msssv, SoLienLacDienTu.Models.SinhVien SinhVienModel)
        {
            var tkuser = Convert.ToString(Session["idsv"]);
            if (string.IsNullOrWhiteSpace(tkuser))
            {
                return RedirectToAction("DangNhap");
            }
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
        public ActionResult XemHocPhi(SoLienLacDienTu.Models.SVXemDiem msssv, SoLienLacDienTu.Models.SinhVien SinhVienModel)
        {
            var tkuser = Convert.ToString(Session["idsv"]);
            if (string.IsNullOrWhiteSpace(tkuser))
            {
                return RedirectToAction("DangNhap");
            }
            var idsv = Session["idsv"];

            List<SinhVien> SinhViennames = db.SinhViens.ToList();
            List<SinhVien_LopMon> sv_lopmonnames = db.SinhVien_LopMons.ToList();
            List<LopMon> LopMonnames = db.LopMons.ToList();
            List<MonHoc> MonHocnames = db.MonHocs.ToList();
            List<ChiTietHocPhi> HocPhinames = db.ChiTietHocPhis.ToList();
            List<ThoiKhoaBieu> TKNnames = db.ThoiKhoaBieus.ToList();

            List<SinhVien_Nganh> sv_nganhnames = db.SinhVien_Nganhs.ToList();
            //SqlCommand cmd = new SqlCommand();

            string msv = Convert.ToString(Session["idsv"]);
            //var tohp = (cmd.CommandText = "Tonghp",
            //cmd.Parameters.Add(new SqlParameter("@MaSV", msv)));
            //var thp = db.Tonghp(msv);
            //string tp = Convert.ToString(thp);
            int hocky = 0;
            foreach (var i in TKNnames)
            {
                foreach (var g in sv_lopmonnames)
                    if (g.MaSV == Convert.ToString(idsv))
                    {
                        for (int j = 0; j < TKNnames.Count(); j++)
                        {
                            if (g.MaLM == i.MaLM)
                            {
                                if (i.Nam == DateTime.Now.Year)
                                {
                                    if (i.HocKy > hocky)
                                    {
                                        hocky = i.HocKy;
                                    }
                                }

                            }

                        }
                    }

            }

            var XemHocPhi = (from s in SinhViennames
                             where s.MaSV == Convert.ToString(idsv)
                             join sv in sv_lopmonnames on s.MaSV equals sv.MaSV
                             join x in LopMonnames on sv.MaLM equals x.MaLM
                             join m in MonHocnames on x.MaMon equals m.MaMon
                             join h in HocPhinames on x.MaMon equals h.MaMon
                             join t in TKNnames on m.MaMon equals t.MaMon
                             //join svl in sv_lopnames on s.MaSV equals svl.MaSV //
                             join svn in sv_nganhnames on s.MaSV equals svn.MaSV
                             where t.HocKy == hocky  /*&& t.ThoiGianHoc == t.ThoiGianHoc *//*&& db.Tonghp(s.MaSV) == "1711061034"*/
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
        public ActionResult XemTB(SoLienLacDienTu.Models.SVXemDiem msssv, SoLienLacDienTu.Models.SinhVien SinhVienModel)
        {
            var tkuser = Convert.ToString(Session["idsv"]);
            if (string.IsNullOrWhiteSpace(tkuser))
            {
                return RedirectToAction("DangNhap");
            }
            List<SinhVien> SinhViennames = db.SinhViens.ToList();
            List<Lop> Lopnames = db.Lops.ToList();
            List<SV_LOP> SV_lopnames = db.SV_LOPs.ToList();
            List<GV_Mon> gV_Mons = db.GV_Mons.ToList();

            List<LopMon> LopMonnames = db.LopMons.ToList();
            List<SinhVien_LopMon> SV_LopMonnames = db.SinhVien_LopMons.ToList();
            List<ThongBao> TBnames = db.ThongBaos.ToList();
            List<GiangVien> GVnames = db.GiangViens.ToList();

            var idsv = Session["idsv"];

            // Danh sách các LM sinh viên thuộc
            var DSSVLM = (from svlm in SV_LopMonnames
                          where svlm.MaSV == Convert.ToString(idsv)
                          select new SinhVien_LopMon { MaLM = svlm.MaLM, MaSV = svlm.MaSV }).ToList();

            // Danh sách các Lop sinh viên thuộc
            var DSSVL = (from svl in SV_lopnames
                          where svl.MaSV == Convert.ToString(idsv)
                          select new SV_LOP {Malop = svl.Malop, MaSV = svl.MaSV,  }).ToList();

            // thong bao
            var xemdstb = (from svlm in DSSVLM
                           from svl in DSSVL
                           from ktlm in TBnames 
                           where ktlm.MaLM == svlm.MaLM || ktlm.MaLop == svl.Malop || ktlm.MaSV == svlm.MaSV
                           select new ThongBao {
                            MaGV = ktlm.MaGV,
                            MaLop = ktlm.MaLop,
                            MaLM = ktlm.MaLM,
                            NgayDang = ktlm.NgayDang,
                            NoiDung = ktlm.NoiDung
                           }
                           ).ToList();

            var XemTB = (from a in GVnames 
                         where a.MaGV== "17896750"
                         join m in TBnames on  a.MaGV equals m.MaGV  
                         join n in SV_lopnames on m.MaSV equals n.MaSV
                         join o in SV_LopMonnames on m.MaSV equals o.MaSV
                         where m.MaSV ==Convert.ToString(idsv)  || m.MaLop ==n.Malop || m.MaLM== o.MaLM

                         select new SVXemDiem
                         {
                             //SV_lopDetail = m,
                             ////LopDetail = x,
                             //SV_LopMonDetail = n,
                             //LopMonDetail = y,
                             TBdetail = m,
                             GVDetail = a,



                         }).ToList();
            foreach (var i in XemTB)
            {
                int a = 0;
            }
            return View(xemdstb);


          
        }
        public LopMon GetMon(string malm)
        {
            return db.LopMons.Where(n => n.MaLM == malm).FirstOrDefault();
        }
        public ActionResult DKMonHoc()
        {
            var tkuser = Convert.ToString(Session["idsv"]);
            if (string.IsNullOrWhiteSpace(tkuser))
            {
                return RedirectToAction("DangNhap");
            }
            List<SinhVien> SinhViennames = db.SinhViens.ToList();
            List<LopMon> LopMonnames = db.LopMons.ToList();
            List<MonHoc> MonHocnames = db.MonHocs.ToList();
            List<SinhVien_LopMon> SV_Lopmonnames = db.SinhVien_LopMons.ToList();
            List<SV_MON> SV_Monnames = db.SV_MONs.ToList();
            List<ThoiKhoaBieu> TKNnames = db.ThoiKhoaBieus.ToList();

            var idsv = Session["idsv"];
            // load danh sách môn học
            var DKmon = (from sv in SinhViennames
                         where sv.MaSV == Convert.ToString(idsv)

                         join svm in SV_Monnames on sv.MaSV equals svm.MaSV
                         join m in LopMonnames on svm.MaMon equals m.MaMon
                         join l in MonHocnames on m.MaMon equals l.MaMon
                         join t in TKNnames on m.MaMon equals t.MaMon
                         join sv_lm in SV_Lopmonnames on t.MaLM equals sv_lm.MaLM
                         where sv_lm.MaLM == m.MaLM && m.MaLM == t.MaLM && svm.MaMon == l.MaMon && svm.DaHoc == false
                         select new DkMon
                         {
                             MaMon = l.MaMon,
                             TenMon = l.TenMon,
                             SoTinChi = l.Sotinchi,
                             ST = t.ST,
                             HocKy = t.HocKy,
                             TH = t.TH,
                             ThoiGianBD = t.ThoiGianBD,
                             ThoiGianKT = t.ThoiGianKT,
                             TietBD = t.TietBD,
                             MaGV = t.MaGV,
                             MaLM = m.MaLM,
                             Phong = t.Phong,
                             Montienquyet = l.MaMonTienQuyet
                         }).ToList();
            /*foreach (var e in DKmon)
            {
                var updatedHobby = e..ToList().Find(p => p.HobbyId == hobby.HobbyId);
                updatedHobby.IsSelected = hobby.IsSelected;
            }

            entities.SaveChanges();*/
            var idmm = Session["mamon"];
            return View(DKmon);
        }
        /*public List<SV_MON> GetSV_MONs() { List<SV_MON> sv_mon = new List<SV_MON>(); return sv_mon; }
        public List<LopMon> GetLopMons() { List<LopMon> lopmon = new List<LopMon>(); return lopmon; }
       */
        [HttpPost]
        public ActionResult DKMonHoc(string ids, string montq, string mondachon, FormCollection collection)
        {
            List<SinhVien> SinhViennames = db.SinhViens.ToList();
            List<LopMon> LopMonnames = db.LopMons.ToList();
            List<MonHoc> MonHocnames = db.MonHocs.ToList();
            List<SinhVien_LopMon> SV_Lopmonnames = db.SinhVien_LopMons.ToList();
            List<SV_MON> SV_Monnames = db.SV_MONs.ToList();
            List<ThoiKhoaBieu> TKNnames = db.ThoiKhoaBieus.ToList();


            var idsv = Session["idsv"];
            int demm = 0;
            int demm1 = 0; int demm2 = 0;
            /*  var mmtq = Session["montq"];*/
            if (ids != null)
            {
                ViewBag.Message = " ban co id sinh vien la:" + string.Join(",", ids);
                /*var masv = collection["ids"];*/

                /* for (; demm1 < montq.Count(); demm1++)
                 {

                 }*/

                Session["Mamon"] = ids;
                a = demm;
                Session["a"] = a;

                m = montq;
                mdc = mondachon;
                p = ids;
                Session["mondachon"] = mdc;
                Session["montq"] = m;
                Session["b"] = p;
                foreach (var e in LopMonnames)
                {

                    if (e.MaLM != Convert.ToString(ids))
                    {

                        foreach (var y in SV_Monnames)
                        {
                            if (e.MaMon != y.MaMon)
                            {
                                Session["q"] = y.MaMonTienquyet;


                            }
                            else
                            {
                                Session["q"] = "Erro1";
                            }
                        }
                    }
                    else
                    {
                        Session["q"] = "Erro";
                    }

                }
                return RedirectToAction("tableview");
            }
            else
            {
                ViewBag.Message = "no comment";
                return View("IndexSV");
            }
        }
        [HttpGet]
        public ActionResult tableview()//ids la masv cua webgrid
        {
            var tkuser = Convert.ToString(Session["idsv"]);
            if (string.IsNullOrWhiteSpace(tkuser))
            {
                return RedirectToAction("DangNhap");
            }
            var idlm = Session["b"];
            var idsv = Session["idsv"];
            string mmtq = Convert.ToString(Session["montq"]);
            var mdchon = Session["mondachon"];
            List<SinhVien> SinhViennames = db.SinhViens.ToList();
            List<LopMon> LopMonnames = db.LopMons.ToList();
            List<MonHoc> MonHocnames = db.MonHocs.ToList();
            List<SinhVien_LopMon> SV_Lopmonnames = db.SinhVien_LopMons.ToList();
            List<ThoiKhoaBieu> TKNnames = db.ThoiKhoaBieus.ToList();
            List<SV_MON> SV_Monnames = db.SV_MONs.ToList();
            string lmid = (string)Session["b"];
            ViewBag.Message = "Welcome to my demo!";
            /* ViewData["svmon"] = GetSV_MONs();
             ViewData["lopmon"] = GetLopMons();*/
            // load danh sách môn học đẫ chọn  bằng idlm
            var DKmon = (

                         from lm in LopMonnames
                         where lm.MaLM == GetMon(lmid).MaLM
                         join mh in MonHocnames on lm.MaMon equals mh.MaMon
                         join tkb in TKNnames on lm.MaLM equals tkb.MaLM
                         from sv in SinhViennames
                         where sv.MaSV == Convert.ToString(idsv)
                         join svm in SV_Monnames on sv.MaSV equals svm.MaSV
                         where svm.MaMon == mh.MaMon && svm.DaHoc == false
                         select new DkMon
                         {
                             MaMon = lm.MaMon,
                             TenMon = tkb.TenMon,
                             SoTinChi = mh.Sotinchi,
                             ST = tkb.ST,
                             HocKy = tkb.HocKy,
                             TH = tkb.TH,
                             ThoiGianBD = tkb.ThoiGianBD,
                             ThoiGianKT = tkb.ThoiGianKT,
                             TietBD = tkb.TietBD,
                             MaGV = tkb.MaGV,
                             MaLM = tkb.MaLM,
                             Phong = tkb.Phong,
                             Montienquyet = svm.MaMonTienquyet
                         }).ToList();



            foreach (var i in SV_Monnames)
            {
                if (i.MaSV == Convert.ToString(ids))
                {
                    if (i.MaMonTienquyet == Convert.ToString(mmtq))
                    {

                        if (i.DaHoc == false)
                        {
                            ViewBag.mess = "chua hoc";
                            TempData["AlertMessage"] = "chua hoc";
                            TempData["AlertType"] = "alert-warning";

                        }
                        else
                        {

                            ViewBag.mess = "da hoc";
                            TempData["AlertMessage"] = "da hoc";

                            TempData["AlertType"] = "alert-success";



                        }
                    }
                    else
                    {
                        ViewBag.mess = "khong co mon hoc";
                        TempData["AlertMessage"] = "khong co mon hoc";
                        TempData["AlertType"] = "alert-info";

                    }
                }

            }

            /*foreach (var i in SV_Monnames)
            {
                if (i.MaSV == Convert.ToString(idsv))
                {
                    if (i.MaMon == mmtq)
                    {
                        if (i.DaHoc == true)
                        {
                            for(int j=0;j<SV_Monnames.Count();j++)
                            { 
                            }
                            if (i.MaMon == Convert.ToString(mdchon))
                            {
                                if (i.DaHoc == false)
                                {
                                    i.DaHoc = true;
                                    ViewBag.chuahoc = "dang ky thanh cong";
                                }
                            }
                            else
                            {
                                ViewBag.chuahoc = "chua dat du dieu kien 4";
                                return View(DKmon);
                            }

                        }
                        else
                        {
                            ViewBag.chuahoc = "chua dat du dieu kien 3";
                            return View(DKmon);
                        }
                    }
                    else
                    {
                        ViewBag.chuahoc = "chua dat du dieu kien 2";
                        return View(DKmon);
                    }
                }
                else
                {
                    ViewBag.chuahoc = "chua dat du dieu kien 1";
                    return View(DKmon);
                }
            }*/


            return View(DKmon);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult tableview(SinhVien_LopMon sp, SV_MON svm, SoLienLacDienTu.Models.DkMon dkm, string mondachon)
        {
            var idsv = Session["idsv"];
            var o = Session["b"];
            var mamon = Session["Mamon"];
            string mmtq = Convert.ToString(Session["montq"]);

            List<SinhVien> SinhViennames = db.SinhViens.ToList();
            List<SinhVien_LopMon> SV_Lopmonnames = db.SinhVien_LopMons.ToList();
            List<LopMon> LopMonnames = db.LopMons.ToList();
            List<MonHoc> MonHocnames = db.MonHocs.ToList();
            List<SV_MON> SV_Monnames = db.SV_MONs.ToList();
            List<ThoiKhoaBieu> TKNnames = db.ThoiKhoaBieus.ToList();
            var DKmon = (
                     from m in LopMonnames
                     where m.MaLM == Convert.ToString(o)
                     join l in MonHocnames on m.MaMon equals l.MaMon
                     join t in TKNnames on m.MaMon equals t.MaMon
                     join sv_lm in SV_Lopmonnames on t.MaLM equals sv_lm.MaLM
                     where m.MaMon == t.MaMon && m.MaMon == l.MaMon && sv_lm.MaLM == m.MaLM
                     select new DkMon
                     {
                         MaMon = l.MaMon,
                         TenMon = l.TenMon,
                         SoTinChi = l.Sotinchi,
                         ST = t.ST,
                         HocKy = t.HocKy,
                         TH = t.TH,
                         ThoiGianBD = t.ThoiGianBD,
                         ThoiGianKT = t.ThoiGianKT,
                         TietBD = t.TietBD,
                         MaGV = t.MaGV,
                         MaLM = m.MaLM,
                         Phong = t.Phong,
                         Montienquyet = l.MaMonTienQuyet
                     }).ToList();
            if (ModelState.IsValid)
            {

                sp.MaLM = Convert.ToString(o);// malm
                sp.MaSV = Convert.ToString(idsv);// ma sinh vien 
                var mondc = (

                        from q in SV_Monnames
                        where q.MaSV == Convert.ToString(idsv) && q.MaMon == Convert.ToString(mondachon)

                        select new mondc
                        {

                            MaSV = q.MaSV,
                            MaMon = q.MaMon,
                            DaHoc = q.DaHoc,
                            MaMonTienquyet = q.MaMonTienquyet,

                        }).ToList();
                var xetDK = (

                        from q in SV_Monnames
                        where q.MaSV == Convert.ToString(idsv) && q.MaMon == Convert.ToString(mmtq)

                        select new xettq
                        {

                            MaSV = q.MaSV,
                            MaMon = q.MaMon,
                            DaHoc = q.DaHoc,
                            MaMonTienquyet = q.MaMonTienquyet,

                        }).ToList();
                foreach (var a in db.LopMons.Where(x => x.MaLM == Convert.ToString(mamon)))
                {
                    foreach (var b in db.SV_MONs.Where(x => x.MaSV == Convert.ToString(idsv) && x.MaMon == a.MaMon))
                    {
                        foreach (var c in db.SV_MONs.Where(x => x.MaSV == Convert.ToString(idsv) && x.MaMonTienquyet == b.MaMonTienquyet))
                        {
                            if (c.DaHoc == true)
                            {

                                b.DaHoc = true;

                                ViewBag.chuahoc = "dang ky thanh cong";
                                db.SinhVien_LopMons.InsertOnSubmit(sp);
                                db.SubmitChanges();
                            }
                            else
                            {
                                ViewBag.chuahoc = "chua dat du dieu kien 1";

                            }

                        }
                    }
                }



                return RedirectToAction("XemTKB");

            }




            else // lam vay cho de biet 
            {
                return View("IndexSV");
            }
        }
        public ActionResult Hotro()
        {
            var tkuser = Convert.ToString(Session["idsv"]);
            if (string.IsNullOrWhiteSpace(tkuser))
            {
                return RedirectToAction("DangNhap");
            }
            return View();
        }

        public ActionResult DangkyLop()
        {
            var tkuser = Convert.ToString(Session["idsv"]);
            if (string.IsNullOrWhiteSpace(tkuser))
            {
                return RedirectToAction("DangNhap");
            }
            return View();
        }
        public ActionResult DangKyXBD()
        {
            var tkuser = Convert.ToString(Session["idsv"]);
            if (string.IsNullOrWhiteSpace(tkuser))
            {
                return RedirectToAction("DangNhap");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult DangKyXBD(HttpPostedFileBase file)
        {

            var tkuser = Convert.ToString(Session["idsv"]);
            if (string.IsNullOrWhiteSpace(tkuser))
            {
                return RedirectToAction("DangNhap");
            }
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    file.SaveAs(_path);
                }
                ViewBag.Message = "File Uploaded Successfully!!";
                return View();
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }


        public ActionResult Uploadfile()
        {
            var tkuser = Convert.ToString(Session["idsv"]);
            if (string.IsNullOrWhiteSpace(tkuser))
            {
                return RedirectToAction("DangNhap");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Uploadfile([Bind(Include = "MaSV, TenSV, LiDo, FileDon,IDLD")] Don dk, HttpPostedFileBase file, string idLD)
        {
            var tkuser = Convert.ToString(Session["idsv"]);
            if (string.IsNullOrWhiteSpace(tkuser))
            {
                return RedirectToAction("DangNhap");
            }
            var path = "";
            var filename = "";
            var idsv = Convert.ToString(Session["idsv"]);
            List<SinhVien> SinhViennames = db.SinhViens.ToList();
            var getTenSV = (from s in SinhViennames
                            where s.MaSV == Convert.ToString(idsv)
                            select s.TenSV).FirstOrDefault();

            dk.MaSV = idsv;
            dk.TenSV = getTenSV;
            dk.NgayDang = DateTime.Now;
            dk.TrangThai = 0;

            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    filename = file.FileName;
                    path = Path.Combine(Server.MapPath("~/UploadedFiles"), filename);
                    file.SaveAs(path);
                    dk.FileDon = filename;
                    dk.IDLD = idLD;
                }
                else
                {
                    dk.FileDon = null;
                }
                db.Dons.InsertOnSubmit(dk);
                db.SubmitChanges();
                ViewBag.Message = "File upload failed!!";
                return RedirectToAction("Hotro");

            }
            ViewBag.Message = "File upload succ!!";
            return View(dk);
        }

        public ActionResult DangKyMon()
        {
            var tkuser = Convert.ToString(Session["idsv"]);
            if (string.IsNullOrWhiteSpace(tkuser))
            {
                return RedirectToAction("DangNhap");
            }
            return View(db.ThoiKhoaBieus.ToList());
        }

        [HttpPost]
        public ActionResult save(List<ThoiKhoaBieu> hobbies)
        {
            var idsv = Session["idsv"]; // luu session idsv luc log in vao

            List<SV_MON> svmnames = db.SV_MONs.ToList();
            //DS cần thiết để kiểm tra SV DA HOC
            List<SinhVien_LopMon> svlmnames = db.SinhVien_LopMons.ToList();
            List<MonHoc> mhnames = db.MonHocs.ToList();

            List<ThoiKhoaBieu> tkbnames = db.ThoiKhoaBieus.ToList();


            var ktDahoc = (from svm in svmnames
                           where svm.MaSV == Convert.ToString(idsv) && svm.DaHoc == true
                           join tkb in tkbnames on svm.MaMon equals tkb.MaMon
                           join mh in mhnames on tkb.MaMon equals mh.MaMon
                           select new ThoiKhoaBieu
                           {
                               MaMon = mh.MaMonTienQuyet
                           }).ToList();


            foreach (ThoiKhoaBieu tkb in hobbies)
            {
                ThoiKhoaBieu updatetkb = db.ThoiKhoaBieus.ToList().Find(p => p.IDTKB == tkb.IDTKB);

                updatetkb.IsSelected = tkb.IsSelected;
                if (updatetkb.IsSelected != null)
                {
                    foreach (SV_MON svm in svmnames)
                    {
                        if (svm.MaSV == Convert.ToString(idsv) && svm.MaMon == updatetkb.MaMon && svm.DaHoc == false)
                        {
                            svm.DaHoc = true;// ông bấm save bên dkm phải ko thì nó nèvlin ^^
                        }
                        else
                        {
                            ViewBag.test = "dang ky that bai";
                        }
                    }
                }
                else
                {
                    ViewBag.test = "ko ton tai";
                }
                /*
                Session["Luuchecked"] = updatetkb.MaLM;*/
            }
            // vậy thằng này thì sao nó lấy đc cái lúc check á

            //code này để làm j z ông
            // nó tìm thằng nào có idTKB á r nó lấy cái isselected gán á
            //nó lưu vô đâu z ông lưu tkb á chỉ có lưu cái check thôi cái  isselected á
            /*  db.s();*/
            db.SubmitChanges();

            return RedirectToAction("DangKyMon");
        }
        [HttpPost]
        public ActionResult LuuData(List<ThoiKhoaBieu> hobbies, SinhVien_LopMon sp, IEnumerable<SinhVien_LopMon> sp1)
        {
            var idsv = Session["idsv"]; // luu session idsv luc log in vao
            List<SV_MON> svmnames = db.SV_MONs.ToList();
            List<MonHoc> mhnames = db.MonHocs.ToList();
            List<ThoiKhoaBieu> tkbnames = db.ThoiKhoaBieus.ToList();
            List<LopMon> lopMons = db.LopMons.ToList();

            List<SinhVien_LopMon> svlmnames = db.SinhVien_LopMons.ToList();
            List<MonHoc> monHocs = db.MonHocs.ToList();
            var ktDahoc = (from svm in svmnames
                           where svm.MaSV == Convert.ToString(idsv) && svm.DaHoc == true
                           join tkb in tkbnames on svm.MaMon equals tkb.MaMon
                           join mh in mhnames on tkb.MaMon equals mh.MaMon
                           select new ThoiKhoaBieu
                           {
                               MaMon = mh.MaMon

                           }).ToList();
            var updatesvlm = db.SinhVien_LopMons.ToList();
            int dem = 0;
            int tinchi = 0;



            updatesvlm = (from tkbdh in tkbnames
                          where tkbdh.IsSelected == true// ra dc thằng đã chọn
                          join svmdahoc in svmnames on tkbdh.MaMon equals svmdahoc.MaMon//đổ ra bảng svm với nhưng oj dc chọn
                          where svmdahoc.MaSV == Convert.ToString(idsv)//lấy ra oj có masv là mã user
                          join svmtq in svmnames on svmdahoc.MaMonTienquyet equals svmtq.MaMon//lấy ra môn tq với mamontq là mamon da chon
                          where svmtq.DaHoc == true//lấy ra với đahoc=true
                          select new SinhVien_LopMon
                          {
                              MaSV = Convert.ToString(idsv),
                              MaLM = tkbdh.MaLM

                          }).ToList();
            foreach (var a in updatesvlm)//lấy ra các đối tượng đủ DK
            {
                foreach (var b in lopMons)
                {
                    if (a.MaLM == b.MaLM)//so sánh với lớp môn để giống qua lớp môn
                    {

                        for (int i = 0; i < monHocs.Count(); i++)
                        {
                            if (monHocs[i].MaMon == b.MaMon)
                            {
                                tinchi = tinchi + Convert.ToInt32(monHocs[i].Sotinchi);
                            }
                        }

                    }
                }
            }

            foreach (var trung1 in updatesvlm)
            {

                ViewBag.trung1 = trung1.MaLM;

            }

            var test = (from tr in svlmnames
                        select new trungmon
                        {
                            MaLM = tr.MaLM
                        }).ToList();
            foreach (var trung2 in test)
            {

                ViewBag.trung2 = trung2.MaLM;

            }

            foreach (var item in updatesvlm)
            {
                for (int i = 0; i < svlmnames.Count(); i++)
                {
                    if (svlmnames[i].MaLM == item.MaLM)
                    {
                        dem = dem + 1;
                    }
                }
            }

            Session["dem"] = dem;
            if (dem > 64)
            {
                Session["loi"] = "123";
            }
            else if (tinchi > 3)
            {
                Session["tinchi"] = "vuotqua";
            }
            else if (ViewBag.trung1 == ViewBag.trung2)
            {
                Session["trung"] = "trung mon";
            }
            else
            {
                db.SinhVien_LopMons.InsertAllOnSubmit(updatesvlm);
                db.SubmitChanges();
                Session["success"] = "ok";
            }

            return RedirectToAction("testlist");
        }


        // Draft:code test ko dùng đến
        public ActionResult testlist()
        {
            var tkuser = Convert.ToString(Session["idsv"]);
            if (string.IsNullOrWhiteSpace(tkuser))
            {
                return RedirectToAction("DangNhap");
            }

            return View(db.ThoiKhoaBieus.ToList());
        }
        [HttpPost]
        public ActionResult testsave(List<ThoiKhoaBieu> hobbies)
        {
            var idsv = Session["idsv"]; // luu session idsv luc log in vao

            //DS cần thiết để kiểm tra SV DA HOC
            List<SinhVien_LopMon> svlmnames = db.SinhVien_LopMons.ToList();

            List<ThoiKhoaBieu> tkbnames = db.ThoiKhoaBieus.ToList();




            foreach (ThoiKhoaBieu tkb in hobbies)
            {
                ThoiKhoaBieu updatetkb = db.ThoiKhoaBieus.ToList().Find(p => p.IDTKB == tkb.IDTKB);

                updatetkb.IsSelected = tkb.IsSelected;
                /*  if (updatetkb.IsSelected)
                  {*/

                /*   }
                   else
                   {
                       Session["test"] = "ko ton tai";
                   }*/
                /*
                Session["Luuchecked"] = updatetkb.MaLM;*/
            }
            // vậy thằng này thì sao nó lấy đc cái lúc check á

            //code này để làm j z ông
            // nó tìm thằng nào có idTKB á r nó lấy cái isselected gán á
            //nó lưu vô đâu z ông lưu tkb á chỉ có lưu cái check thôi cái  isselected á
            /*  db.s();*/
            db.SubmitChanges();

            return RedirectToAction("testlist");
        }
    }
}