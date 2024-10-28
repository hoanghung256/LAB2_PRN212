using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
        void AddProduct(Product p);
        void DeleteProduct(Product p);
        void UpdateProduct(Product p);
        Product GetProductById(int id);
    }
}
