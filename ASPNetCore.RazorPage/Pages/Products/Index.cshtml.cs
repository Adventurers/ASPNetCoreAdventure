using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCore.RazorPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASPNetCore.RazorPage.Pages.Products
{
    public class IndexModel : PageModel
    {
        public List<Product> Products { get; set; }
        public void OnGet()
        {

        }

        public void OnGetProducts(int id , string name)
        {

        }
    }
}