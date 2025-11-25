using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.DTOs.Compound
{
    public class CompoundReadDto
    {
        public Guid CompoundId { get; set; }
        public required string Name { get; set; }
        public required string City { get; set; }
        public required string Address { get; set; }
        public required string Description { get; set; }
    }
}
