using BulkyBooks1.DataAccessLayer.Repository.IRepository;
using BulkyBooks1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBooks1.DataAccessLayer.Repository
{
    public class CoverRepository : Repository<Cover>, ICoverRepository
    {
        private ApplicationDbContext _db;
        public CoverRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }




        public void Update(Cover obj)
        {
            _db.Covers.Update(obj);
        }
    }
}
