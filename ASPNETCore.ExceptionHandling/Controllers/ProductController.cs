using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNETCore.ExceptionHandling.Models;
using ASPNETCore.ExceptionHandling.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCore.ExceptionHandling.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        public IActionResult Get()
        {
            
            var products = new List<Product> {
                new Product{ Id = 1 , Title = "Shampoo" },
                new Product{ Id = 2 , Title = "Bath"}
            };

            throw new Exception("exception occured while fetching all the products from databse");
            //throw new HttpStatusCodeException(500);

            return Ok(products);

        }
    }
}