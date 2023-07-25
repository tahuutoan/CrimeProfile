using Project4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project4.Common;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace Project4.Controllers
{
    public class TaiKhoanController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult DangNhap()
        {
            if (User.Identity.GetQuanNgucId() != "")
            {
                return RedirectToAction("Index", "TrangChu");
            }
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(TaiKhoan tk)
        {
            if (tk.TenDangNhap == "streamer" && tk.MatKhau == "streamer123")
            {
                return RedirectToAction("Index", "TrangChu");
            }
            else
            {
                ViewBag.CheckValid = "Thông tin đăng nhập không hợp lệ";
                return View(tk);
            }

        }

        public ActionResult ThongTinTaiKhoan()
        {
            var _id = User.Identity.GetQuanNgucId();
            var quanNguc = db.QuanNguc.Find(Guid.Parse(_id));

            QuanNgucprTessssstt _quanNguc = new QuanNgucprTessssstt();
            _quanNguc.ID = quanNguc.ID;
            _quanNguc.TenQuanNguc = quanNguc.TenQuanNguc;
            _quanNguc.NgaySinh = quanNguc.NgaySinh;
            _quanNguc.QueQuan = quanNguc.QueQuan;
            _quanNguc.GioiTinh = quanNguc.GioiTinh;
            _quanNguc.KhuID = db.Khu.Where(k => k.ID == quanNguc.KhuID).Select(k => k.TenKhu).FirstOrDefault();
            _quanNguc.NamCongTac = quanNguc.NamCongTac;
            _quanNguc.ThoiHanCongTac = quanNguc.ThoiHanCongTac;
            _quanNguc.CMND = quanNguc.CMND;
            _quanNguc.ChucVu = CommonConstant.chucVu[quanNguc.ChucVu];
            _quanNguc.QuanHam = CommonConstant.quanHam[quanNguc.QuanHam];
            _quanNguc.AnhNhanDien = quanNguc.AnhNhanDien;

            return View(_quanNguc);
        }

        public ActionResult DoiMatkhau()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoiMatkhau(DoiMatKhauParams matKhau)
        {
            ViewBag.IsValid = "";
            if (matKhau.MatKhauMoi != matKhau.XacNhanMatKhauMoi)
            {
                ViewBag.IsValid = "Mật khẩu mới không trùng khớp với xác nhận mật khẩu";
                return View(); 
            }
            try
            {
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var result = UserManager.ChangePassword(User.Identity.GetUserId(), matKhau.MatKhauHienTai, matKhau.MatKhauMoi);
                if (result.Succeeded)
                {
                    ViewBag.IsValid = "Đổi mật khẩu thành công";
                }
                else
                {
                    ViewBag.IsValid = "Mật khẩu hiện tại không chính xác";
                }
            }
            catch
            {
                ViewBag.IsValid = "Mật khẩu hiện tại không chính xác";
            }


            return View();
        }
    }


}
