using QuanLySDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLySDD.Controllers
{
    [Authorize]
    public class DSThanhToanController : Controller
    {
        // GET: DSThanhToan
        public ActionResult Index(FormCollection collections)
        {
            QLDDataContext context = new QLDDataContext();
            List<dsthanhtoan> dsdsthanhtoan = null;

            if (collections.Count == 0)
            {
                dsdsthanhtoan = context.dsthanhtoans.ToList();
            }
            /*else
            {
                String s = Request.QueryString["txtSearch"];
                dsdsthanhtoan = context.dsthanhtoans.Where(name => name.gioitinh == s).ToList();
            }*/
            return View(dsdsthanhtoan);
        }


        public ActionResult Details(int id)
        {
            QLDDataContext context = new QLDDataContext();
            dsthanhtoan ch = context.dsthanhtoans.FirstOrDefault(x => x.mathanhtoan == id); //Tìm x theo id rồi so sách với id của database
            return View(ch);
        }
        [Authorize(Users = "Admin,Admin1")]
        public ActionResult Create()
        {
            if (Request.Form.Count > 0)
            {
                //int mathanhtoan = int.Parse(Request.Form["mathanhtoan"]);
                String macanho = Request.Form["macanho"];
                String madienke = Request.Form["madienke"];
                String tinhtrang = Request.Form["tinhtrang"];

                QLDDataContext context = new QLDDataContext();

                dsthanhtoan dstt = new dsthanhtoan();
                //dstt.mathanhtoan = mathanhtoan;
                dstt.macanho = macanho;
                dstt.madienke = madienke;
                dstt.tinhtrang = tinhtrang;
                context.dsthanhtoans.InsertOnSubmit(dstt);
                context.SubmitChanges();
                return RedirectToAction("Index");

            }
            return View();
        }
        [Authorize(Users = "Admin,Admin1")]
        public ActionResult Edit(int id)
        {
            QLDDataContext context = new QLDDataContext();
            dsthanhtoan dstt = context.dsthanhtoans.FirstOrDefault(x => x.mathanhtoan == id); //Tìm x theo id rồi so sách với id của database
            if (Request.Form.Count == 0)
            {
                return View(dstt);
            }
            //int masudung = int.Parse(Request.Form["masudung"]);
            String macanho = Request.Form["macanho"];
            String madienke = Request.Form["madienke"];
            String tinhtrang = Request.Form["tinhtrang"];

            //sdd.masudung = masudung;
            dstt.macanho = macanho;
            dstt.madienke = madienke;
            dstt.tinhtrang = tinhtrang;
            context.SubmitChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Users = "Admin,Admin1")]
        public ActionResult Delete(int id)
        {
            QLDDataContext context = new QLDDataContext();
            dsthanhtoan dstt = context.dsthanhtoans.FirstOrDefault(x => x.mathanhtoan == id); //Tìm x theo id rồi so sách với id của database
            context.dsthanhtoans.DeleteOnSubmit(dstt);
            context.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}