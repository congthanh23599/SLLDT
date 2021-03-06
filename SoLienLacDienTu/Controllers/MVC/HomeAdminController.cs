using OfficeOpenXml;
using SoLienLacDienTu.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using SoLienLacDienTu.Helper;

namespace DO_AN_Thu_nghiem.Controllers
{
    public class HomeAdminController : Controller
    {
        // GET: HomeAdmin
        DataClasses1DataContext db = new DataClasses1DataContext();
        // GET: ADMIN/HomeAdmin
        public ActionResult Index()
        {
            var tkuser = Convert.ToString(Session["TKadmin"]);
            if (string.IsNullOrWhiteSpace(tkuser))
            {
                return RedirectToAction("Login", "DangNhap");
            }
            return View();
        }
        public ActionResult DangXuat()
        {
            var tkdangxuat = Convert.ToString(Session["TKadmin"]);
            if (string.IsNullOrWhiteSpace(tkdangxuat))
            {

                return RedirectToAction("Login", "DangNhap");
            }
            else
            {
                Session["TKadmin"] = "";

                return RedirectToAction("Login", "DangNhap");
            }
        }
        public ActionResult DanhSachSV()
        {
            var tkuser = Convert.ToString(Session["TKadmin"]);
            if (string.IsNullOrWhiteSpace(tkuser))
            {
                return RedirectToAction("Login", "DangNhap");
            }
            //var Sinhvien = from p in db.SinhViens
            //               select p;

            return View(db.SinhViens.ToList());
        }
        [HttpPost]
        public ActionResult DanhSachSV(HttpPostedFileBase file)
        {
            var tkuser = Convert.ToString(Session["TKadmin"]);
            if (string.IsNullOrWhiteSpace(tkuser))
            {
                return RedirectToAction("Login", "DangNhap");
            }
            string filePath = string.Empty;
            if (file != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                file.SaveAs(filePath);

                string conString = string.Empty;

                switch (extension)
                {
                    case ".xls": //Excel 97-03.
                        conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                        break;
                    case ".xlsx": //Excel 07 and above.
                        conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                        break;
                }

                DataTable dt = new DataTable();
                conString = string.Format(conString, filePath);

                using (OleDbConnection connExcel = new OleDbConnection(conString))
                {
                    using (OleDbCommand cmdExcel = new OleDbCommand())
                    {
                        using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                        {
                            cmdExcel.Connection = connExcel;

                            //Get the name of First Sheet.
                            connExcel.Open();
                            DataTable dtExcelSchema;
                            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            connExcel.Close();

                            //Read Data from First Sheet.
                            connExcel.Open();
                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                            odaExcel.SelectCommand = cmdExcel;
                            odaExcel.Fill(dt);
                            connExcel.Close();
                        }
                    }
                }

                conString = @"Server=LAPTOP-TUUBPQO5\SQLEXPRESS;Database=SoLienLacDT;Trusted_Connection=True;";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name.
                        sqlBulkCopy.DestinationTableName = "dbo.SinhVien";

                        // Map the Excel columns with that of the database table, this is optional but good if you do
                        // 
                        sqlBulkCopy.ColumnMappings.Add("MaSV", "MaSV");
                        sqlBulkCopy.ColumnMappings.Add("TenSV", "TenSV");
                        sqlBulkCopy.ColumnMappings.Add("GioiTinh", "GioiTinh");
                        sqlBulkCopy.ColumnMappings.Add("DiaChi", "DiaChi");
                        sqlBulkCopy.ColumnMappings.Add("Email", "Email");
                        sqlBulkCopy.ColumnMappings.Add("SDT", "SDT");
                        sqlBulkCopy.ColumnMappings.Add("NgaySinh", "NgaySinh");
                        sqlBulkCopy.ColumnMappings.Add("Password", "Password");
                        sqlBulkCopy.ColumnMappings.Add("MaPH", "MaPH");


                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }
                }
            }
            //if the code reach here means everthing goes fine and excel data is imported into database
            ViewBag.Success = "File Imported and excel data saved into database";
            List<SoLienLacDienTu.Models.LamQuenCodeFirst.SinhVien> emplist = db.SinhViens.Select(x => new SoLienLacDienTu.Models.LamQuenCodeFirst.SinhVien
            {
                MaSV = x.MaSV,
                TenSV = x.TenSV,
                GioiTinh = x.GioiTinh,
                Diachi = x.Diachi,
                Email = x.Email,
                SDT = x.SDT,
                NgaySinh = x.NgaySinh,
                Password = x.Password,
                MaPH = x.MaPH,
            }).ToList();

