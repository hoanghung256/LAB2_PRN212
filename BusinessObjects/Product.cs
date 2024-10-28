using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Required]
        [StringLength(40)]
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public short UnitsInStock { get; set; }
        public decimal UnitPrice { get; set; }
        public virtual Category Category { get; set; }

        public Product() { }

        public Product(int productID, string productName, int categoryID, short unitsInStock, decimal unitPrice)
        {
            ProductID = productID;
            ProductName = productName;
            CategoryID = categoryID;
            UnitsInStock = unitsInStock;
            UnitPrice = unitPrice;
        }

        public override string? ToString()
        {
            return $"ProductID: {ProductID}, ProductName: {ProductName}, CategoryID: {CategoryID}, " +
                   $"UnitsInStock: {UnitsInStock}, UnitPrice: {UnitPrice:C}";
        }
    }
}
