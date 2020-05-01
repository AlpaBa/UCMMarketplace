using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using UCMMarketplace.Models;
//Controller for Login of existing User
//Code by:Alpa Bandekar  04/13/2020
namespace UCMMarketplace.Controllers
{
    public class LoginController : Controller
    {
        //Registration Action
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
            //return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(user user)
        {
           // bool LogStatus = false;
            if (ModelState.IsValid)
            {
                
                var validLogin = IsValidLogin(user.UserName, user.Password);
                if (validLogin == true)
                {
                    ViewBag.Status = true;
                    Session["UserName"] = user.UserName;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Status = false;
                    return View();
                }              
                //Email is already Exists
                //var isExist = IsEmailExist(user.UserName);
                //if (isExist)
                //{
                //    ModelState.AddModelError("AccountExist", "Account already exists");
                //    return View(user);
                //}
                ////password hashing
                //user.Password = Crypto.Hash(user.Password);
                ////save to databse
                //user.IsActive = false;
                //using (ucmmarketplaceEntities en = new ucmmarketplaceEntities())
                //{
                //    en.userprofiles.Add(user);
                //    en.SaveChanges();

                //}
            }
           return View();
        }
        //Registration POST action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude="UserId")]user user)
        {
            bool Status = false;
            string message = "";

            //Model Validation
            if (ModelState.IsValid)
            {
                //Email is already Exists
                var isExist = IsEmailExist(user.UserName);
                if (isExist)
                {
                    ModelState.AddModelError("AccountExist", "Account already exists");
                    return View(user);
                }
                //password hashing
                user.Password = Crypto.Hash(user.Password);
                //save to databse
               // user.IsActive = false;
                using (ucmmarketplaceEntities en = new ucmmarketplaceEntities())
                {
                    en.users.Add(user);
                    en.SaveChanges();
                   
                }
                //send email to user
                SendVerificationemail(user.EmailId);
                message = "Registration successful " + user.UserName;
                Status = true;
            }


            else
            {
                message = "Invalid Request";
            }
            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(user);
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            using (ucmmarketplaceEntities entity = new ucmmarketplaceEntities())
            {
                var v = entity.users.Where(a => a.EmailId == emailID).FirstOrDefault();
                return v != null;
            }
        }
        [NonAction]
        public bool IsValidLogin(string UserName, string Password)
        {
            using (ucmmarketplaceEntities entity = new ucmmarketplaceEntities())
            {
                string UserPassowrd= Crypto.Hash(Password);
                var v = entity.users.Where(a => a.UserName == UserName && a.Password == UserPassowrd).FirstOrDefault();
                if (v == null) { ViewBag.message = "Invalid Login Details. If not already registered, please Register."; }
                return v != null;
            }
        }
        [NonAction]
        public void SendVerificationemail(string emailID)
        {
            var fromEmail = new MailAddress("ucmmarketemailid@gmail.com", "Welcome to UCM Marketplace");
            var toEmail = new MailAddress(emailID);
            var fromEmailpwd = "Ucmproject_1";
            string subject = "Your UCM Marketplace account is created.";
            string body = "<br/><br/>Lets start selling and buying." +
                            "<br/><br/>";
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailpwd)
            };
            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }

    }
    
}
