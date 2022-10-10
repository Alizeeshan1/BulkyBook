using BulkyBooks1.DataAccessLayer.Repository.IRepository;
using BulkyBooks1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBooks1.DataAccessLayer.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Product obj)
        {
            var objfromdb = _db.Products.FirstOrDefault(x => x.Id == obj.Id);
            if(objfromdb != null)
            {
                objfromdb.Title = obj.Title;
                objfromdb.Description = obj.Description;
                objfromdb.Author = obj.Author;
                objfromdb.ISBN = obj.ISBN;
                objfromdb.ListPrice = obj.ListPrice;
                objfromdb.Price = obj.Price;
               // objfromdb.Category = obj.Category;
                objfromdb.PriceFor50 = obj.PriceFor50;
                objfromdb.PriceFor100 = obj.PriceFor100;
                objfromdb.CategoryId = obj.CategoryId;
                objfromdb.CoverId = obj.CoverId;
                //objfromdb.Cover = obj.Cover;
                if(objfromdb != null)
                {
                    objfromdb.ImageUrl = obj.ImageUrl;
                }

            }
        }
    }
}
