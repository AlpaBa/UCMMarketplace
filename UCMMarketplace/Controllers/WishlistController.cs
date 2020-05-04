using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCMMarketplace.Models;

namespace UCMMarketplace.Controllers
{
    public class WishlistController : Controller
    {
        // GET: Wishlist
        [HttpGet]
        public ActionResult SaveItemtoWishlist(int itemid)
        {
            using (ucmmarketplaceEntities en = new ucmmarketplaceEntities())
            {
                wishlist objwishlist = new wishlist();
                if (Session["UserName"] != null || Session["UserName"].ToString() != "")
                {
                    int UserID;
                    var username = Session["UserName"].ToString();
                    UserID = en.users.Where(x => x.UserName == username).Select(x => x.UserId).FirstOrDefault();
                    objwishlist.UserID = UserID;
                    objwishlist.ItemID = itemid;
                    en.wishlists.Add(objwishlist);
                    en.SaveChanges();
                }

            }
            return View();
        }

        public ActionResult RemoveItemfromWishlist(int itemid)
        {
            using (ucmmarketplaceEntities en = new ucmmarketplaceEntities())
            {
                wishlist objwishlist = new wishlist();
                if (Session["UserName"] != null || Session["UserName"].ToString() != "")
                {
                    int UserID;
                    var username = Session["UserName"].ToString();
                    UserID = en.users.Where(x => x.UserName == username).Select(x => x.UserId).FirstOrDefault();
                    wishlist listDelete = en.wishlists.Find(UserID);
                    en.wishlists.Remove(listDelete);
                    en.SaveChanges();
                    objwishlist.UserID = UserID;
                    objwishlist.ItemID = itemid;
                    en.wishlists.Add(objwishlist);
                }
              
            }

            return View();
        }

   
    }
}