            return View(emplist);

        }
        public void ExcelToSinhVien()
        {

            List<SoLienLacDienTu.Models.LamQuenCodeFirst.SinhVien> emplist = db.SinhViens.Select(x => new SoLienLacDienTu.Models.LamQuenCodeFirst.SinhVien
            {
                MaSV = x.MaSV,
                TenSV = x.TenSV,
                GioiTinh = x.GioiTinh,
                Diachi = x.Diachi,
                Email = x.Email,
                SDT = x.SDT,
                NgaySinh = x.NgaySinh,
                Password = x.Password,
                MaPH = x.MaPH,

            }).ToList();
            //List<DiemCTViews> emplist = db.DiemCTs.Select(x => new DiemCTViews
            //{
            //    MaMon = x.MaMon,
            //    IDD = x.IDDiem,
            //    MaSV =
            //    DiemGK = x.DiemGK,
            //    DiemCK = x.DiemCK,
            //    DTBMon = x.DiemTBM,
            //    NamHoc = x.NamHoc,
            //    HocKy = x.HocKy,
            //    KQ = x.KQ,
            //}).ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Bảng sinh viên";
            ws.Cells["B1"].Value = " ";

            ws.Cells["A2"].Value = "";
            ws.Cells["B2"].Value = "Phòng đào tạo";

            ws.Cells["A3"].Value = "Ngày xuất";
            ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A6"].Value = "MaSV";
            ws.Cells["B6"].Value = "TenSV";
            ws.Cells["C6"].Value = "GioiTinh";
            ws.Cells["D6"].Value = "DiaChi";
            ws.Cells["E6"].Value = "Email";
            ws.Cells["F6"].Value = "SDT";
            ws.Cells["G6"].Value = "NgaySinh";
            ws.Cells["H6"].Value = "MaPH";
            ws.Cells["I6"].Value = "Password";

            int rowStart = 7;
            foreach (var item in emplist)
            {   /*
                if (item.Experience < 5)
                {
                    ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Row(rowStart).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));

                }*/

                ws.Cells[string.Format("A{0}", rowStart)].Value = item.MaSV;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.TenSV;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.GioiTinh;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.Diachi;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.Email;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.SDT;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.NgaySinh;
                ws.Cells[string.Format("H{0}", rowStart)].Value = item.MaPH;
                ws.Cells[string.Format("I{0}", rowStart)].Value = item.Password;
                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "SinhVien.xls");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();

        }
        public ActionResult TaoTTSV()
        {
            var tkuser = Convert.ToString(Session["TKadmin"]);
            if (string.IsNullOrWhiteSpace(tkuser))
            {
                return RedirectToAction("Login", "DangNhap");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult TaoTTSV([Bind(Include = "MaSV, TenSV, GioiTinh , Diachi, Email , SDT, NgaySinh, Password ")] SinhVien sp)
        {

            //var path = "";
            //var filename = "";
            if (ModelState.IsValid)
            {
                db.SinhViens.InsertOnSubmit(sp);
                db.SubmitChanges();
                return RedirectToAction("DanhSachSV");
            }

            return View(sp);
        }

        public SinhVien getMaSV(string id)
        {

            return db.SinhViens.Where(m => m.MaSV == id).FirstOrDefault();
        }
        [HttpGet]
        public ActionResult EditTTSV(string id)
        {
            var tkuser = Convert.ToString(Session["TKadmin"]);
            if (string.IsNullOrWhiteSpace(tkuser))
            {
                return RedirectToAction("Login", "DangNhap");
            }
            var editLNT = db.SinhViens.First(m => m.MaSV == id);
            return View(editLNT);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditTTSV([Bind(Include = "MaSV, TenSV, GioiTinh , Diachi, Email , SDT, NgaySinh, Password ")] SinhVien lSV)
        {

            //var path = "";
            //var filename = "";
            SinhVien temp = getMaSV(lSV.MaSV);
            if (ModelState.IsValid)
            {

                temp.TenSV = lSV.TenSV;
                temp.GioiTinh = lSV.GioiTinh;
                temp.Diachi = lSV.Diachi;
                temp.Email = lSV.Email;
                temp.SDT = lSV.SDT;
                temp.NgaySinh = lSV.NgaySinh;
                temp.Password = lSV.Password;
                //temp.IMGPath = lnt.IMGPath;
                UpdateModel(lSV);
                db.SubmitChanges();
                return RedirectToAction("DanhSachSV");
            }
            return View(lSV);
        }
        public ActionResult DanhSachGV()
        {
            var tkuser = Convert.ToString(Session["TKadmin"]);
            if (string.IsNullOrWhiteSpace(tkuser))
            {
                return RedirectToAction("Login", "DangNhap");
            }
            List<SoLienLacDienTu.Models.LamQuenCodeFirst.GiangVien> emplist = db.GiangViens.Select(x => new SoLienLacDienTu.Models.LamQuenCodeFirst.GiangVien
            {
                MaGV = x.MaGV,
                TenGV = x.TenGV,
                Password = x.Password,
            }).ToList();
            //var GiangVien = from p in db.GiangViens
            //                select p;
            return View(emplist);

        }
        [HttpPost]
        public ActionResult DanhSachGV(HttpPostedFileBase file)
        {
            string filePath = string.Empty;
            if (file != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                file.SaveAs(filePath);

                string conString = string.Empty;

                switch (extension)
                {
                    case ".xls": //Excel 97-03.
                        conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                        break;
                    case ".xlsx": //Excel 07 and above.
                        conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                        break;
                }

                DataTable dt = new DataTable();
                conString = string.Format(conString, filePath);

                using (OleDbConnection connExcel = new OleDbConnection(conString))
                {
                    using (OleDbCommand cmdExcel = new OleDbCommand())
                    {
                        using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                        {
                            cmdExcel.Connection = connExcel;

                            //Get the name of First Sheet.
                            connExcel.Open();
                            DataTable dtExcelSchema;
                            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            connExcel.Close();

                            //Read Data from First Sheet.
                            connExcel.Open();
                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                            odaExcel.SelectCommand = cmdExcel;
                            odaExcel.Fill(dt);
                            connExcel.Close();
                        }
                    }
                }

                conString = @"Server=LAPTOP-TUUBPQO5\SQLEXPRESS;Database=SoLienLacDT;Trusted_Connection=True;";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name.
                        sqlBulkCopy.DestinationTableName = "dbo.GiangVien";

                        // Map the Excel columns with that of the database table, this is optional but good if you do
                        // 
                        sqlBulkCopy.ColumnMappings.Add("MaGV", "MaGV");
                        sqlBulkCopy.ColumnMappings.Add("TenGV", "TenGV");
                        sqlBulkCopy.ColumnMappings.Add("Password", "Password");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }
                }
            }
            //if the code reach here means everthing goes fine and excel data is imported into database
            ViewBag.Success = "File Imported and excel data saved into database";
            List<SoLienLacDienTu.Models.LamQuenCodeFirst.GiangVien> emplist = db.GiangViens.Select(x => new SoLienLacDienTu.Models.LamQuenCodeFirst.GiangVien
            {
                MaGV = x.MaGV,
                TenGV = x.TenGV,
                Password = x.Password,

            }).ToList();

            return View(emplist);

        }
        public void ExcelToGiangVien()
        {
            List<SoLienLacDienTu.Models.LamQuenCodeFirst.GiangVien> emplist = db.GiangViens.Select(x => new SoLienLacDienTu.Models.LamQuenCodeFirst.GiangVien
            {
                MaGV = x.MaGV,
                TenGV = x.TenGV,
                Password = x.Password,

            }).ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Bảng giảng viên";
            ws.Cells["B1"].Value = " ";

            ws.Cells["A2"].Value = "";
            ws.Cells["B2"].Value = "Phòng đào tạo";

            ws.Cells["A3"].Value = "Ngày xuất";
            ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A6"].Value = "MaGV";
            ws.Cells["B6"].Value = "TenGV";
            ws.Cells["C6"].Value = "Password";


            int rowStart = 7;
            foreach (var item in emplist)
            {   /*
                if (item.Experience < 5)
                {
                    ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Row(rowStart).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));

                }*/

                ws.Cells[string.Format("A{0}", rowStart)].Value = item.MaGV;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.TenGV;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.Password;
                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "GiangVien.xls");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();

        }
        public ActionResult TaoTTGV()
        {
            var tkuser = Convert.ToString(Session["TKadmin"]);
            if (string.IsNullOrWhiteSpace(tkuser))
            {
                return RedirectToAction("Login", "DangNhap");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult TaoTTGV([Bind(Include = "MaGV, TenGV, Password ")] GiangVien sp)
        {

            //var path = "";
            //var filename = "";
            if (ModelState.IsValid)
            {
                db.GiangViens.InsertOnSubmit(sp);
                db.SubmitChanges();
                return RedirectToAction("DanhSachGV");
            }

            return View(sp);
        }

        public GiangVien getMaGV(string idGV)
        {
            return db.GiangViens.Where(n => n.MaGV == idGV).FirstOrDefault();
        }
        [HttpGet]
        public ActionResult EditTTGV(string id)
        {
            var tkuser = Convert.ToString(Session["TKadmin"]);
            if (string.IsNullOrWhiteSpace(tkuser))
            {
                return RedirectToAction("Login", "DangNhap");
            }
            var editLNT = db.GiangViens.First(m => m.MaGV == id);
            return View(editLNT);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditTTGV([Bind(Include = "MaGV, TenGV, Password ")] GiangVien lGV)
        {
            //var path = "";
            //var filename = "";
            GiangVien temp = getMaGV(lGV.MaGV);
            if (ModelState.IsValid)
            {
                temp.MaGV = lGV.MaGV;
                temp.TenGV = lGV.TenGV;
                temp.Password = lGV.Password;
                //temp.IMGPath = lnt.IMGPath;
                UpdateModel(lGV);
                db.SubmitChanges();
                return RedirectToAction("DanhSachGV");
            }
            return View(lGV);
        }
        public ActionResult QLTKB()
        {
            var tkuser = Convert.ToString(Session["TKadmin"]);
            if (string.IsNullOrWhiteSpace(tkuser))
            {
                return RedirectToAction("Login", "DangNhap");
            }
            List<DkMon> emplist = db.ThoiKhoaBieus.Select(x => new DkMon
            {
                MaMon = x.MaMon,
                TenMon = x.TenMon,
                MaGV = x.MaGV,
                Phong = x.Phong,
                TietBD = x.TietBD,
                Thu = x.Thu,
                TH = x.TH,
                ST = x.ST,
                ThoiGianBD = x.ThoiGianBD,
                ThoiGianKT = x.ThoiGianKT,
                Nam = x.Nam,
                HocKy = x.HocKy,
                MaLM = x.MaLM,
            }).ToList();

            return View(emplist);

        }
        [HttpPost]
        public ActionResult QLTKB(HttpPostedFileBase file, string de)
        {

            string filePath = string.Empty;
            if (de == "Có")
            {
                db.ThoiKhoaBieus.DeleteAllOnSubmit(db.ThoiKhoaBieus);
                db.SubmitChanges();
                if (file != null)
                {
                    string path = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    
                    filePath = path + Path.GetFileName(file.FileName);
                    string extension = Path.GetExtension(file.FileName);
                    file.SaveAs(filePath);

                    string conString = string.Empty;

                    switch (extension)
                    {
                        case ".xls": //Excel 97-03.
                            conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                            break;
                        case ".xlsx": //Excel 07 and above.
                            conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                            break;
                    }

                    DataTable dt = new DataTable();
                    conString = string.Format(conString, filePath);

                    using (OleDbConnection connExcel = new OleDbConnection(conString))
                    {
                        using (OleDbCommand cmdExcel = new OleDbCommand())
                        {
                            using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                            {
                                cmdExcel.Connection = connExcel;

                                //Get the name of First Sheet.
                                connExcel.Open();
                                DataTable dtExcelSchema;
                                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

                                connExcel.Close();

                                //Read Data from First Sheet.
                                connExcel.Open();
                                cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";

                                //cmdExcel.CommandText = "UPDATE [" + sheetName + "] ";

                                odaExcel.SelectCommand = cmdExcel;
                                odaExcel.Fill(dt);
                                connExcel.Close();
                            }
                        }
                    }

                    conString = @"Server=LAPTOP-TUUBPQO5\SQLEXPRESS;Database=SoLienLacDT;Trusted_Connection=True;";
                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                        {
                            //Set the database table name.
                            sqlBulkCopy.DestinationTableName = "dbo.ThoiKhoaBieu";

                            // Map the Excel columns with that of the database table, this is optional but good if you do
                            // 
                            sqlBulkCopy.ColumnMappings.Add("MaMon", "MaMon");
                            sqlBulkCopy.ColumnMappings.Add("MaLM", "MaLM");
                            sqlBulkCopy.ColumnMappings.Add("TenMon", "TenMon");
                            sqlBulkCopy.ColumnMappings.Add("MaGV", "MaGV");
                            sqlBulkCopy.ColumnMappings.Add("Phong", "Phong");
                            sqlBulkCopy.ColumnMappings.Add("TietBD", "TietBD");
                            sqlBulkCopy.ColumnMappings.Add("Thu", "Thu");
                            sqlBulkCopy.ColumnMappings.Add("TH", "TH");
                            sqlBulkCopy.ColumnMappings.Add("ST", "ST");
                            sqlBulkCopy.ColumnMappings.Add("ThoiGianBD", "ThoiGianBD");
                            sqlBulkCopy.ColumnMappings.Add("ThoiGianKT", "ThoiGianKT");
                            sqlBulkCopy.ColumnMappings.Add("Nam", "Nam");
                            sqlBulkCopy.ColumnMappings.Add("HocKy", "HocKy");
                            sqlBulkCopy.ColumnMappings.Add("IsSelected", "IsSelected");

                            con.Open();
                            
                            sqlBulkCopy.WriteToServer(dt);
                            con.Close();
                        }
                    }
                }
                //if the code reach here means everthing goes fine and excel data is imported into database
                ViewBag.Success = "File Imported and excel data saved into database";
                List<DkMon> emplist = db.ThoiKhoaBieus.Select(x => new DkMon
                {
                    MaMon = x.MaMon,
                    TenMon = x.TenMon,
                    MaGV = x.MaGV,
                    Phong = x.Phong,
                    TietBD = x.TietBD,
                    Thu = x.Thu,
                    TH = x.TH,
                    ST = x.ST,
                    ThoiGianBD = x.ThoiGianBD,
                    ThoiGianKT = x.ThoiGianKT,
                    Nam = x.Nam,
                    HocKy = x.HocKy,
                    MaLM = x.MaLM,
                }).ToList();

                return View(emplist);

            }
            else
            {
                if (file != null)
                {
                    string path = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    filePath = path + Path.GetFileName(file.FileName);
                    string extension = Path.GetExtension(file.FileName);
                    file.SaveAs(filePath);

                    string conString = string.Empty;

                    switch (extension)
                    {
                        case ".xls": //Excel 97-03.
                            conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                            break;
                        case ".xlsx": //Excel 07 and above.
                            conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                            break;
                    }

                    DataTable dt = new DataTable();
                    conString = string.Format(conString, filePath);

                    using (OleDbConnection connExcel = new OleDbConnection(conString))
                    {
                        using (OleDbCommand cmdExcel = new OleDbCommand())
                        {
                            using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                            {
                                cmdExcel.Connection = connExcel;

                                //Get the name of First Sheet.
                                connExcel.Open();
                                DataTable dtExcelSchema;
                                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

                                connExcel.Close();

                                //Read Data from First Sheet.
                                connExcel.Open();
                                cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";

                                //cmdExcel.CommandText = "UPDATE [" + sheetName + "] ";

                                odaExcel.SelectCommand = cmdExcel;
                                odaExcel.Fill(dt);
                                connExcel.Close();
                            }
                        }
                    }

                    conString = @"Server=LAPTOP-TUUBPQO5\SQLEXPRESS;Database=SoLienLacDT;Trusted_Connection=True;";
                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                        {
                            //Set the database table name.
                            sqlBulkCopy.DestinationTableName = "dbo.ThoiKhoaBieu";

                            // Map the Excel columns with that of the database table, this is optional but good if you do
                            // 
                            sqlBulkCopy.ColumnMappings.Add("MaMon", "MaMon");
                            sqlBulkCopy.ColumnMappings.Add("MaLM", "MaLM");
                            sqlBulkCopy.ColumnMappings.Add("TenMon", "TenMon");
                            sqlBulkCopy.ColumnMappings.Add("MaGV", "MaGV");
                            sqlBulkCopy.ColumnMappings.Add("Phong", "Phong");
                            sqlBulkCopy.ColumnMappings.Add("TietBD", "TietBD");
                            sqlBulkCopy.ColumnMappings.Add("Thu", "Thu");
                            sqlBulkCopy.ColumnMappings.Add("TH", "TH");
                            sqlBulkCopy.ColumnMappings.Add("ST", "ST");
                            sqlBulkCopy.ColumnMappings.Add("ThoiGianBD", "ThoiGianBD");
                            sqlBulkCopy.ColumnMappings.Add("ThoiGianKT", "ThoiGianKT");
                            sqlBulkCopy.ColumnMappings.Add("Nam", "Nam");
                            sqlBulkCopy.ColumnMappings.Add("HocKy", "HocKy");
                            sqlBulkCopy.ColumnMappings.Add("IsSelected", "IsSelected");


                            con.Open();
                            sqlBulkCopy.WriteToServer(dt);
                            con.Close();
                        }
                    }
                }
                //if the code reach here means everthing goes fine and excel data is imported into database
                ViewBag.Success = "File Imported and excel data saved into database";
                List<DkMon> emplist = db.ThoiKhoaBieus.Select(x => new DkMon
                {
                    MaMon = x.MaMon,
                    TenMon = x.TenMon,
                    MaGV = x.MaGV,
                    Phong = x.Phong,
                    TietBD = x.TietBD,
                    Thu = x.Thu,
                    TH = x.TH,
                    ST = x.ST,
                    ThoiGianBD = x.ThoiGianBD,
                    ThoiGianKT = x.ThoiGianKT,
                    Nam = x.Nam,
                    HocKy = x.HocKy,
                    MaLM = x.MaLM,
                }).ToList();

                return View(emplist);

            }

        }
        // check lại thứ tự col và dư liệu data còn sai chỗ
        public void ExportToExcel()
        {
            List<DkMon> emplist = db.ThoiKhoaBieus.Select(x => new DkMon
            {
                MaMon = x.MaMon,
                TenMon = x.TenMon,
                MaGV = x.MaGV,
                Phong = x.Phong,
                TietBD = x.TietBD,
                Thu = x.Thu,
                TH = x.TH,
                ST = x.ST,
                ThoiGianBD = x.ThoiGianBD,
                ThoiGianKT = x.ThoiGianKT,
                Nam = x.Nam,
                HocKy = x.HocKy,
                MaLM = x.MaLM,
            }).ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Communication";
            ws.Cells["B1"].Value = "Com1";

            ws.Cells["A2"].Value = "Report";
            ws.Cells["B2"].Value = "Report1";

            ws.Cells["A3"].Value = "Date";
            ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A6"].Value = "MaMon";
            ws.Cells["B6"].Value = "MaLM";
            ws.Cells["C6"].Value = "TenMon";
            ws.Cells["D6"].Value = "MaGV";
            ws.Cells["E6"].Value = "Phong";
            ws.Cells["F6"].Value = "TietBD";
            ws.Cells["G6"].Value = "Thu";
            ws.Cells["H6"].Value = "TH";
            ws.Cells["I6"].Value = "ST";
            ws.Cells["J6"].Value = "ThoiGianBD";
            ws.Cells["K6"].Value = "ThoiGianKT";
            ws.Cells["L6"].Value = "Nam";
            ws.Cells["M6"].Value = "HocKy";
            int rowStart = 7;
            foreach (var item in emplist)
            {   /*
                if (item.Experience < 5)
                {
                    ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Row(rowStart).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));

                }*/

                ws.Cells[string.Format("A{0}", rowStart)].Value = item.MaMon;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.TenMon;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.MaGV;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.Phong;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.TietBD;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.Thu;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.TH;
                ws.Cells[string.Format("H{0}", rowStart)].Value = item.ST;
                ws.Cells[string.Format("I{0}", rowStart)].Value = item.ThoiGianBD;
                ws.Cells[string.Format("J{0}", rowStart)].Value = item.ThoiGianKT;
                ws.Cells[string.Format("K{0}", rowStart)].Value = item.HocKy;
                ws.Cells[string.Format("L{0}", rowStart)].Value = item.MaLM;

                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "QLTKB.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();

        }

        public ActionResult QLD()
        {
            var tkuser = Convert.ToString(Session["TKadmin"]);
            if (string.IsNullOrWhiteSpace(tkuser))
            {
                return RedirectToAction("Login", "DangNhap");
            }
            //    List<DiemCTViews> emplist = db.DiemCTs.Select(x => new DiemCTViews
            //    {
            //        MaMon = x.MaMon,
            //        IDD = x.IDDiem,
            //        DiemGK = x.DiemGK,
            //        DiemCK = x.DiemCK,
            //        DTBMon = x.DiemTBM,
            //        NamHoc = x.NamHoc,
            //        HocKy = x.HocKy,
            //        KQ = x.KQ,
            //    }).ToList();
            List<DiemCT> DiemCTnames = db.DiemCTs.ToList();
            List<Diem> Diemnames = db.Diems.ToList();
            var maSV = (from dct in DiemCTnames
                        join d in Diemnames on dct.IDDiem equals d.IDDiem
                        select new DiemCTViews
                        {
                            MaSV = d.MaSV,
                            MaMon = dct.MaMon,
                            IDD = dct.IDDiem,
                            DiemGK = dct.DiemGK,
                            DiemCK = dct.DiemCK,
                            DTBMon = dct.DiemTBM,
                            NamHoc = dct.NamHoc,
                            HocKy = dct.HocKy,
                            KQ = dct.KQ,
                        }).ToList();
            return View(maSV);

        }
        [HttpPost]
        public ActionResult QLD(HttpPostedFileBase file)
        {
            string filePath = string.Empty;
            if (file != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                file.SaveAs(filePath);

                string conString = string.Empty;

                switch (extension)
                {
                    case ".xls": //Excel 97-03.
                        conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                        break;
                    case ".xlsx": //Excel 07 and above.
                        conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                        break;
                }

                DataTable dt = new DataTable();
                conString = string.Format(conString, filePath);

                using (OleDbConnection connExcel = new OleDbConnection(conString))
                {
                    using (OleDbCommand cmdExcel = new OleDbCommand())
                    {
                        using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                        {
                            cmdExcel.Connection = connExcel;

                            //Get the name of First Sheet.
                            connExcel.Open();
                            DataTable dtExcelSchema;
                            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            //string mamon = dtExcelSchema.Rows[3]
                            connExcel.Close();

                            //Read Data from First Sheet.
                            connExcel.Open();
                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                            odaExcel.SelectCommand = cmdExcel;
                            odaExcel.Fill(dt);
                            connExcel.Close();
                        }
                    }
                }

                conString = @"Server=LAPTOP-TUUBPQO5\SQLEXPRESS;Database=SoLienLacDT;Trusted_Connection=True;";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name.
                        sqlBulkCopy.DestinationTableName = "dbo.DiemCT";

                        // Map the Excel columns with that of the database table, this is optional but good if you do
                        // 
                        sqlBulkCopy.ColumnMappings.Add("IDDiem", "IDDiem");

                        sqlBulkCopy.ColumnMappings.Add("MaMon", "MaMon");
                        sqlBulkCopy.ColumnMappings.Add("DiemGK", "DiemGK");
                        sqlBulkCopy.ColumnMappings.Add("DiemCK", "DiemCK");
                        sqlBulkCopy.ColumnMappings.Add("DiemTBM", "DiemTBM");
                        sqlBulkCopy.ColumnMappings.Add("NamHoc", "NamHoc");
                        sqlBulkCopy.ColumnMappings.Add("HocKy", "HocKy");
                        sqlBulkCopy.ColumnMappings.Add("KQ", "KQ");

                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }
                }
            }
            //if the code reach here means everthing goes fine and excel data is imported into database
            ViewBag.Success = "File Imported and excel data saved into database";
            List<DiemCTViews> emplist = db.DiemCTs.Select(x => new DiemCTViews
            {
                MaMon = x.MaMon,
                IDD = x.IDDiem,
                DiemGK = x.DiemGK,
                DiemCK = x.DiemCK,
                DTBMon = x.DiemTBM,
                NamHoc = x.NamHoc,
                HocKy = x.HocKy,
                KQ = x.KQ,
            }).ToList();

            return View(emplist);

        }
        public void ExcelToDiemCT()
        {
            List<DiemCT> DiemCTnames = db.DiemCTs.ToList();
            List<Diem> Diemnames = db.Diems.ToList();
            var maSV = (from dct in DiemCTnames
                        join d in Diemnames on dct.IDDiem equals d.IDDiem
                        select new DiemCTViews
                        {
                            MaSV = d.MaSV,
                            MaMon = dct.MaMon,
                            IDD = dct.IDDiem,
                            DiemGK = dct.DiemGK,
                            DiemCK = dct.DiemCK,
                            DTBMon = dct.DiemTBM,
                            NamHoc = dct.NamHoc,
                            HocKy = dct.HocKy,
                            KQ = dct.KQ,
                        }).ToList();
            //List<DiemCTViews> emplist = db.DiemCTs.Select(x => new DiemCTViews
            //{
            //    MaMon = x.MaMon,
            //    IDD = x.IDDiem,
            //    MaSV =
            //    DiemGK = x.DiemGK,
            //    DiemCK = x.DiemCK,
            //    DTBMon = x.DiemTBM,
            //    NamHoc = x.NamHoc,
            //    HocKy = x.HocKy,
            //    KQ = x.KQ,
            //}).ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Bảng Điểm chỉ tiết";
            ws.Cells["B1"].Value = " ";

            ws.Cells["A2"].Value = "";
            ws.Cells["B2"].Value = "Phòng đào tạo";

            ws.Cells["A3"].Value = "Date";
            ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A6"].Value = "IDD";
            ws.Cells["B6"].Value = "MaSV";
            ws.Cells["C6"].Value = "MaMon";
            ws.Cells["D6"].Value = "DiemGK";
            ws.Cells["E6"].Value = "DiemCK";
            ws.Cells["F6"].Value = "DiemTBM";
            ws.Cells["G6"].Value = "NamHoc";
            ws.Cells["H6"].Value = "HocKy";
            ws.Cells["I6"].Value = "KQ";


            int rowStart = 7;
            foreach (var item in maSV)
            {   /*
                if (item.Experience < 5)
                {
                    ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Row(rowStart).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));

                }*/

                ws.Cells[string.Format("A{0}", rowStart)].Value = item.IDD;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.MaSV;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.MaMon;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.DiemGK;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.DiemCK;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.DTBMon;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.NamHoc;
                ws.Cells[string.Format("H{0}", rowStart)].Value = item.HocKy;
                ws.Cells[string.Format("I{0}", rowStart)].Value = item.KQ;

                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "DiemCT.xls");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();

        }
        public ActionResult QLLM()
        {
            var tkuser = Convert.ToString(Session["TKadmin"]);
            if (string.IsNullOrWhiteSpace(tkuser))
            {
                return RedirectToAction("Login", "DangNhap");
            }
            List<QLLM> emplist = db.LopMons.Select(x => new QLLM
            {
                MaLM = x.MaLM,
                MaMon = x.MaMon,
                Lop = x.Lop,

            }).ToList();
            //List<DiemCT> DiemCTnames = db.DiemCTs.ToList();
            //List<Diem> Diemnames = db.Diems.ToList();
            //var maSV = (from dct in DiemCTnames
            //            join d in Diemnames on dct.IDDiem equals d.IDDiem
            //            select new DiemCTViews
            //            {
            //                MaSV = d.MaSV,
            //                MaMon = dct.MaMon,
            //                IDD = dct.IDDiem,
            //                DiemGK = dct.DiemGK,
            //                DiemCK = dct.DiemCK,
            //                DTBMon = dct.DiemTBM,
            //                NamHoc = dct.NamHoc,
            //                HocKy = dct.HocKy,
            //                KQ = dct.KQ,
            //            }).ToList();
            return View(emplist);

        }
        [HttpPost]
        public ActionResult QLLM(HttpPostedFileBase file)
        {
            string filePath = string.Empty;
            if (file != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                file.SaveAs(filePath);

                string conString = string.Empty;

                switch (extension)
                {
                    case ".xls": //Excel 97-03.
                        conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                        break;
                    case ".xlsx": //Excel 07 and above.
                        conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                        break;
                }

                DataTable dt = new DataTable();
                conString = string.Format(conString, filePath);

                using (OleDbConnection connExcel = new OleDbConnection(conString))
                {
                    using (OleDbCommand cmdExcel = new OleDbCommand())
                    {
                        using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                        {
                            cmdExcel.Connection = connExcel;

                            //Get the name of First Sheet.
                            connExcel.Open();
                            DataTable dtExcelSchema;
                            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            connExcel.Close();

                            //Read Data from First Sheet.
                            connExcel.Open();
                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                            odaExcel.SelectCommand = cmdExcel;
                            odaExcel.Fill(dt);
                            connExcel.Close();
                        }
                    }
                }

                conString = @"Server=LAPTOP-TUUBPQO5\SQLEXPRESS;Database=SoLienLacDT;Trusted_Connection=True;";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name.
                        sqlBulkCopy.DestinationTableName = "dbo.LopMon";

                        // Map the Excel columns with that of the database table, this is optional but good if you do
                        // 
                        sqlBulkCopy.ColumnMappings.Add("MaLM", "MaLM");

                        sqlBulkCopy.ColumnMappings.Add("Lop", "Lop");
                        sqlBulkCopy.ColumnMappings.Add("MaMon", "MaMon");


                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }
                }
            }
            //if the code reach here means everthing goes fine and excel data is imported into database
            ViewBag.Success = "File Imported and excel data saved into database";
            List<DiemCTViews> emplist = db.DiemCTs.Select(x => new DiemCTViews
            {
                MaMon = x.MaMon,
                IDD = x.IDDiem,
                DiemGK = x.DiemGK,
                DiemCK = x.DiemCK,
                DTBMon = x.DiemTBM,
                NamHoc = x.NamHoc,
                HocKy = x.HocKy,
                KQ = x.KQ,
            }).ToList();

            return View(emplist);

        }
        public void ExcelToLLM()
        {
            List<QLLM> emplist = db.LopMons.Select(x => new QLLM
            {
                MaLM = x.MaLM,
                MaMon = x.MaMon,
                Lop = x.Lop,

            }).ToList();
            //List<DiemCTViews> emplist = db.DiemCTs.Select(x => new DiemCTViews
            //{
            //    MaMon = x.MaMon,
            //    IDD = x.IDDiem,
            //    MaSV =
            //    DiemGK = x.DiemGK,
            //    DiemCK = x.DiemCK,
            //    DTBMon = x.DiemTBM,
            //    NamHoc = x.NamHoc,
            //    HocKy = x.HocKy,
            //    KQ = x.KQ,
            //}).ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Bảng Lớp Môn";
            ws.Cells["B1"].Value = " ";

            ws.Cells["A2"].Value = "";
            ws.Cells["B2"].Value = "Phòng đào tạo";

            ws.Cells["A3"].Value = "Ngày xuất";
            ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A6"].Value = "MaLM";
            ws.Cells["B6"].Value = "Lop";
            ws.Cells["C6"].Value = "MaMon";


            int rowStart = 7;
            foreach (var item in emplist)
            {   /*
                if (item.Experience < 5)
                {
                    ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Row(rowStart).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));

                }*/

                ws.Cells[string.Format("A{0}", rowStart)].Value = item.MaLM;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.Lop;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.MaMon;

                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "LopMon.xls");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();

        }
        public ActionResult QLLop()
        {
            var tkuser = Convert.ToString(Session["TKadmin"]);
            if (string.IsNullOrWhiteSpace(tkuser))
            {
                return RedirectToAction("Login", "DangNhap");
            }
            List<SoLienLacDienTu.Models.LamQuenCodeFirst.Lop> emplist = db.Lops.Select(x => new SoLienLacDienTu.Models.LamQuenCodeFirst.Lop
            {
                Malop = x.Malop,
                MaGV = x.MaGV,

            }).ToList();
            //List<DiemCT> DiemCTnames = db.DiemCTs.ToList();
            //List<Diem> Diemnames = db.Diems.ToList();
            //var maSV = (from dct in DiemCTnames
            //            join d in Diemnames on dct.IDDiem equals d.IDDiem
            //            select new DiemCTViews
            //            {
            //                MaSV = d.MaSV,
            //                MaMon = dct.MaMon,
            //                IDD = dct.IDDiem,
            //                DiemGK = dct.DiemGK,
            //                DiemCK = dct.DiemCK,
            //                DTBMon = dct.DiemTBM,
            //                NamHoc = dct.NamHoc,
            //                HocKy = dct.HocKy,
            //                KQ = dct.KQ,
            //            }).ToList();
            return View(emplist);

        }
        [HttpPost]
        public ActionResult QLLop(HttpPostedFileBase file)
        {
            string filePath = string.Empty;
            if (file != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                file.SaveAs(filePath);

                string conString = string.Empty;

                switch (extension)
                {
                    case ".xls": //Excel 97-03.
                        conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                        break;
                    case ".xlsx": //Excel 07 and above.
                        conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                        break;
                }

                DataTable dt = new DataTable();
                conString = string.Format(conString, filePath);

                using (OleDbConnection connExcel = new OleDbConnection(conString))
                {
                    using (OleDbCommand cmdExcel = new OleDbCommand())
                    {
                        using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                        {
                            cmdExcel.Connection = connExcel;

                            //Get the name of First Sheet.
                            connExcel.Open();
                            DataTable dtExcelSchema;
                            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            connExcel.Close();

                            //Read Data from First Sheet.
                            connExcel.Open();
                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                            odaExcel.SelectCommand = cmdExcel;
                            odaExcel.Fill(dt);
                            connExcel.Close();
                        }
                    }
                }

                conString = @"Server=LAPTOP-TUUBPQO5\SQLEXPRESS;Database=SoLienLacDT;Trusted_Connection=True;";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name.
                        sqlBulkCopy.DestinationTableName = "dbo.Lop";

                        // Map the Excel columns with that of the database table, this is optional but good if you do
                        // 
                        sqlBulkCopy.ColumnMappings.Add("Malop", "Malop");

                        sqlBulkCopy.ColumnMappings.Add("MaGV", "MaGV");



                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }
                }
            }
            //if the code reach here means everthing goes fine and excel data is imported into database
            ViewBag.Success = "File Imported and excel data saved into database";
            List<Lop> emplist = db.Lops.Select(x => new Lop
            {
                MaGV = x.MaGV,
                Malop = x.Malop,

            }).ToList();

            return View(emplist);

        }
        public void ExcelToLop()
        {
            List<Lop> emplist = db.Lops.Select(x => new Lop
            {
                Malop = x.Malop,
                MaGV = x.MaGV,

            }).ToList();
            //List<DiemCTViews> emplist = db.DiemCTs.Select(x => new DiemCTViews
            //{
            //    MaMon = x.MaMon,
            //    IDD = x.IDDiem,
            //    MaSV =
            //    DiemGK = x.DiemGK,
            //    DiemCK = x.DiemCK,
            //    DTBMon = x.DiemTBM,
            //    NamHoc = x.NamHoc,
            //    HocKy = x.HocKy,
            //    KQ = x.KQ,
            //}).ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Bảng Lớp";
            ws.Cells["B1"].Value = " ";

            ws.Cells["A2"].Value = "";
            ws.Cells["B2"].Value = "Phòng đào tạo";

            ws.Cells["A3"].Value = "Ngày xuất";
            ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A6"].Value = "MaLop";
            ws.Cells["B6"].Value = "MaGV";



            int rowStart = 7;
            foreach (var item in emplist)
            {   /*
                if (item.Experience < 5)
                {
                    ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Row(rowStart).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));

                }*/

                ws.Cells[string.Format("A{0}", rowStart)].Value = item.Malop;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.MaGV;


                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "Lop.xls");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();

        }

        public ActionResult DSDONDK()
        {
            var tkuser = Convert.ToString(Session["TKadmin"]);
            if (string.IsNullOrWhiteSpace(tkuser))
            {
                return RedirectToAction("Login", "DangNhap");
            }
            var ddk = from p in db.Dons
                      select p;

            return View(ddk);
        }

        public ActionResult DangGiaDonDK(int id)
        {
            var DangGiaDonDK = db.Dons.First(m => m.STT == id);

            return View(DangGiaDonDK);
        }

        public ActionResult SendEMail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendEMail(gmail model, SoLienLacDienTu.Models.Admin ad)
        {
            int id = (int)Session["idDon"];
            var idad = Session["TKadmin"];

            List<Don> donname = db.Dons.ToList();
            List<Admin> Adminnames = db.Admins.ToList();
            foreach (var e in Adminnames)
            {
                if (e.TenAdmin == Convert.ToString(idad))
                {


                    MailMessage mm = new MailMessage(e.Email, model.To); /*"wolf230599@gmail.com"*/
                    mm.Subject = model.Subject;
                    mm.Body = model.Body;
                    mm.IsBodyHtml = false;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;

                    NetworkCredential nc = new NetworkCredential(e.Email, e.PasswordEmail);// tai khoan va mat khau email "wolf230599@gmail.com"
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = nc;
                    smtp.Send(mm);



                    ViewBag.mess = "Gửi mail thành công!";
                    foreach (var d in donname)
                    {
                        if (d.STT == id)
                        {
                            if (d.TrangThai == 1)
                            {
                                d.TrangThai = 2;
                                db.SubmitChanges();
                                return RedirectToAction("DSDONDK");
                            }
                            else
                            {
                                d.TrangThai = 1;
                                db.SubmitChanges();
                                return RedirectToAction("DSDONDK");
                            }
                        }
                    }
                    return View();
                }
                else
                {
                    ViewBag.mess = "Gửi mail khong thành công!";

                    return View();
                }

            }

            return View();
        }

        [HttpPost]
        /*   [ValidateAntiForgeryToken]*/

        public ActionResult DuocDuyet(gmail model, SoLienLacDienTu.Models.Admin ad, int idduyet, string tksv)
        {
            /* int id = (int)Session["idDon"];*/

            var idad = Session["TKadmin"];
            List<Don> donname = db.Dons.ToList();
            List<Admin> Adminnames = db.Admins.ToList();
            List<SinhVien> svnames = db.SinhViens.ToList();
            var duyetdon = (from s in donname
                            where s.STT == idduyet
                            select new DuyetDonHomeAdmin
                            {
                                STT = s.STT,
                                MaSV = s.MaSV,
                                TenSV = s.TenSV,
                                LiDo = s.LiDo,
                                TrangThai = s.TrangThai,
                            }).ToList();
           
            foreach (var e in donname)
            {
                if (e.STT == idduyet)
                {
                    e.TrangThai = 1;
                    db.SubmitChanges();
                }



            }
            foreach (var sv in svnames)
            {
                if (sv.MaSV == tksv)
                {
                    model.To = sv.Email;
                }
            }
            foreach (var e in Adminnames)
            {
                if (e.TenAdmin == Convert.ToString(idad))
                {


                    MailMessage mm = new MailMessage(e.Email, model.To);/*"wolf230599@gmail.com"*/
                    mm.Subject = "Email phản hồi về đơn đăng ký";
                    mm.Body = "Đơn của sinh viên đã được nhà trường duyệt sinh viên đợi nhà trường xếp lịch sẽ thông báo cho sinh viên qua email này";
                    mm.IsBodyHtml = false;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;

                    NetworkCredential nc = new NetworkCredential(e.Email, e.PasswordEmail);// tai khoan va mat khau email "wolf230599@gmail.com"
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = nc;
                    smtp.Send(mm);
                }
                else
                {
                }
            }
            ViewBag.mess = "Đã duyệt thành công!";
            return RedirectToAction("DSDONDK");
        }


        //còn chưa thay đổi được trang thái chỉ gửi email về phía sinh viên được thôi
        [HttpPost]

        public ActionResult KhongDuocDuyet(gmail model, SoLienLacDienTu.Models.Admin ad, int? idkoduyet, string tksvkdd, string LiDoKD, int? duyet)
        {
            //int id = (int)Session["idDon"];
            var idad = Session["TKadmin"];
            List<Don> donname = db.Dons.ToList();
            List<Admin> Adminnames = db.Admins.ToList();
            List<SinhVien> svnames = db.SinhViens.ToList();
            var khongduyetdon = (from s in donname
                                 where s.STT == duyet
                                 select new KhongDuyetDonHomeAdmin
                                 {
                                     STT = s.STT,
                                     MaSV = s.MaSV,
                                     TenSV = s.TenSV,
                                     LiDo = s.LiDo,
                                     TrangThai = 2,
                                 }).ToList();
            foreach (var e in donname)
            {
                if (e.STT == idkoduyet)
                {
                    e.TrangThai = 2;
                    db.SubmitChanges();
                }



            }
            foreach (var sv in svnames)
            {
                if (sv.MaSV == tksvkdd)
                {
                    model.To = sv.Email;
                }
            }
            foreach (var e in Adminnames)
            {
                if (e.TenAdmin == Convert.ToString(idad))
                {

                    //model.To = Convert.ToString(Session["emailsv"]);
                    MailMessage mm = new MailMessage(e.Email, model.To);/* "wolf230599@gmail.com"*/
                    mm.Subject = "Email phản hồi về đơn đăng ký";
                    mm.Body = LiDoKD;
                    mm.IsBodyHtml = false;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;

                    NetworkCredential nc = new NetworkCredential(e.Email, e.PasswordEmail);// tai khoan va mat khau email "wolf230599@gmail.com"
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = nc;
                    smtp.Send(mm);

                    ViewBag.mess = "Gửi mail thành công!";
                }
                else
                {
                    ViewBag.mess = "Gửi mail khong thành công!";
                }

            }

            ViewBag.mess = "không duyệt thành công!";
            return RedirectToAction("DSDONDK");
        }
        public ActionResult xetTotNghiep()
        {
            List<SinhVien> sinhViens = db.SinhViens.ToList();
            var tkuser = Convert.ToString(Session["TKadmin"]);
            if (string.IsNullOrWhiteSpace(tkuser))
            {
                return RedirectToAction("Login", "DangNhap");
            }
            foreach (var i in sinhViens)
            {
                if (string.IsNullOrEmpty(Convert.ToString(i.TotNghiep)))
                {
                    i.TotNghiep = 0;
                }
                else
                {
                }
            }
            db.SubmitChanges();
            return View(db.SinhViens.ToList());

        }

        [HttpPost]
        public ActionResult svTotNghiep(SinhVien lSV, string idduyet, string de)
        {
            List<SinhVien> sinhViens = db.SinhViens.ToList();
            List<Diem> diems = db.Diems.ToList();
            List<DiemCT> diemcts = db.DiemCTs.ToList();

            int tongtcd = 0;

            var layDiemSV = (from sv in sinhViens
                             where sv.MaSV == idduyet

                             join diem in diems on sv.MaSV equals diem.MaSV
                             join diemct in diemcts on diem.IDDiem equals diemct.IDDiem
                             where diem.MaSV == sv.MaSV && diem.SoTinChiDat != null
                             select new layMaMon
                             {
                                 IDDiem = diem.IDDiem,
                                 masv = diem.MaSV,
                                 mamon = diemct.MaMon,
                                 Namhoc = diemct.NamHoc,
                                 tinchidat = diem.SoTinChiDat
                             }).ToList(); ;

            foreach (var item in layDiemSV)
            {

                tongtcd += Convert.ToInt32(item.tinchidat);

            }
            ViewBag.ád = tongtcd;

            if (de == "Có")
            {
                foreach (SinhVien sv in sinhViens)
                {
                    if (sv.MaSV == idduyet)
                    {
                        if (tongtcd == 23)
                        {
                            sv.TotNghiep = 1;
                            Session["xet"] = "1";
                        }
                        else
                        {
                            Session["xet"] = "2";

                        }
                    }

                }
            }
            else
            {

            }
          
            db.SubmitChanges();
            return RedirectToAction("xetTotNghiep");

        }

   
        [HttpPost]
     
        public ActionResult addFile( HttpPostedFileBase file, int idSTT, string idsv)
        {
            /* int id = (int)Session["idDon"];*/
            var path = "";
            var filename = "";

            List<Don> donnames = db.Dons.ToList();
            // chua biet thông tin nàm ở đâu 
            foreach(var item in donnames)
            {
                if (item.STT == idSTT && item.MaSV == idsv)
                {

                    if (file != null && file.ContentLength > 0)
                    {
                        filename = file.FileName;
                        path = Path.Combine(Server.MapPath("~/UploadedFiles"), filename);
                        file.SaveAs(path);
                        item.FileDon = filename;

                    }

                }
                else
                {

                }

            }

            db.SubmitChanges();


            return RedirectToAction("DSDONDK");
        }

    }
}