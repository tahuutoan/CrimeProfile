﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using PagedList;
using Project4.Models;

namespace Project4.Controllers
{
    public class CheDoNghiPhepCuaQuanNgucsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: CheDoNghiPhepCuaQuanNgucs 
        public ActionResult Index(string tenQuanNguc, int? i)
        {
            var quanNgucs = db.QuanNguc.Include(p => p.Khu);
            if (!User.IsInRole("QuanNgucTruong"))
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            } 
            var khuList = db.Khu.ToList();
            ViewBag.KhuQuanLi = new SelectList(khuList, "ID", "ID"); 

            if (string.IsNullOrEmpty(tenQuanNguc)) tenQuanNguc = "";
            var nghiphepList = from c in db.CheDoNghiPhepCuaQuanNguc
                               join q in db.QuanNguc
                                    on c.QuanNgucID equals q.ID
                               where q.TenQuanNguc.Contains(tenQuanNguc)
                               select new NghiPhepQuanNgucParams
                               {
                                   ID = c.ID,
                                   TenQuanNguc = q.TenQuanNguc,
                                   SoNgayNghi = c.SoNgayNghi,
                                   LyDoNghi = c.LyDoNghi
                               };   
            return View(nghiphepList.Distinct().OrderBy(c => c.TenQuanNguc).ToPagedList(i ?? 1, 5)); 
        }

        // GET: CheDoNghiPhepCuaQuanNgucs/Details/5
        public ActionResult Details(int? id)
        {
            if (!User.IsInRole("QuanNgucTruong"))
            {
                return RedirectToAction("Index", "TrangChu");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheDoNghiPhepCuaQuanNguc cheDoNghiPhepCuaQuanNguc = db.CheDoNghiPhepCuaQuanNguc.Find(id);
            if (cheDoNghiPhepCuaQuanNguc == null)
            {
                return HttpNotFound();
            }
            return View(cheDoNghiPhepCuaQuanNguc);
        }

        public ActionResult PhongDamNhiemBoi(string idQuanNguc)
        {
            var quanNguc = db.QuanNguc.FirstOrDefault(w => w.ID.ToString() == idQuanNguc);
            if (quanNguc == null)
            {
                ViewBag.QuanNgucDamNhiem = db.QuanNguc;
            }
            else
            {
                ViewBag.QuanNgucDamNhiem = db.QuanNguc.Where(w => w.ID.ToString() != idQuanNguc);
            }
            ViewBag.Phong = db.PhongGiam.Where(w => w.KhuID == quanNguc.KhuID).ToList();
            return PartialView("_PhongDamNhiemBoi");
        }
        // GET: CheDoNghiPhepCuaQuanNgucs/Create
        public ActionResult Create()
        {
            if (!User.IsInRole("QuanNgucTruong"))
            {
                return RedirectToAction("Index", "TrangChu");
            }
            ViewBag.QuanNgucID = new SelectList(db.QuanNguc, "ID", "TenQuanNguc", null);
            var quanNgucDamNhiem = db.QuanNguc;
            ViewBag.QuanNgucDamNhiem = quanNgucDamNhiem.Where(w => w.ID != quanNgucDamNhiem.FirstOrDefault().ID);
            ViewBag.Phong = db.PhongGiam.Where(w => w.KhuID == quanNgucDamNhiem.FirstOrDefault().KhuID).ToList();
            return View();
        } 

        // POST: CheDoNghiPhepCuaQuanNgucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,QuanNgucID,SoNgayNghi,LyDoNghi")] CheDoNghiPhepCuaQuanNguc cheDoNghiPhepCuaQuanNguc, string[] phongName, string[] QuanNgucDamNhiem)
        {
            if (ModelState.IsValid)
            {
                db.CheDoNghiPhepCuaQuanNguc.Add(cheDoNghiPhepCuaQuanNguc);
                for (int i = 0; i < phongName.Length; i++)
                {
                    var newBanGiao = new BanGiaoPhamNhan();
                    var phong = phongName[i];
                    var phongId = db.PhongGiam.FirstOrDefault(f => f.TenPhong == phong).ID;
                    newBanGiao.PhongID = phongId;
                    newBanGiao.QuanNgucNghiID = cheDoNghiPhepCuaQuanNguc.QuanNgucID;
                    newBanGiao.QuanNgucNhanID = Guid.Parse(QuanNgucDamNhiem[i]);
                    newBanGiao.NgayNhan = DateTime.Now;
                    newBanGiao.SoNgayBanGiao = cheDoNghiPhepCuaQuanNguc.SoNgayNghi;
                    db.BanGiaoPhamNhan.Add(newBanGiao);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cheDoNghiPhepCuaQuanNguc);
        }

        // GET: CheDoNghiPhepCuaQuanNgucs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!User.IsInRole("QuanNgucTruong"))
            {
                return RedirectToAction("Index", "TrangChu");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheDoNghiPhepCuaQuanNguc cheDoNghiPhepCuaQuanNguc = db.CheDoNghiPhepCuaQuanNguc.Find(id);
            if (cheDoNghiPhepCuaQuanNguc == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuanNgucID = new SelectList(db.QuanNguc, "ID", "TenQuanNguc", cheDoNghiPhepCuaQuanNguc.QuanNgucID.ToString());
            ViewBag.QuanNgucDamNhiem = db.QuanNguc.Where(w => w.ID != cheDoNghiPhepCuaQuanNguc.QuanNgucID);
            var khuBanGiao = db.QuanNguc.Find(cheDoNghiPhepCuaQuanNguc.QuanNgucID).KhuID;
            ViewBag.Phong = db.PhongGiam.Where(w => w.KhuID == khuBanGiao).ToList();
            ViewBag.QuanNgucBanGiaoID = db.BanGiaoPhamNhan.Where(w => w.QuanNgucNghiID == cheDoNghiPhepCuaQuanNguc.QuanNgucID).Select(s => s.QuanNgucNhanID.ToString()).ToList();
            return View(cheDoNghiPhepCuaQuanNguc);
        }

        // POST: CheDoNghiPhepCuaQuanNgucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,QuanNgucID,SoNgayNghi,LyDoNghi")] CheDoNghiPhepCuaQuanNguc cheDoNghiPhepCuaQuanNguc, string[] phongName, string[] QuanNgucDamNhiem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cheDoNghiPhepCuaQuanNguc).State = EntityState.Modified;
                for (int i = 0; i < phongName.Length; i++)
                {
                    var phong = phongName[i];
                    var phongId = db.PhongGiam.FirstOrDefault(f => f.TenPhong == phong).ID;
                    var quanNgucNghi = cheDoNghiPhepCuaQuanNguc.QuanNgucID;
                    var editBanGiao = db.BanGiaoPhamNhan.FirstOrDefault(f => f.PhongID == phongId && f.QuanNgucNghiID == quanNgucNghi && DbFunctions.DiffDays(DateTime.Now, f.NgayNhan) <= f.SoNgayBanGiao);
                    editBanGiao.QuanNgucNhanID = Guid.Parse(QuanNgucDamNhiem[i]);
                    editBanGiao.SoNgayBanGiao = cheDoNghiPhepCuaQuanNguc.SoNgayNghi;
                    db.Entry(editBanGiao).State = EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cheDoNghiPhepCuaQuanNguc);
        }

        // GET: CheDoNghiPhepCuaQuanNgucs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!User.IsInRole("QuanNgucTruong"))
            {
                return RedirectToAction("Index", "TrangChu");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheDoNghiPhepCuaQuanNguc cheDoNghiPhepCuaQuanNguc = db.CheDoNghiPhepCuaQuanNguc.Find(id);
            if (cheDoNghiPhepCuaQuanNguc == null)
            {
                return HttpNotFound();
            }
            return View(cheDoNghiPhepCuaQuanNguc);
        }

        // POST: CheDoNghiPhepCuaQuanNgucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CheDoNghiPhepCuaQuanNguc cheDoNghiPhepCuaQuanNguc = db.CheDoNghiPhepCuaQuanNguc.Find(id);
            db.CheDoNghiPhepCuaQuanNguc.Remove(cheDoNghiPhepCuaQuanNguc);
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
    }
}
