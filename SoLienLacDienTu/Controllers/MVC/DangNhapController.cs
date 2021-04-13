using SoLienLacDienTu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoLienLacDienTu.Controllers
{
    public class DangNhapController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        // GET: DangNhap
        public ActionResult SignUp()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
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
                Admin ad = db.Admins.SingleOrDefault(n => n.TaiKhoan == tendn && n.Password == matkhau);
                if (ad != null)
                {
                    Session["TKadmin"] = ad.TenAdmin;
                    return RedirectToAction("Index", "Homeadmin");
                }
                else
                    ViewBag.ThongBao = "Tên Đăng Nhập Hoặc Mật Khẩu Không Đúng !";
            }
            return View();
        }
    }
}