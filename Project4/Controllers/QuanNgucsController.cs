using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Project4.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Text.RegularExpressions;

namespace Project4.Controllers
{
    public class QuanNgucsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: QuanNgucs 
        public ActionResult Index(string tenQuanNguc, string khuID, int? i)
        {
            var quanNgucs = db.QuanNguc.Include(p => p.Khu);
            if (string.IsNullOrEmpty(tenQuanNguc)) tenQuanNguc = "";
            ViewBag.Khu = db.Khu.ToList();
            int _khuid = 0; 
            if (string.IsNullOrEmpty(khuID)) _khuid = db.Khu.FirstOrDefault().ID; 
            else _khuid = Convert.ToInt32(khuID);
            if (!User.IsInRole("QuanNgucTruong"))
            { 
                return RedirectToAction("Index", "TrangChu");
            }
            else
            {
                var idQuanNgucTruong = Guid.Parse(User.Identity.GetQuanNgucId());
                var quanNgucTruong = db.QuanNguc.Where(q => q.ID == idQuanNgucTruong);

                quanNgucs = quanNgucs
                    .Where(q => q.TenQuanNguc.Contains(tenQuanNguc) && q.KhuID == _khuid)
                    .Except(quanNgucTruong); 
            }
            int pageSize = 5;
            int pageNumber = (i ?? 1);
            ViewBag.ChucVu = Common.CommonConstant.chucVu;
            ViewBag.QuanHam = Common.CommonConstant.quanHam;
            return View(quanNgucs.OrderBy(q => q.TenQuanNguc).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ViewDetails(Guid id)
        {
            QuanNguc quanNguc;
            quanNguc = db.QuanNguc.Find(id);
            return PartialView("_Details", quanNguc);
        }

        public ActionResult TimKiem(string txtTenHoacMa, string khuID)
        {
            IQueryable<QuanNguc> listQuanNguc = db.QuanNguc;
            if (txtTenHoacMa.Length == 5)
            {
                //listPhamNhan = db.PhamNhan.Where(w => w. == txtTenOrMa);
            }
            else
            {
                listQuanNguc = listQuanNguc.Where(w => w.TenQuanNguc.Contains(txtTenHoacMa));
            }
            var IDkhu = int.Parse(khuID);
            listQuanNguc = listQuanNguc.Where(w => w.KhuID == IDkhu);

            return PartialView("_QuanNgucsDataTable", listQuanNguc);
        }
        // GET: QuanNgucs/Details/5 
        public ActionResult Details(Guid? id)
        {
            ViewBag.GioiTinh = Common.CommonConstant.gioiTinh;
            ViewBag.ChucVu = Common.CommonConstant.chucVu;
            ViewBag.QuanHam = Common.CommonConstant.quanHam;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuanNguc quanNguc = db.QuanNguc.Find(id);
            if (quanNguc == null)
            {
                return HttpNotFound();
            }
            return View(quanNguc);
        }

        // GET: QuanNgucs/Create
        public ActionResult Create()
        {
            if (!User.IsInRole("QuanNgucTruong"))
            {
                return RedirectToAction("Index", "TrangChu");
            }
            ViewBag.GioiTinh = new SelectList(Common.CommonConstant.gioiTinh, "Key", "Value", null);
            ViewBag.KhuID = new SelectList(db.Khu, "ID", "TenKhu", null);
            ViewBag.ChucVu = new SelectList(Common.CommonConstant.chucVu, "Key", "Value", null);
            ViewBag.QuanHam = new SelectList(Common.CommonConstant.quanHam, "Key", "Value", null);
            return View();
        }

        // POST: QuanNgucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenQuanNguc,AnhNhanDien,NgaySinh,QueQuan,GioiTinh,KhuID,NamCongTac,ThoiHanCongTac,CMND,ChucVu,QuanHam")] QuanNguc quanNguc, HttpPostedFileBase file)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            if (ModelState.IsValid)
            {
                //quanNguc.GioiTinh = 
                quanNguc.ID = Guid.NewGuid();
                string url = string.Empty;
                quanNguc.ID = Guid.NewGuid();
                quanNguc.NamCongTac = DateTime.Now;
                FileUpload(file, quanNguc.ID, out url);
                if (url == string.Empty)
                {
                    //error = "You must include a Featured Image for event.";
                    //ViewBag.Error = error;
                    ViewBag.NgaySinh = quanNguc.NgaySinh.HasValue ? quanNguc.NgaySinh.Value.ToString("MM/dd/yyyy") : null;
                    ViewBag.ThoiHanCongTac = quanNguc.ThoiHanCongTac.HasValue ? quanNguc.ThoiHanCongTac.Value.ToString("MM/dd/yyyy") : null;
                    return View(quanNguc);
                }
                quanNguc.AnhNhanDien = url;
                db.QuanNguc.Add(quanNguc);
                db.SaveChanges();
                var newUser = new ApplicationUser();
                var generatedEmail = RemoveUnicodeAndSpace(quanNguc.TenQuanNguc) + @"@chihoa.com";
                newUser.UserName = generatedEmail;
                newUser.Email = generatedEmail;
                newUser.QuanNgucID = quanNguc.ID;
                string password = "123456";
                var checkUser = UserManager.Create(newUser, password);
                if (checkUser.Succeeded)
                {
                    var checkRole = UserManager.AddToRole(newUser.Id, "QuanNguc");
                }
<<<<<<< HEAD

=======
>>>>>>> 8735bbc2b274040065dd3811d0d181406537f57c
                if (db.QuanNguc.Where(w => w.KhuID == quanNguc.KhuID).Count() > 1)
                {
                    var idPhongCuaKhu = db.PhongGiam.Where(w => w.KhuID == quanNguc.KhuID).Select(s => s.ID).ToList();
                    var phongDangBanGiao = db.BanGiaoCongViecCuaQuanNgucNghi.Where(w => idPhongCuaKhu.Contains(w.PhongID.Value));
                    if (phongDangBanGiao != null)
                    {
                        foreach (var item in phongDangBanGiao)
                        {
                            db.BanGiaoCongViecCuaQuanNgucNghi.Remove(item);
                        }
                    }
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.GioiTinh = new SelectList(Common.CommonConstant.gioiTinh, "Key", "Value", null);
            ViewBag.KhuID = new SelectList(db.Khu, "ID", "TenKhu", null);
            ViewBag.ChucVu = new SelectList(Common.CommonConstant.chucVu, "Key", "Value", null);
            ViewBag.QuanHam = new SelectList(Common.CommonConstant.quanHam, "Key", "Value", null);
            return View(quanNguc);
        }

        // GET: QuanNgucs/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (!User.IsInRole("QuanNgucTruong"))
            {
                return RedirectToAction("Index", "TrangChu");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuanNguc quanNguc = db.QuanNguc.Find(id);
            if (quanNguc == null)
            {
                return HttpNotFound();
            }
            ViewBag.GioiTinh = new SelectList(Common.CommonConstant.gioiTinh, "Key", "Value", quanNguc.GioiTinh);
            ViewBag.KhuID = new SelectList(db.Khu, "ID", "TenKhu", quanNguc.KhuID);
            ViewBag.ChucVu = new SelectList(Common.CommonConstant.chucVu, "Key", "Value", quanNguc.ChucVu);
            ViewBag.QuanHam = new SelectList(Common.CommonConstant.quanHam, "Key", "Value", quanNguc.QuanHam);
            ViewBag.NgaySinh = quanNguc.NgaySinh.HasValue ? quanNguc.NgaySinh.Value.ToString("MM/dd/yyyy") : null;
            ViewBag.ThoiHanCongTac = quanNguc.ThoiHanCongTac.HasValue ? quanNguc.ThoiHanCongTac.Value.ToString("MM/dd/yyyy") : null;
            return View(quanNguc);
        }

        // POST: QuanNgucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenQuanNguc,AnhNhanDien,NgaySinh,QueQuan,GioiTinh,KhuID,NamCongTac,ThoiHanCongTac,CMND,ChucVu,QuanHam")] QuanNguc quanNguc, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string url = string.Empty;
                FileUpload(file, quanNguc.ID, out url);
                if (url != string.Empty)
                {
                    quanNguc.AnhNhanDien = url;
                }
                db.Entry(quanNguc).State = EntityState.Modified;
                if (db.QuanNguc.Where(w => w.KhuID == quanNguc.KhuID).Count() > 1)
                {
                    var idPhongCuaKhu = db.PhongGiam.Where(w => w.KhuID == quanNguc.KhuID).Select(s => s.ID).ToList();
                    var phongDangBanGiao = db.BanGiaoCongViecCuaQuanNgucNghi.Where(w => idPhongCuaKhu.Contains(w.PhongID.Value));
                    if (phongDangBanGiao != null)
                    {
                        foreach (var item in phongDangBanGiao)
                        {
                            db.BanGiaoCongViecCuaQuanNgucNghi.Remove(item);
                        }
                    }
                    db.SaveChanges();
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NgaySinh = quanNguc.NgaySinh.HasValue ? quanNguc.NgaySinh.Value.ToString("MM/dd/yyyy") : null;
            ViewBag.ThoiHanCongTac = quanNguc.ThoiHanCongTac.HasValue ? quanNguc.ThoiHanCongTac.Value.ToString("MM/dd/yyyy") : null;
            return View(quanNguc);
        }

        public ActionResult Delete(Guid? id)
        {
            if (!User.IsInRole("QuanNgucTruong"))
            {
                return RedirectToAction("Index", "TrangChu");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuanNguc quanNguc = db.QuanNguc.Find(id);
            if (quanNguc == null)
            {
                return HttpNotFound();
            }
            var quanNgucDamNhiem = db.QuanNguc;
<<<<<<< HEAD

            if (db.BanGiaoCongViecCuaQuanNgucNghi.Where(w => w.QuanNgucNhanID == id).Any())
            {
                ViewBag.Message = "Quản ngục đang được bàn giao không thể xóa cho đến khi không còn phòng được bàn giao";
                return RedirectToAction("Index");
            }
            if (db.BanGiaoPhamNhan.Where(w => w.QuanNgucNhanID == id).Any())
            {
                ViewBag.Message = "Quản ngục đang được bàn giao không thể xóa cho đến khi hết thời hạn bàn giao";
                return RedirectToAction("Index");
            }

=======
>>>>>>> 8735bbc2b274040065dd3811d0d181406537f57c
            ViewBag.QuanNgucDamNhiem = quanNgucDamNhiem.Where(w => w.ID != quanNgucDamNhiem.FirstOrDefault().ID);
            ViewBag.Phong = db.PhongGiam.Where(w => w.KhuID == quanNgucDamNhiem.FirstOrDefault().KhuID).ToList();
            return View(quanNguc);
        }

        // POST: QuanNgucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id, string[] phongName, string[] QuanNgucDamNhiem)
        {
            QuanNguc quanNguc = db.QuanNguc.Find(id);
            db.QuanNguc.Remove(quanNguc);
            for (int i = 0; i < phongName.Length; i++)
            {
                var newBanGiao = new BanGiaoCongViecCuaQuanNgucNghi();
                var phong = phongName[i];
                var phongId = db.PhongGiam.FirstOrDefault(f => f.TenPhong == phong).ID;
                newBanGiao.PhongID = phongId;
                newBanGiao.QuanNgucNhanID = Guid.Parse(QuanNgucDamNhiem[i]);
                newBanGiao.NgayNhan = DateTime.Now;
                db.BanGiaoCongViecCuaQuanNgucNghi.Add(newBanGiao);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public void FileUpload(HttpPostedFileBase file, Guid quanNgucID, out string url)
        {
            url = string.Empty;
            try
            {
                if (file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    DirectoryInfo di = Directory.CreateDirectory(Server.MapPath($"~\\Common\\Image\\QuanNgucs\\{quanNgucID}"));
                    string path = System.IO.Path.Combine(
                                           Server.MapPath($"~/Common/Image/QuanNgucs/{quanNgucID}"), pic);
                    url = $"~/Common/Image/QuanNgucs/{quanNgucID}/" + pic;
                    // file is uploaded
                    file.SaveAs(path);

                    // save the image path path to the database or you can send image 
                    // directly to database
                    // in-case if you want to store byte[] ie. for DB
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }

                }
            }
            catch (Exception ex)
            {

            }

            // after successfully uploading redirect the user
            //return RedirectToAction("actionname", "controller name");
        }

        public static string RemoveUnicodeAndSpace(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
            "đ",
            "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
            "í","ì","ỉ","ĩ","ị",
            "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
            "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
            "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
            "d",
            "e","e","e","e","e","e","e","e","e","e","e",
            "i","i","i","i","i",
            "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
            "u","u","u","u","u","u","u","u","u","u","u",
            "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            text = Regex.Replace(text, @"\s", "");
            text = text.ToLower();
            return text;
        }
    }
}
