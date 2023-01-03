using QuanLySDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLySDD.Controllers
{
    [Authorize]
    public class CanHoController : Controller
    {
        // GET: CanHo
       
        public ActionResult Index(FormCollection collections)
        {
            QLDDataContext context = new QLDDataContext();
            List<canho> dsCanho = null;

            if (collections.Count == 0)
            {
                dsCanho = context.canhos.ToList();
            }
            else
            {
                String s = collections["txtSearch"];
                dsCanho = context.canhos.Where(name => name.macanho == s).ToList();
            }
            return View(dsCanho);
        }


        public ActionResult Details(String id)
        {
            QLDDataContext context = new QLDDataContext();
            canho ch = context.canhos.FirstOrDefault(x => x.macanho == id); //Tìm x theo id rồi so sách với id của database
            return View(ch);
        }
        [Authorize(Users = "Admin,Admin1")]
        public ActionResult Create()
        {
            if (Request.Form.Count > 0)
            {
                String macanho = Request.Form["macanho"];
                String tenchuho = Request.Form["tenchucanho"];
                String gioitinh = Request.Form["gioitinh"];
                String quequan = Request.Form["quequan"];
                String sodienthoai = Request.Form["sodienthoai"];
                String email = Request.Form["email"];
                String cccd = Request.Form["cccd"];

                QLDDataContext context = new QLDDataContext();

                canho ch = new canho();
                ch.macanho = macanho;
                ch.tenchucanho = tenchuho;
                ch.gioitinh = gioitinh;
                ch.quequan = quequan;
                ch.sodienthoai = sodienthoai;
                ch.email = email;
                ch.cccd = cccd;
                context.canhos.InsertOnSubmit(ch);
                context.SubmitChanges();
                return RedirectToAction("Index");

            }
            return View();
        }

        [Authorize(Users = "Admin,Admin1")]
        public ActionResult Edit(String id)
        {
            QLDDataContext context = new QLDDataContext();
            canho ch = context.canhos.FirstOrDefault(x => x.macanho == id); //Tìm x theo id rồi so sách với id của database
            if (Request.Form.Count == 0)
            {
                return View(ch);
            }
            String macanho = Request.Form["macanho"];
            String tenchuho = Request.Form["tenchucanho"];
            String gioitinh = Request.Form["gioitinh"];
            String quequan = Request.Form["quequan"];
            String sodienthoai = Request.Form["sodienthoai"];
            String email = Request.Form["email"];
            String cccd = Request.Form["cccd"];

            ch.macanho = macanho;
            ch.tenchucanho = tenchuho;
            ch.gioitinh = gioitinh;
            ch.quequan = quequan;
            ch.sodienthoai = sodienthoai;
            ch.email = email;
            ch.cccd = cccd;
            context.SubmitChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Users = "Admin,Admin1")]
        public ActionResult Delete(String id)
        {
            QLDDataContext context = new QLDDataContext();
            canho ch = context.canhos.FirstOrDefault(x => x.macanho == id); //Tìm x theo id rồi so sách với id của database
            context.canhos.DeleteOnSubmit(ch);
            context.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}