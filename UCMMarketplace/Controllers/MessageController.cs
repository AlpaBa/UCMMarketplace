using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using UCMMarketplace.Models;

namespace UCMMarketplace.Controllers
{
    public class MessageController : Controller
    {
        //TODO Shweta
        // GET: Show Message
        public PartialViewResult ShowMessage(int itemId)
        {
            using (ucmmarketplaceEntities en = new ucmmarketplaceEntities())
            {
                List<item> itemlists = en.items.ToList();
                List<GetUserData> itemlist = itemlists.Select(x => new GetUserData
                {
                    ItemID = x.ItemID,
                    UploadUserID = x.UploadUserID,
                    EmailId = x.user.EmailId,
                    UserId = x.user.UserId,
                    UserName = x.user.UserName,
                    ItemTitle = x.Title,
                }).Where(x => x.ItemID == itemId && x.UploadUserID == x.UserId).ToList();
                return PartialView(itemlist);
            }
        }

        // Send Message thorough Email
        public ActionResult SendMessage(string emailid,string strmessage,string sendername,string recievername, string itemtitle, int itemid)
        {
            SendMessageEmail(emailid, strmessage, sendername, recievername,itemtitle);
            return RedirectToAction("View","Item", new { id = itemid });
        }
        [NonAction]
        public void SendMessageEmail(string emailID,string strMessage, string sendername, string recievername, string itemtitle)
        {
            var fromEmail = new MailAddress("ucmmarketemailid@gmail.com", "Welcome to UCM Marketplace");
            var toEmail = new MailAddress(emailID);
            var fromEmailpwd = "Ucmproject_1";
            string subject = "Message from Buyer at UCM Marketplace";
            strMessage = "Hello" + recievername + "<br/>Buyer named " + sendername + " is interested in your item with titled" + itemtitle + "Please see below message<p>" + strMessage;
            string body = "<br/><br/>" + strMessage +
                            "<br/><br/>";
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
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
