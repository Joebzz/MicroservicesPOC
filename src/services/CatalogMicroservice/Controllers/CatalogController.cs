using System;
using System.Collections.Generic;
using System.Linq;
using CatalogMicroservice.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogMicroservice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogController : Controller
    {
        private static List<CatalogItem> _catalogItems = new List<CatalogItem>
            {
                new CatalogItem { Id = Guid.NewGuid(), Description = "Test Item Description 1", Name = "Test Item Name 1", Price = 123.45m },
                new CatalogItem { Id = Guid.NewGuid(), Description = "Test Item Description 2", Name = "Test Item Name 2", Price = 124.45m },
                new CatalogItem { Id = Guid.NewGuid(), Description = "Test Item Description 3", Name = "Test Item Name 3", Price = 125.45m },
                new CatalogItem { Id = Guid.NewGuid(), Description = "Test Item Description 4", Name = "Test Item Name 4", Price = 126.45m },
                new CatalogItem { Id = Guid.NewGuid(), Description = "Test Item Description 5", Name = "Test Item Name 5", Price = 127.45m },
            };

        // GET: api/<CatalogController>
        [HttpGet]
        public ActionResult<IEnumerable<CatalogItem>> Get()
        {
            return Ok(_catalogItems);
        }

        // GET <CatalogController>/110ec627-2f05-4a7e-9a95-7a91e8005da8
        [HttpGet("{id}")]
        public ActionResult<CatalogItem> Get(Guid id)
        {
            var catalogItem = _catalogItems.FirstOrDefault(i => i.Id == id);
            return Ok(catalogItem);
        }

        // POST <CatalogController>
        [HttpPost]
        public ActionResult Post([FromBody] CatalogItem catalogItem)
        {
            _catalogItems.Add(catalogItem);
            return CreatedAtAction(nameof(Get), new { id = catalogItem.Id }, catalogItem);
        }

        // DELETE <CatalogController>/110ec627-2f05-4a7e-9a95-7a91e8005da8
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var catalogItem = _catalogItems.FirstOrDefault(i => i.Id == id);
            _catalogItems.Remove(catalogItem);
            return new OkResult();
        }
    }
}
