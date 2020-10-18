using System;
using System.Collections.Generic;
using System.Text;

namespace TagTeam.ShoppingCart.Domain.CustomModels
{
    public class Ref_StockModel
    {
        public int stockID { get; set; }
        public string code { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string mainImageURL { get; set; }
        public string colorCode { get; set; }
        public int userID { get; set; }

        public string imageData { get; set; }
    }
}
