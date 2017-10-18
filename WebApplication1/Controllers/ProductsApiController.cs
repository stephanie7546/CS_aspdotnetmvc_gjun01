using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Models
{
    [RoutePrefix("api/Products")]
    public class ProductsApiController : ApiController
    {
        MyDBEntities db = new MyDBEntities();

        [Route("GetPriceByID/{id}")]
        public int? GetPriceByID(int id)
        {
            var obj = db.Products.Find(id);
            if (obj != null)
                return obj.Price;
            else
                throw new HttpResponseException(HttpStatusCode.NotFound);
        }

    }
}
