using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIs.AspNetCore.ExistingDb.Models
{
    public class DisplaySettings
    {   
        public int Id { get; set; }
        public string[] Backgrounds { get; set; }        
        public string[] Sensors { get; set; }

        public DisplaySettings() { }

        public DisplaySettings(string[] backgrounds, string[] sensors)
        {
            Id = 4000;
            Backgrounds = backgrounds;
            Sensors = sensors;
        }
    }
}
