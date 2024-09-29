using MyProject;

class Program
{
    static void Main(string[] args)
    {
        var station = new TelephoneStation();

        // Подписываемся на события
        station.TariffChanged += message => Console.WriteLine($"Event: {message}");
        station.ClientRegistered += message => Console.WriteLine($"Event: {message}");
        station.CallRegistered += message => Console.WriteLine($"Event: {message}");

        // Добавляем тарифы
        station.AddTariff("New York", 0.05m);
        station.AddTariff("Los Angeles", 0.07m);
        station.AddTariff("Chicago", 0.06m);

        // Регистрация клиентов
        station.RegisterClient("John", "Doe");
        station.RegisterClient("Jane", "Smith");

        // Регистрация звонков
        station.RegisterCall("Doe", "New York", 10);
        station.RegisterCall("Doe", "Los Angeles", 5);
        station.RegisterCall("Smith", "Chicago", 20);

        // Вывод результатов
        Console.WriteLine("Tariffs sorted by price:");
        foreach (var tariff in station.GetTariffsSortedByPrice())
        {
            Console.WriteLine(tariff);
        }

        Console.WriteLine($"\nTotal cost of all calls: {station.GetTotalCostOfAllCalls()}");

        Console.WriteLine($"\nTotal cost for client Doe: {station.GetTotalCostForClient("Doe")}");

        Console.WriteLine($"\nClient with maximum payment: {station.GetClientWithMaxPayment()}");

        decimal amountThreshold = 1.0m;
        Console.WriteLine($"\nNumber of clients with payments greater than {amountThreshold}: {station.GetClientsWithPaymentGreaterThan(amountThreshold)}");

        Console.WriteLine("\nPayments by tariff for client Doe:");
        foreach (var (City, TotalPayment) in station.GetPaymentByTariffForClient("Doe"))
        {
            Console.WriteLine($"City: {City}, Total Payment: {TotalPayment}");
        }
    }
}
