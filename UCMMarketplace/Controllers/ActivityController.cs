using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCMMarketplace.Models;

namespace UCMMarketplace.Controllers
{
    public class ActivityController : Controller
    {
        [HttpGet]
        public ActionResult ShowActivity()
        {
            ucmmarketplaceEntities en = new ucmmarketplaceEntities();
            List<item> itemlists = en.items.ToList();

            if (Session["UserName"] != null)
            {
                var username = Session["UserName"];
                var UserID = en.users.Where(x => x.UserName == username.ToString()).Select(x => x.UserId).FirstOrDefault();
                ViewBag.UserId = UserID;
                List<Itemviewmodel> itemlist = itemlists.Select(x => new Itemviewmodel
                {
                    ItemID = x.ItemID,
                    Title = x.Title,
                    Price = x.Price,
                    Description = x.Description,
                    Condition = x.Condition,
                    ImagePath = x.ImagePath,
                    CategoryName = x.category.CategoryName,
                    Status = x.Status,
                    UploadUserID = x.UploadUserID
                }).Where(x => x.UploadUserID == UserID).ToList();
                return View(itemlist);
            }
            else
            {
                return View();
            }
           
            
        }

        // GET: Acitivity/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Acitivity/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Acitivity/Create
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

        // GET: Acitivity/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Acitivity/Edit/5
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

        // GET: Acitivity/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Acitivity/Delete/5
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
    }
}
