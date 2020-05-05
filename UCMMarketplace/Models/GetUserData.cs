using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//Created by : Alpa Bandekar
// Class for User and Item Data
namespace UCMMarketplace.Models
{
    public class GetUserData
    {
        public long ItemID { get; set; }
        public int UploadUserID { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string SenderUserName { get; set; }
        public string ItemTitle { get; set; }

    }
}