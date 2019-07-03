using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebAPIs.AspNetCore.ExistingDb.Models;

namespace WebAPIs.AspNetCore.ExistingDb.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class DisplaySettingsController : ControllerBase
    {
        private DisplaySettings _context;

        public DisplaySettingsController(){            
            SeedSettings();
        }

        // GET: api/DisplaySettings
        [HttpGet]
        public DisplaySettings GetDisplaySettings()
        {
            if (_context == null)
            {
                return null;
            }

            return _context;            
        }

        private void SeedSettings()
        {
            _context = new DisplaySettings
            (
                new string[] { "Blue", "White", "Lightseagreen" },
                new string[] { "OGEN", "IKONOS", "OFEK", "GEOEYE" }                
            );             
            
        }
    }
}
