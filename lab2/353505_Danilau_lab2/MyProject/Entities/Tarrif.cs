using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Entities
{
    public class Tariff
    {
        public string City { get; set; }
        public decimal PricePerMinute { get; set; }

        public Tariff(string city, decimal pricePerMinute)
        {
            City = city;
            PricePerMinute = pricePerMinute;
        }
    }
}
