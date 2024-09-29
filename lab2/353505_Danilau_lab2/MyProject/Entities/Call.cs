using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Entities
{
    public class Call
    {
        public string City { get; set; }
        public int Duration { get; set; }

        public Call(string city, int duration)
        {
            City = city;
            Duration = duration;
        }
    }
}
