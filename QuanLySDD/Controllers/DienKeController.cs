using QuanLySDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLySDD.Controllers
{   
    
    public class DienKeController : Controller
    {
        
        public ActionResult Index(FormCollection collections)
        {
            QLDDataContext context = new QLDDataContext();
            List<dienke> dsdienke = null;

            if (collections.Count == 0)
            {
                dsdienke = context.dienkes.ToList();
            }
            else
            {
                String s = collections["txtSearchdk"];
                dsdienke = context.dienkes.Where(name => name.madienke == s).ToList();
            }
            return View(dsdienke);
        }


        public ActionResult Details(String id)
        {
            QLDDataContext context = new QLDDataContext();
            dienke dk = context.dienkes.FirstOrDefault(x => x.madienke == id); //Tìm x theo id rồi so sádk với id của database
            return View(dk);
        }
        [Authorize(Users = "Admin,Admin1")]
        public ActionResult Create()
        {
            if (Request.Form.Count > 0)
            {
                String madienke = Request.Form["madienke"];
                String thang = Request.Form["thang"];
                String nam = Request.Form["nam"];
                float giadien = float.Parse(Request.Form["giadien"]);

                QLDDataContext context = new QLDDataContext();

                dienke dk = new dienke();
                dk.madienke = madienke;
                dk.thang = thang;
                dk.nam = nam;
                dk.giadien = giadien;
                context.dienkes.InsertOnSubmit(dk);
                context.SubmitChanges();
                return RedirectToAction("Index");

            }
            return View();
        }
        [Authorize(Users = "Admin,Admin1")]

        public ActionResult Edit(String id)
        {
            QLDDataContext context = new QLDDataContext();
            dienke dk = context.dienkes.FirstOrDefault(x => x.madienke == id); //Tìm x theo id rồi so sádk với id của database
            if (Request.Form.Count == 0)
            {
                return View(dk);
            }
            String madienke = Request.Form["madienke"];
            String thang = Request.Form["thang"];
            String nam = Request.Form["nam"];
            float giadien = float.Parse(Request.Form["giadien"]);

            dk.madienke = madienke;
            dk.thang = thang;
            dk.nam = nam;
            dk.giadien = giadien;
            context.SubmitChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Users = "Admin,Admin1")]
        public ActionResult Delete(String id)
        {
            QLDDataContext context = new QLDDataContext();
            dienke dk = context.dienkes.FirstOrDefault(x => x.madienke == id); //Tìm x theo id rồi so sádk với id của database
            context.dienkes.DeleteOnSubmit(dk);
            context.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}