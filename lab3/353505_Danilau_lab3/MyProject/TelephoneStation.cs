using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject
{
    public class TelephoneStation
    {
        private Dictionary<string, Tariff> tariffs;
        private List<Client> clients;

        public event Action<string>? TariffChanged;
        public event Action<string>? ClientRegistered;
        public event Action<string>? CallRegistered;

        public TelephoneStation()
        {
            tariffs = new Dictionary<string, Tariff>();
            clients = new List<Client>();
        }

        public void AddTariff(string city, decimal pricePerMinute)
        {
            tariffs[city] = new Tariff(city, pricePerMinute);
            TariffChanged?.Invoke($"Tariff added/updated: {city} - {pricePerMinute} per minute.");
        }

        public void RegisterClient(string name, string surname)
        {
            clients.Add(new Client(name, surname));
            ClientRegistered?.Invoke($"Client registered: {name} {surname}.");
        }

        public void RegisterCall(string clientSurname, string city, int duration)
        {
            var client = clients.FirstOrDefault(c => c.Surname == clientSurname);
            if (client != null)
            {
                client.Calls.Add(new Call(city, duration));
                CallRegistered?.Invoke($"Call registered: {clientSurname} to {city}, duration: {duration} minutes.");
            }
        }

        public IEnumerable<string> GetTariffsSortedByPrice()
        {
            // Получение тарифов, отсортированных по цене
            var query = from tariff in tariffs.Values
                        orderby tariff.PricePerMinute
                        select tariff.City;

            return query;
        }

        public decimal GetTotalCostOfAllCalls()
        {
            // Подсчет общей стоимости всех звонков
            var query = (from client in clients
                         from call in client.Calls
                         where tariffs.ContainsKey(call.City)
                         let result = tariffs[call.City].PricePerMinute * call.Duration
                         select result).Sum();

            return query;
        }

        public decimal GetTotalCostForClient(string surname)
        {
            // Подсчет общей стоимости всех звонков клиента
            var client = clients.FirstOrDefault(c => c.Surname == surname);
            if (client is null) return 0;

            var query = (from call in client.Calls
                         where tariffs.ContainsKey(call.City)
                         select tariffs[call.City].PricePerMinute * call.Duration).Sum();

            return query;
        }

        public string GetClientWithMaxPayment()
        {
            // Получение клиента с максимальной оплатой
            var query = (from client in clients
                         let totalCost = GetTotalCostForClient(client.Surname)
                         orderby totalCost descending
                         select client).FirstOrDefault();

            return query?.Name ?? "No clients";
        }

        public int GetClientsWithPaymentGreaterThan(decimal amount)
        {
            // Получение количества клиентов, заплативших больше определенной суммы
            var query = (from client in clients
                         let totalCost = GetTotalCostForClient(client.Surname)
                         where totalCost > amount
                         select client).Count();

            return query;
        }
        public IEnumerable<(string City, decimal TotalPayment)> GetPaymentByTariffForClient(string surname)
        {
            // Получение платежей по каждому тарифу для клиента
            var client = clients.FirstOrDefault(c => c.Surname == surname);
            if (client is null) return Enumerable.Empty<(string, decimal)>();

            var query = from call in client.Calls
                        where tariffs.ContainsKey(call.City)
                        group call by call.City into groupedCalls
                        let totalPayment = groupedCalls.Sum(call => tariffs[call.City].PricePerMinute * call.Duration)
                        select (City: groupedCalls.Key, TotalPayment: totalPayment);

            return query;
        }
    }
}