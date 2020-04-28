using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//Class to join Item and Category table and access thorught the project.
//Code by:Alpa Bandekar  04/16/2020
namespace UCMMarketplace.Models
{
    public class Itemviewmodel
    {
        public long ItemID { get; set; }
        public string Title { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
        public string Condition { get; set; }
        public string Status { get; set; }
        public string ImagePath { get; set; }
        public int UploadUserID { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public Nullable<double> Price { get; set; }
    }
}