using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAPIs.AspNetCore.ExistingDb.Models;

namespace WebAPIs.AspNetCore.ExistingDb.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private Image[] _imageContext;

        public ImagesController()
        {            
            SeedImages();
        }

        // GET: api/Images
        [HttpGet]
        public IEnumerable<Image> GetImages()
        {  
            if (_imageContext == null)
            {
                return null;
            }

            return _imageContext;
        }

        private void SeedImages()
        {
            _imageContext = new Image[]
            {
                new Image(1000, "Frog", "IKONOS", 0, 0, 800, 640, 300, 400),
                new Image(1001, "Bull", "IKONOS", 50, 350, 1300, 600, 700, 500),
                new Image(1002, "Dog", "GEOEYE", 250, 15, 500, 200, 500, 450),
                new Image(1003, "Cat", "OFEK", 310, 360, 600, 300, 350, 300)
            };             
        }
       
    }
}
