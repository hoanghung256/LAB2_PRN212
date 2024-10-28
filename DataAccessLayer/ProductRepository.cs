using BusinessObjects;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductRepository : IProductRepository
    {
        public void AddProduct(Product p)
        {
            using var dbContext = new ProductStoreDbContext();
            var productInDb = dbContext.Products.Find(p.ProductID);

            if (productInDb != null)
            {
                throw new Exception("Product exists");
            }
            dbContext.Products.Add(p);
            dbContext.SaveChanges();
        }

        public void DeleteProduct(Product p)
        {
            using var dbContext = new ProductStoreDbContext();
            dbContext.Products.Remove(p);
            dbContext.SaveChanges();
        }

        public Product GetProductById(int id)
        {
            using var dbContext = new ProductStoreDbContext();
            var product = (from p in dbContext.Products
                           join c in dbContext.Categories on p.CategoryID equals c.CategoryID
                           where p.ProductID == id
                           select new Product
                           {
                               ProductID = p.ProductID,
                               ProductName = p.ProductName,
                               Category = new Category { CategoryName = c.CategoryName },
                               UnitsInStock = p.UnitsInStock,
                               UnitPrice = p.UnitPrice
                           }).FirstOrDefault();

            return product;
        }

        public List<Product> GetProducts()
        {
            using var dbContext = new ProductStoreDbContext();
            var products = from product in dbContext.Products
                           join category in dbContext.Categories on product.CategoryID equals category.CategoryID
                           select new Product
                           {
                               ProductID = product.ProductID,
                               ProductName = product.ProductName,
                               Category = new Category(product.CategoryID, category.CategoryName),
                               UnitsInStock = product.UnitsInStock,
                               UnitPrice = product.UnitPrice
                           };

            return products.ToList();
        }

        public void UpdateProduct(Product p)
        {
            using var dbContext = new ProductStoreDbContext();
            dbContext.Products.Update(p);
            dbContext.SaveChanges();
        }
    }
}
