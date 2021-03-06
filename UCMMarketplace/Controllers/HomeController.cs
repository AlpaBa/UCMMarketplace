﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCMMarketplace.Models;

namespace UCMMarketplace.Controllers
{
    public class HomeController : Controller
    {
        //Get : Item
        //[HttpGet]
        //public ActionResult Index()
        //{
        //    ucmmarketplaceEntities en = new ucmmarketplaceEntities();
        //    return View(en.items.ToList());
        //}

        //Get Item Data
        [HttpGet]
        public ActionResult Index(string search)
        {
            if (search == "")
            {
                ucmmarketplaceEntities en = new ucmmarketplaceEntities();
                return View(en.items.ToList());
            }
            else
            {
                ucmmarketplaceEntities en = new ucmmarketplaceEntities();
                return View(en.items.Where(x => x.Title.Contains(search) || search == null).ToList());
            }
        }
        //About Page
        public ActionResult About()
        {
            ViewBag.Message = "Buy-Sell-Go";

            return View();
        }
        //Contact Page
        public ActionResult Contact()
        {
            ViewBag.Message = "";

            return View();
        }
        // GET: Category
        [HttpGet]
        public PartialViewResult GetCategory()
        {
            ucmmarketplaceEntities en = new ucmmarketplaceEntities();
            return PartialView(en.categories.ToList());
        }
        // Get : Filter images by category
        [HttpGet]
        public PartialViewResult FilterbyCategory(int CategoryID)
        {

            using (ucmmarketplaceEntities en = new ucmmarketplaceEntities())
            {
                List<item> itemlists = en.items.ToList();
                List<Itemviewmodel> itemlist = itemlists.Select(x => new Itemviewmodel
                {
                    ItemID = x.ItemID,
                    Title = x.Title,
                    Description = x.Description,
                    Condition = x.Condition,
                    ImagePath = x.ImagePath,
                    CategoryID =x.CategoryID,
                    CategoryName = x.category.CategoryName,
                    Status = x.Status
                }).Where(x => x.CategoryID == CategoryID)
                  .OrderByDescending(x => x.ItemID)
                  .ToList();
                if (itemlist != null || itemlist.Count == 0 )
                {
                    return PartialView(itemlist);
                }else
                {
                    return PartialView(itemlists);
                }
            }
        }

    }
}