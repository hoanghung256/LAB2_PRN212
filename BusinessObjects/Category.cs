using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Category
    {
        public int CategoryID { get; set; }
        [StringLength(15)]
        public string CategoryName { get; set; } = string.Empty;
        public virtual ICollection<Product> Products { get; set; }

        public Category()
        {
            Products = new HashSet<Product>();
        }

        public Category(string categoryName)
        {
            CategoryName = categoryName;
        }

        public Category(int categoryID, string categoryName)
        {
            CategoryID = categoryID;
            CategoryName = categoryName;
            Products = new HashSet<Product>();
        }
    }
}
