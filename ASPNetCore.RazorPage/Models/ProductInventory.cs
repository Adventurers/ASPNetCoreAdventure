namespace ASPNetCore.RazorPage.Models
{
    public class ProductInventory
    {
        public int ProductId { get; set; }
        public int InventoryId { get; set; }
        public Product Product { get; set; }
        public Inventory Inventory { get; set; }
        public int ProductCount { get; set; }
    }
}
