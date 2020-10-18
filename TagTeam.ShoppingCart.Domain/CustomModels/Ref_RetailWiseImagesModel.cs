using System;
using System.Collections.Generic;
using System.Text;

namespace TagTeam.ShoppingCart.Domain.CustomModels
{
    public class Ref_RetailWiseImagesModel
    {
        public int imageID { get; set; }
        public string retailCode { get; set; }
        public int retailID { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string imageURL { get; set; }
        public string colorCode { get; set; }
        public int userID { get; set; }
        public string imageData { get; set; }
    }
}
