using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCMMarketplace.Models;
//Created by :Alpa Bandekar
//Date: 05/1/2020
namespace UCMMarketplace.Controllers
{
    public class ActivityController : Controller
    {
        //Shows User Activity
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
    }
}
