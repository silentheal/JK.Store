﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodingTemple.BoardGameStore.Models
{
    public class ProductModel
    {
        public int? ID { get; set; }

        public string Name { get; set; }
        public decimal? Price { get; set; }
        public int? Rating { get; set; }
        public string Description { get; set; }
        public string[] Images { get; set; }
    }
}