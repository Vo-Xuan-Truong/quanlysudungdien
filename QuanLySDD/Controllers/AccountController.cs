using QuanLySDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QuanLySDD.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [Authorize(Users = "Admin,Admin1")]
        public ActionResult Index(FormCollection collections)
        {
            QLDDataContext context = new QLDDataContext();
            List<account> dsAccount = null;

            if (collections.Count == 0)
            {
                dsAccount = context.accounts.ToList();
            }
            else
            {
                String s = Request.QueryString["txtSearch"];
                dsAccount = context.accounts.Where(name => name.username == s).ToList();
            }
            return View(dsAccount);
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collections,account acc, String returnUrl)
        {
            String Username = collections["username"];
            String Password = collections["password"];
            QLDDataContext context = new QLDDataContext();
            account a = context.accounts.SingleOrDefault(x => x.username == Username && x.password == Password);
            if (ModelState.IsValid)
            {
                if (a != null)
                {
                    
                    FormsAuthentication.SetAuthCookie(acc.username, false);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user or password provided is incorrect.");
                }
            }
            return View(acc);
        }

        public ActionResult Register(FormCollection collections)
        {
            String Canho = collections["canho"];
            QLDDataContext context = new QLDDataContext();
            canho ch = context.canhos.SingleOrDefault(x => x.macanho == Canho);
            if (ch != null)
            {
                String Username = Request.Form["username"];
                String Tencanho = Request.Form["canho"];
                String Password = Request.Form["password"];
                String Role = "User";
                String RPPassword = Request.Form["rppassword"];
                if (Password.Equals(RPPassword))
                {
                    account acc = new account();
                    acc.username = Username;
                    acc.canho = Tencanho;
                    acc.password = Password;
                    acc.role = Role;
                    context.accounts.InsertOnSubmit(acc);
                    context.SubmitChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return Content("Mật khẩu không khớp, vui lòng nhập lại!!!");
                }
            }

            return View();
        }
        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account"); ;
        }
        [Authorize(Users = "Admin,Admin1")]
        public ActionResult Details(String id)
        {
            QLDDataContext context = new QLDDataContext();
            account acc = context.accounts.FirstOrDefault(x => x.username == id); //Tìm x theo id rồi so sádk với id của database
            return View(acc);
        }
        [Authorize(Users = "Admin,Admin1")]
        public ActionResult Create()
        {
            if (Request.Form.Count > 0)
            {
                String Username = Request.Form["username"];
                String Tencanho = Request.Form["canho"];
                String Password = Request.Form["password"];
                String Role = Request.Form["role"];

                QLDDataContext context = new QLDDataContext();

                account acc = new account();
                acc.username = Username;
                acc.canho = Tencanho;
                acc.password = Password;
                acc.role = Role;
                context.accounts.InsertOnSubmit(acc);
                context.SubmitChanges();
                return RedirectToAction("Index");

            }
            return View();
        }
        [Authorize(Users = "Admin,Admin1")]
        public ActionResult Edit(String id)
        {
            QLDDataContext context = new QLDDataContext();
            account acc = context.accounts.FirstOrDefault(x => x.username == id); //Tìm x theo id rồi so sádk với id của database
            if (Request.Form.Count == 0)
            {
                return View(acc);
            }
            String Username = Request.Form["username"];
            String Tencanho = Request.Form["canho"];
            String Password = Request.Form["password"];
            String Role = Request.Form["role"];

            acc.username = Username;
            acc.canho = Tencanho;
            acc.password = Password;
            acc.role = Role;
            context.SubmitChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Users = "Admin,Admin1")]
        public ActionResult Delete(String id)
        {
            QLDDataContext context = new QLDDataContext();
            account acc = context.accounts.FirstOrDefault(x => x.username == id); //Tìm x theo id rồi so sádk với id của database
            context.accounts.DeleteOnSubmit(acc);
            context.SubmitChanges();
            return RedirectToAction("Index");
        }

    }
}