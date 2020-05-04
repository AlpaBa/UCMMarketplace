using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCMMarketplace.Models;
using System.IO;
using System.Data.Entity;
//Controller for Item tasks such as Add,Edit,View
//Code by Alpa Bandekar 04/18/2020
namespace UCMMarketplace.Controllers
{
    public class ItemController : Controller
    {
        //Get: assign Category and Condition values list of item to the drodown
        [HttpGet]
        public ActionResult Add()
        {
            using (ucmmarketplaceEntities itemimage = new ucmmarketplaceEntities())
            {
                var categorydatalist = itemimage.categories.ToList();
                SelectList catlist = new SelectList(categorydatalist, "CategoryID", "CategoryName");
                ViewBag.categoryname = catlist;
                List<SelectListItem> Condition = new List<SelectListItem>()
                {
                    new SelectListItem() { Value = "New", Text = "New", /*Selected = true, Disabled = true*/ },
                    new SelectListItem() { Value = "BarelyUsed", Text = "BarelyUsed" },
                    new SelectListItem() { Value = "Used", Text = "Used" },
                };
                ViewBag.ItemCondition = Condition;
                return View();
            }
        }
        //Post: an item for sell
        [HttpPost]
        public ActionResult Add(item item)
        {
            int UserID;
            var username = Session["UserName"].ToString();
            string imagefilename = "";
            ucmmarketplaceEntities en = new ucmmarketplaceEntities();
            UserID = en.users.Where(x => x.UserName == username).Select(x => x.UserId).FirstOrDefault();
            foreach (var file in item.Imagefile)
            {
                string filename = Path.GetFileNameWithoutExtension(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                if (imagefilename == "")
                {
                    imagefilename = "~/ItemImages/" + filename;
                }
                else
                {
                    imagefilename = imagefilename + "|" + "~/ItemImages/" + filename;
                }
                filename = Path.Combine(Server.MapPath("~/ItemImages/"), filename);
                file.SaveAs(filename);
            }
                using (ucmmarketplaceEntities itemimage = new ucmmarketplaceEntities())
                {
                    if (imagefilename != "") { item.ImagePath = imagefilename; }
                    item.Status = "Available";
                    item.Condition = item.ItemCond;
                    item.Price = Convert.ToDouble(item.Price);
                    item.CategoryID = Convert.ToInt32(item.CategoryList);
                    item.UploadUserID = Convert.ToInt32(UserID); 
                    itemimage.items.Add(item);
                    itemimage.SaveChanges();

                }
               
            
            ModelState.Clear();
            return RedirectToAction("Index", "Home");

        }
        //Get: View an item
        [HttpGet]
        public ActionResult View(int id)
        {
            item itemimage = new item();

            using (ucmmarketplaceEntities en = new ucmmarketplaceEntities())
            {

                List<item> itemlists = en.items.ToList();
                if (Session["UserName"] != null)
                { 
                var username = Session["UserName"];
                var UserID = en.users.Where(x => x.UserName == username.ToString()).Select(x => x.UserId).FirstOrDefault();
                ViewBag.UserId = UserID;
                 }
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
                }).Where(x => x.ItemID == id).ToList();
                return View(itemlist);
            }

        }
        //Edit an item Assign condition and status dropdown
        [HttpGet]
        public ActionResult Edit(int id)
        {
            item itemimage = new item();
            using (ucmmarketplaceEntities en = new ucmmarketplaceEntities())

            {
                itemimage = en.items.Where(x => x.ItemID == id).FirstOrDefault();

            }
            List<SelectListItem> Condition = new List<SelectListItem>()
                {
                    new SelectListItem() { Value = "New", Text = "New", /*Selected = true, Disabled = true*/ },
                    new SelectListItem() { Value = "BarelyUsed", Text = "BarelyUsed" },
                    new SelectListItem() { Value = "Used", Text = "Used" },
                };
            ViewBag.ItemCondition = Condition;
            List<SelectListItem> ItemStatus = new List<SelectListItem>()
                {
                    new SelectListItem() { Value = "Available", Text = "Available", /*Selected = true, Disabled = true*/ },
                    new SelectListItem() { Value = "Sold", Text = "Sold" },
                    new SelectListItem() { Value = "PendingForPickup", Text = "PendingForPickup" },
                };
            ViewBag.ItemStatus = ItemStatus;

            return View(itemimage);
        }
        //Post: Edit an item
        [HttpPost]
        public ActionResult Edit(item item)
        {
            item itemimage = new item();

            //category cg = new category();
            //cg.categoryList = new SelectList(cg.GetCategoryList(), "CategoryID", "CateoryName");
            using (ucmmarketplaceEntities en = new ucmmarketplaceEntities())
            {

                int UserID;
                var username = Session["UserName"];
                UserID = en.users.Where(x => x.UserName == username.ToString()).Select(x => x.UserId).FirstOrDefault();
                itemimage = en.items.Where(x => x.ItemID == item.ItemID && x.UploadUserID == UserID).FirstOrDefault();
                string imagefilename="";
                string filename1 = "";
                foreach (var file in item.Imagefile)
                {
                    if (file != null)
                    {
                        
                            string filename = Path.GetFileNameWithoutExtension(file.FileName);
                            string extension = Path.GetExtension(file.FileName);
                            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                            // item.ImagePath = "~/ItemImages/" + filename;
                            filename1 = Path.Combine(Server.MapPath("~/ItemImages/"), filename);
                            file.SaveAs(filename1);
                            if (imagefilename == "")
                            {
                                imagefilename = "~/ItemImages/" + filename;
                            }
                            else
                            {
                                imagefilename = imagefilename + "|" + "~/ItemImages/" + filename;
                            }
                        
                    }
                }
                if (ModelState.IsValid)
                {
                    itemimage.Title = item.Title;
                    if (imagefilename == null || imagefilename=="") { 
                        itemimage.ImagePath = itemimage.ImagePath; 
                    } else {
                        itemimage.ImagePath = imagefilename; 
                    }
                    
                    itemimage.Description = item.Description;
                    if (item.Status != "" || item.Status != null)
                    {
                        itemimage.Status = item.Status;
                    }
                    else
                    {
                        itemimage.Status = itemimage.Status;
                    }
                    
                    if (item.Condition != "" || item.Condition != null)
                        {
                            itemimage.Condition = item.Condition;
                        }
                    else
                        {
                            itemimage.Condition = itemimage.Condition;
                        }
                    itemimage.Price = item.Price;
                    itemimage.CategoryID = item.CategoryID;//Convert.ToInt32(cg.categoryList.SelectedValue);
                    itemimage.UploadUserID = UserID;
                    itemimage.Imagefile = item.Imagefile;
                    en.SaveChanges();

                }
                    ModelState.Clear();
                    return RedirectToAction("View", new { id = itemimage.ItemID });
                
            }
        
        }


    }
}
