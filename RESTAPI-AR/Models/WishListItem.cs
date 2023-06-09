﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTAPI_AR.Models
{
    internal class WishListItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime Created { get; set; } = DateTime.Now;
        public string ItemName { get; set; }
        public bool Collected {get; set;}
    }

    internal class CreateWishListItem
    {
        public string ItemName { get; set; }

    }
    internal class UpdateWishListItem
    {
        public string ItemName { get; set; }
        public bool Collected { get; set; }

    }
}
