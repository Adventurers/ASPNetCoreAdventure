using ASPNetCore.RazorPage.Models;
using System.Linq;

namespace ASPNetCore.RazorPage.Helpers
{
    public static class ProductInventoryHelper
    {
        public static string IsProductAvailibility(this Product product)
        {
            var productCount = product.Inventories.Select(p => p.ProductCount).Sum();
            return productCount > 0 ? "isAvailable" : "notAvailable";
        }
    }
}
