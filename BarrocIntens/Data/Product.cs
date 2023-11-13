using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data
{
    public class Product
    {
        public string Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
       
        public decimal Price { get; set; }
        public int StockQuantity {  get; set; }
        public bool IsOrdered { get; set; }

        //public string ImagePath { get; set; }

        public  Product_category category { get; set; } 
        public int ProductcatogoryId { get; set; }

       


        public string PriceFormatted => string.Format("{0:N2}", Price);
    }
}
