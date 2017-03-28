﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodingTemple.BoardGameStore.Models
{
    public class CartModel
    {
        public decimal? SubTotal { get; set; }
        public CartItemModel[] Items { get; set; }
    }
}