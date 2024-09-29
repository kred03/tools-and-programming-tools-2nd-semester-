using MyProject.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Entities
{
    public class Journal
    {
        private MyCustomCollection<string> logEntries;

        public Journal()
        {
            logEntries = new MyCustomCollection<string>();
        }

        public void LogEvent(string message)
        {
            logEntries.Add(message);
        }

        public void PrintLog()
        {
            Console.WriteLine("Журнал событий:");
            foreach (var entry in logEntries)
            {
                Console.WriteLine(entry);
            }
        }
    }
}
