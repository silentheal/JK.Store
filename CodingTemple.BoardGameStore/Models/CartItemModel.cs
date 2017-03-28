using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodingTemple.BoardGameStore.Models
{
    public class CartItemModel
    {
        public ProductModel Product { get; set; }
        public int? Quantity { get; set; }
    }
}