using System;
using System.Collections.Generic;
using System.Text;

namespace TagTeam.ShoppingCart.Domain
{
    public class Ref_StockWiseImages
    {
        public int imageID { get; set; }
        public int stockID { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string imageURL { get; set; }
        public string colorCode { get; set; }
        public int userID { get; set; }
    }
}
