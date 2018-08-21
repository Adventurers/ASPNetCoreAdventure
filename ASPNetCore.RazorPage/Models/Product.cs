using System;
using System.Collections.Generic;

namespace ASPNetCore.RazorPage.Models
{
    public class Product
    {
        public int Id { get; set; }

        public String Title { get; set; }

        public List<ProductInventory> Inventories { get; set; }
    }
}
