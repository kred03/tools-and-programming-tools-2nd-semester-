using MyProject.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Entities
{
    public class Client
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public MyCustomCollection<Call> Calls { get; set; }

        public Client(string name, string surname)
        {
            Name = name;
            Surname = surname;
            Calls = new MyCustomCollection<Call>();
        }
    }
}
