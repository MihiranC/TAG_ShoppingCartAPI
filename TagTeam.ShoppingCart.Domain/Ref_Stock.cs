﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TagTeam.ShoppingCart.Domain
{
    public class Ref_Stock
    {
        public int stockID { get; set; }
        public string code { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string mainImageURL { get; set; }
        public string colorCode { get; set; }
        public int userID { get; set; }
    }
}
