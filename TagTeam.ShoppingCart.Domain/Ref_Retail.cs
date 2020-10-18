using System;
using System.Collections.Generic;
using System.Text;

namespace TagTeam.ShoppingCart.Domain
{
    public class Ref_Retail
    {
        public int retailID { get; set; }
        public string code { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string mainImageURL { get; set; }
        public string colorCode { get; set; }
        public int userID { get; set; }
        public string commissionPercentage { get; set; }
        public Boolean isActive { get; set; }
    }
}
