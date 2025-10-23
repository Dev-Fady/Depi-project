using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RealEstateBroker.DAL.Enums.PropertyPurpose;
using static RealEstateBroker.DAL.Enums.PropertyStateType;
using static RealEstateBroker.DAL.Enums.PropertyStatus;

namespace RealEstateBroker.DAL.Models
{
    public class Property
    {
        public Guid PropertyID { get; set; }

        public string City { get; set; }

        public string Address { get; set; } 

        public string GoogleMapsURL { get; set; }

        public PropertyType PropertyType { get; set; }
        public Purpose PropertyPurpose { get; set; } 
        public Status PropertyStatus { get; set; }  

        public float Price { get; set; }

        public float Square { get; set; }   

        public string Description { get; set; }

        public DateTime DateListed { get; set; }

        public Guid AgentID { get; set; }
        public Guid CompoundID { get; set; }

        public virtual Amenity Amenity { get; set; }
        public virtual Agent Agent { get; set; }
        public virtual Compound Compound { get; set; }

        public virtual ICollection<PropertyGallery> PropertyGalleries { get; set; } = new List<PropertyGallery>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<WishList> WishLists { get; set; } = new List<WishList>();
    }
}
