using SoLienLacDienTu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DO_AN_Thu_nghiem.Controllers
{
    public class GVController : Controller
    {
        // GET: GV

        DataClasses1DataContext db = new DataClasses1DataContext();

        // GET: GV
        public ActionResult IndexGV()
        {

            return View();
        }
        public ActionResult GiaoDien(GiangVien GiangVienModel)
        {
            return View();

        }
        [HttpPost]
        public ActionResult Login(GiangVien GiangVienModel)
        {


            var GiangVienDetail = db.GiangViens.Where(x => x.MaGV == GiangVienModel.MaGV && x.Password == GiangVienModel.Password).FirstOrDefault();
            if (GiangVienDetail == null)
            {
                ViewBag.message = "Sai Tài khoản Hoặc mật khẩu! ";
                return View("IndexGV", GiangVienModel);

            }
            else
            {
                ViewData["mamon"] = GiangVienDetail.MaGV;
                Session["MaGV"] = GiangVienDetail.MaGV;

                return RedirectToAction("GiaoDien", "GV");
            }
        }
        public ActionResult XemTKBGVTong(GiangVien GVModel)
        {
            List<LopMon> LMnames = db.LopMons.ToList();                         // khởi tạo Lmnames cho danh sách lopmon
            List<MonHoc> MonHocnames = db.MonHocs.ToList();                     // khởi tạo MonHocnames cho danh sách monhoc
            List<GiangVien> GVnames = db.GiangViens.ToList();                   // khởi tạo GVnames cho danh sách giangvien
            List<GV_Mon> GV_Monnames = db.GV_Mons.ToList();                     // khởi tạo GV_Monnames cho danh sách GV_mon
            List<GV_LM> gv_lmnames = db.GV_LMs.ToList();                        // khởi tạo TKBnames cho danh sách TKB 
            List<ThoiKhoaBieu> TKNnames = db.ThoiKhoaBieus.ToList();

            var idgv = Session["MaGV"];
            
            var XemTKB = (from a in GVnames
                          where a.MaGV == Convert.ToString(Session["MaGV"])
                          join b in gv_lmnames on a.MaGV equals b.MaGV
                          join c in LMnames on b.MaLM equals c.MaLM
                          join d in MonHocnames on c.MaMon equals d.MaMon
                      //    join e in GV_Monnames on a.MaGV equals e.MaGV
                          join t in TKNnames on d.MaMon equals t.MaMon
                          where b.MaLM == c.MaLM && a.MaGV == t.MaGV && c.MaMon ==d.MaMon && c.MaLM == t.MaLM
                          select new SVXemDiem
                          { 
                              GVDetail = a,
                              GV_LMDetail =b,
                              LopMonDetail = c,
                              MonHocDetail = d,
                            //  GV_MonDetail = e,
                              TKBDetail = t,
                          }).ToList();

            return View(XemTKB); // return view lại theo danh XemTKB đã xếp
        }
        // list thông báo
        public ActionResult TBchoSV()
        {
            var idgv = Session["MaGV"];

            var thongbao = from p in db.ThongBaos
                           select p;
            return View(thongbao);

        }
        public ActionResult TKBTuanGV()
        {
            List<LopMon> LMnames = db.LopMons.ToList();                         // khởi tạo Lmnames cho danh sách lopmon
            List<MonHoc> MonHocnames = db.MonHocs.ToList();                     // khởi tạo MonHocnames cho danh sách monhoc
            List<GiangVien> GVnames = db.GiangViens.ToList();                   // khởi tạo GVnames cho danh sách giangvien
            List<GV_Mon> GV_Monnames = db.GV_Mons.ToList();                     // khởi tạo GV_Monnames cho danh sách GV_mon
            List<GV_LM> gv_lmnames = db.GV_LMs.ToList();                        // khởi tạo TKBnames cho danh sách TKB 
            List<ThoiKhoaBieu> TKNnames = db.ThoiKhoaBieus.ToList();

            var idgv = Session["MaGV"];

            var XemTKB = (from a in GVnames
                          where a.MaGV == Convert.ToString(Session["MaGV"])
                          join b in gv_lmnames on a.MaGV equals b.MaGV
                          join c in LMnames on b.MaLM equals c.MaLM
                          join d in MonHocnames on c.MaMon equals d.MaMon
                          //    join e in GV_Monnames on a.MaGV equals e.MaGV
                          join t in TKNnames on d.MaMon equals t.MaMon
                          where b.MaLM == c.MaLM && a.MaGV == t.MaGV && c.MaMon == d.MaMon && c.MaLM == t.MaLM
                          select new SVXemDiem
                          {
                              GVDetail = a,
                              GV_LMDetail = b,
                              LopMonDetail = c,
                              MonHocDetail = d,
                              //  GV_MonDetail = e,
                              TKBDetail = t,
                          }).ToList();
            return View(XemTKB);
        }
        [HttpPost]
        public ActionResult TKBTuanGV(int ids)
        {
            List<LopMon> LMnames = db.LopMons.ToList();                         // khởi tạo Lmnames cho danh sách lopmon
            List<MonHoc> MonHocnames = db.MonHocs.ToList();                     // khởi tạo MonHocnames cho danh sách monhoc
            List<GiangVien> GVnames = db.GiangViens.ToList();                   // khởi tạo GVnames cho danh sách giangvien
            List<GV_Mon> GV_Monnames = db.GV_Mons.ToList();                     // khởi tạo GV_Monnames cho danh sách GV_mon
            List<GV_LM> gv_lmnames = db.GV_LMs.ToList();                        // khởi tạo TKBnames cho danh sách TKB 
            List<ThoiKhoaBieu> TKNnames = db.ThoiKhoaBieus.ToList();

            var idgv = Session["MaGV"];

            var XemTKB = (from a in GVnames
                          where a.MaGV == Convert.ToString(Session["MaGV"])
                          join b in gv_lmnames on a.MaGV equals b.MaGV
                          join c in LMnames on b.MaLM equals c.MaLM
                          join d in MonHocnames on c.MaMon equals d.MaMon
                          //    join e in GV_Monnames on a.MaGV equals e.MaGV
                          join t in TKNnames on d.MaMon equals t.MaMon
                          where b.MaLM == c.MaLM && a.MaGV == t.MaGV && c.MaMon == d.MaMon && c.MaLM == t.MaLM
                          select new SVXemDiem
                          {
                              GVDetail = a,
                              GV_LMDetail = b,
                              LopMonDetail = c,
                              MonHocDetail = d,
                              //  GV_MonDetail = e,
                              TKBDetail = t,
                          }).ToList();
            if (ids != null)
            {
                Session["i"] = ids;
                return View(XemTKB);
            }
            else
            {
                return View();
            }
          
        }
        public ActionResult CreateLNT()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateLNT([Bind(Include = "IDTB, MaGV,NoiDung, MaLop, MaLM, MaSV")] ThongBao sp)
        {
            var idgv =Convert.ToString( Session["MaGV"]);
            sp.MaGV = idgv;
            sp.NgayDang = DateTime.Now;
            if (ModelState.IsValid)
            {

                db.ThongBaos.InsertOnSubmit(sp);
                db.SubmitChanges();
                return RedirectToAction("TBchoSV");
            }

            return View(sp);
        }

        public ThongBao getIDTB(int id)
        {
            return db.ThongBaos.Where(m => m.IDTB == id).FirstOrDefault();
        }
        [HttpGet]
        public ActionResult EditLNT(int id)
        {

            var editLNT = db.ThongBaos.First(m => m.IDTB == id);
            return View(editLNT);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditLNT([Bind(Include = "IDTB, MaGV ,NoiDung, NgayDang, MaLop, MaLM, MaSV")] ThongBao lnt)
        {
            //var idgv = Convert.ToString(Session["MaGV"]);
            //lnt.MaGV = idgv;
            ThongBao temp = getIDTB(lnt.IDTB);
            if (ModelState.IsValid)
            {

                temp.MaGV = lnt.MaGV;
                temp.NoiDung = lnt.NoiDung;
                temp.NgayDang = DateTime.Now;
                temp.MaLM = lnt.MaLM;
                temp.MaLop = lnt.MaLop;
                temp.MaSV = lnt.MaSV;
                UpdateModel(lnt);
                db.SubmitChanges();
                return RedirectToAction("TBchoSV");
            }
            return View(lnt);
        }

        public ActionResult DeleteLNT(int id)
        {
            var deletelnt = db.ThongBaos.First(m => m.IDTB == id);
            return View(deletelnt);
        }

        [HttpPost]
        public ActionResult DeleteLNT(int id, FormCollection collection)
        {
            var xoa = db.ThongBaos.Where(m => m.IDTB == id).First();
            db.ThongBaos.DeleteOnSubmit(xoa);
            db.SubmitChanges();
            return RedirectToAction("TBchoSV");
        }
        //// Detail
        //public ActionResult Details(int id)
        //{
        //    GV model = _repository.GetUserById(id);
        //    return View(model);
        //}

        //// gửi Thông báo cho SV

        //public ActionResult Create(int id)
        //{
        //    GV model = _repository.GetUserById(id);

        //    List<GV_Mon> GV_Monnames = db.GV_Mons.ToList();

        //    List<LopMon> cate = db.LopMons.ToList();

        //    List<SinhVien_Lop> lopcung = db.SinhVien_Lops.ToList();
        //    string idgv = Convert.ToString( Session["MaGV"]);
        //    var selectOptions =
        //                from u in db.GV_Mons
        //                where u.MaGV == idgv
        //                join t in db.MonHocs on u.MaMon equals t.MaMon
        //                join l in db.LopMons on t.MaMon equals l.MaMon

        //                select new SelectListItem { 
        //                Value = u.MaGV, 
        //                Text = "" 
        //                };


        //    // Tạo SelectList
        //    SelectList cateList = new SelectList(cate, "MaLM", "MaLM");
        //    SelectList LOPCUNGList = new SelectList(lopcung, "MaLop", "MaLop");
        //    SelectList sinhVienlist = new SelectList(lopcung, "MaLop", "MaSV");
        //    // Set vào ViewBag
        //    ViewBag.CategoryList = cateList;
        //    ViewBag.LOPCUNGList = LOPCUNGList;
        //    ViewBag.sinhVienlist = sinhVienlist;
        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult Create(GV user)
        //{
        //    //List<ThongBao> TBnames = db.ThongBaos.ToList();
        //    //var ngay = (from t in db.ThongBaos select t.NgayDang).ToList() ;

        //    //if ( user.NgayDang < DateTime.Now)
        //    //{
        //    //    ViewBag.Thongbao = "nhập sai ngay";
        //    //    return View("Create");
        //    //}
        //    int sosanh = DateTime.Compare(user.NgayDang, DateTime.Now);

        //    //if (sosanh < 0)
        //    //{
        //    //    var mgv = Session["MaGV"];
        //    //    //try
        //    //    //{
        //    //    //    if (ModelState.IsValid)
        //    //    //    {
        //    //            ViewBag.Thongbao = "ngay dang nho hoi ngay he thong";
        //    //            _repository.InsertUser(user);
        //    //            return View(user);
        //    //    //    }
        //    //    //}
        //    //    //catch (DataException)
        //    //    //{

        //    //    //    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");


        //    //    //}

        //    //    //return RedirectToAction("TBchoSV");
        //    //}
        //    //else
        //    //{


        //    //user.NgayDang = DateTime.Now;
        //    //var mgv = Session["MaGV"];
        //    try
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                _repository.InsertUser(user);
        //                return RedirectToAction("TBchoSV");
        //            }
        //        }
        //        catch (DataException)
        //        {

        //            ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
        //        }
        //        //user.MaGV = Convert.ToString(mgv);
        //        return View(user);
        //    //}
        //}



        //[NonAction]
        //public SelectList ToSelectList(DataTable table, string valueField, string textField)
        //{
        //    List<SelectListItem> list = new List<SelectListItem>();

        //    foreach (DataRow row in table.Rows)
        //    {
        //        list.Add(new SelectListItem()
        //        {
        //            Text = row[textField].ToString(),
        //            Value = row[valueField].ToString()
        //        });
        //    }

        //    return new SelectList(list, "Value", "Text");
        //}


        //// Sửa TB của SV
        //public ActionResult Edit(int id)
        //{
        //    GV model = _repository.GetUserById(id);
        //    return View(model);
        //}
        //[HttpPost]
        //public ActionResult Edit(GV user)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            _repository.UpdateUser(user);
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    catch (DataException)
        //    {
        //        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
        //    }
        //    return View(user);
        //}


        //// Xóa TB của sv
        //public ActionResult Delete(int id, bool? saveChangesError)
        //{
        //    if (saveChangesError.GetValueOrDefault())
        //    {
        //        ViewBag.ErrorMessage = "Unable to save changes. Try again, and if the problem persists see your system administrator.";
        //    }
        //    GV user = _repository.GetUserById(id);
        //    return View(user);
        //}
        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    try
        //    {
        //        GV user = _repository.GetUserById(id);
        //        _repository.DeleteUser(id);
        //    }
        //    catch (DataException)
        //    {
        //        return RedirectToAction("Delete",
        //        new System.Web.Routing.RouteValueDictionary {
        //  { "id", id },
        //  { "saveChangesError", true } });
        //    }
        //    return RedirectToAction("Index");
        //}



    }
}