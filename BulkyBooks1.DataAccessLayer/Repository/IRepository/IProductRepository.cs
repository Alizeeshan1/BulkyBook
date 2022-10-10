using BulkyBooks1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBooks1.DataAccessLayer.Repository.IRepository
{
    public interface ICoverRepository : IRepository<Cover>
    { 
        void Update(Cover obj);
    }
}
