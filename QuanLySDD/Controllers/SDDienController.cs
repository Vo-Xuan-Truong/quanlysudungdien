using QuanLySDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLySDD.Controllers
{
    [Authorize]
    public class SDDienController : Controller
    {
        // GET: SDDien
        public ActionResult Index(FormCollection collections)
        {
            QLDDataContext context = new QLDDataContext();
            List<sudungdien> dssudungdien = null;

            if (collections.Count == 0)
            {
                dssudungdien = context.sudungdiens.ToList();
            }
            /*else
            {
                String s = Request.QueryString["txtSearch"];
                dssudungdien = context.sudungdiens.Where(name => name.gioitinh == s).ToList();
            }*/
            return View(dssudungdien);
        }


        public ActionResult Details(int id)
        {
            QLDDataContext context = new QLDDataContext();
            sudungdien ch = context.sudungdiens.FirstOrDefault(x => x.masudung == id); //Tìm x theo id rồi so sách với id của database
            return View(ch);
        }
        [Authorize(Users = "Admin,Admin1")]
        public ActionResult Create()
        {
            if (Request.Form.Count > 0)
            {
                int masudung = int.Parse(Request.Form["masudung"]);
                String canho = Request.Form["canho"];
                String madienke = Request.Form["madienke"];
                float soluongdien = float.Parse(Request.Form["soluongdien"]);

                QLDDataContext context = new QLDDataContext();

                sudungdien sdd = new sudungdien();
                sdd.masudung = masudung;
                sdd.canho = canho;
                sdd.madienke = madienke;
                sdd.soluongdien = soluongdien;
                context.sudungdiens.InsertOnSubmit(sdd);
                context.SubmitChanges();
                return RedirectToAction("Index");

            }
            return View();
        }
        [Authorize(Users = "Admin,Admin1")]
        public ActionResult Edit(int id)
        {
            QLDDataContext context = new QLDDataContext();
            sudungdien sdd = context.sudungdiens.FirstOrDefault(x => x.masudung == id); //Tìm x theo id rồi so sách với id của database
            if (Request.Form.Count == 0)
            {
                return View(sdd);
            }
            int masudung = int.Parse(Request.Form["masudung"]);
            String canho = Request.Form["canho"];
            String madienke = Request.Form["madienke"];
            float soluongdien = float.Parse(Request.Form["soluongdien"]);

            sdd.masudung = masudung;
            sdd.canho = canho;
            sdd.madienke = madienke;
            sdd.soluongdien = soluongdien;
            context.SubmitChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Users = "Admin,Admin1")]
        public ActionResult Delete(int id)
        {
            QLDDataContext context = new QLDDataContext();
            sudungdien ch = context.sudungdiens.FirstOrDefault(x => x.masudung == id); //Tìm x theo id rồi so sách với id của database
            context.sudungdiens.DeleteOnSubmit(ch);
            context.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}