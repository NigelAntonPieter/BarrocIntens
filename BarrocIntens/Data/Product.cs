using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data
{
    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
       
        public decimal Price { get; set; }
        public int StockQuantity {  get; set; }

        public string StockStatus => StockQuantity > 0 ? "Momenteel leverbaar" : "Uit voorraad";

        public bool IsOrdered { get; set; }

        public string ImagePath { get; set; }
        public List<Maintenance_Receipt> Maintenance_Receipts { get; set; }

        public int Product_categoryId { get; set; }
        public  Product_category Product_category { get; set; }
        public bool Is_employee_only { get; set; }
        public string ImagePathWithFallBack => ImagePath ?? "/Assets/Logo6_klein.png";
        public string PriceFormatted => string.Format("{0:N2}", Price);

        public override string ToString()
        {
            return Name;
        }
    }
}
