using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBroker.DAL.Models
{
    public class Compound
    {
       public Guid CompoundID { get; set; }
       public string Name { get; set; }
       public string City { get; set; }
       public string Address { get; set; }
       public string Description { get; set; }

        public virtual ICollection<Property> Properties { get; set; } = new List<Property>();
    }
}
