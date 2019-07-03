using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIs.AspNetCore.ExistingDb.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string SrcUrl { get; set; }
        public string Name { get; set; }
        public string Sensor { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int XResponsive { get; set; }
        public int YResponsive { get; set; }
        public int ClipX { get; set; }
        public int ClipY { get; set; }
        public int ClipW { get; set; }
        public int ClipH { get; set; }

        public Image(int id, string name, string sensor, int x, int y, int clipX, int clipY, int clipW, int clipH)
        {
            Id = id;
            Name = name;
            Sensor = sensor;
            X = x;
            Y = y;
            ClipX = clipX;
            ClipY = clipY;
            ClipW = clipW;
            ClipH = clipH;
        }
    }
}
