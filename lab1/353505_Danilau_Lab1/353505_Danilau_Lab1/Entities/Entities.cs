using System.Collections.Generic;

namespace Entities
{
    public class Room
    {
        public int Number { get; }
        public decimal Price { get; }
        public bool IsBooked { get; set; }
        public List<string> Customers { get; set; }

        public Room(int number, decimal price)
        {
            Number = number;
            Price = price;
            IsBooked = false;
            Customers = new List<string>();
        }
    }
}
namespace Entities
{
    public class Customer
    {
        public string Name { get; }

        public Customer(string name)
        {
            Name = name;
        }
    }
}