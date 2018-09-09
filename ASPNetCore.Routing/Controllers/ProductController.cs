using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPNetCore.Routing.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [Route("api/{version:versionConstraint(2)}/Product")]
        public async Task<bool> GetProduct(string version) {
            return await Task.FromResult(true);
        }
    }
}