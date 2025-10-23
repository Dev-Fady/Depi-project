using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RealEstateBroker.DAL.Models
{
    public class PropertyGallery
    {
        [Key]
        public Guid MediaID { get; set; }
        public Guid PropertyID { get; set; }
        public string ImageURL { get; set; }
        public DateTime UploadedAt { get; set; }

        public virtual Property Property { get; set; }
    }
}
