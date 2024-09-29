using MyProject.Entities;
using MyProject.Entities;

namespace MyProject;

class Program
{
    static void Main(string[] args)
    {
        var station = new TelephoneStation();
        var journal = new Journal();

        station.TariffChanged += journal.LogEvent;
        station.ClientAdded += journal.LogEvent;
        station.CallRegistered += (message) => Console.WriteLine($"Event: {message}");

        // Заполнение данными тарифов
        station.AddTariff("New York", 0.05m);
        station.AddTariff("Los Angeles", 0.07m);
        station.AddTariff("Chicago", 0.06m);

        // Заполнение данными клиентов
        station.AddClient("John", "Doe");
        station.AddClient("Jane", "Smith");

        // Регистрация звонков
        station.RegisterCall("Doe", "New York", 10);  // 10 минут
        station.RegisterCall("Doe", "Los Angeles", 5);  // 5 минут
        station.RegisterCall("Smith", "Chicago", 20);  // 20 минут

        // Подсчет общей стоимости звонков для клиента "Doe"
        decimal totalCostDoe = station.CalculateTotalCostForClient("Doe");
        Console.WriteLine($"Total cost for client Doe: {totalCostDoe}");

        // Подсчет общей стоимости всех звонков
        decimal totalCostAll = station.CalculateTotalCostForAllCalls();
        Console.WriteLine($"Total cost for all calls: {totalCostAll}");

        // Подсчет количества звонков в Нью-Йорк
        int callsToNY = station.CountCallsToCity("New York");
        Console.WriteLine($"Number of calls to New York: {callsToNY}");

        journal.PrintLog();
    }
}