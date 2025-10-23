using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBroker.DAL.Models
{
    public class Agent : User
    {
        public string AgencyName { get; set; }
        public string TaxIdentificationNumber { get; set; }
        public float Rating { get; set; }
        public int ExperienceYears { get; set; }

        public virtual ICollection<Property> Properties { get; set; } = new List<Property>();
    }
}
