using DEPI_PROJECT.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DEPI_PROJECT.DAL.Repositories.Interfaces
{
    public interface IPropertyRepo
    {
        public IQueryable<Property> GetAll();
    }

}