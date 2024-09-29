using MyProject.Collection;
using MyProject.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Entities
{
    public class TelephoneStation : ITelephoneStation
    {
        private MyCustomCollection<Tariff> tariffs;
        private MyCustomCollection<Client> clients;

        public TelephoneStation()
        {
            tariffs = new MyCustomCollection<Tariff>();
            clients = new MyCustomCollection<Client>();
        }

        public event Action<string> TariffChanged;
        public event Action<string> ClientAdded;
        public event Action<string> CallRegistered;

        public void AddTariff(string city, decimal pricePerMinute)
        {
            tariffs.Add(new Tariff(city, pricePerMinute));
            TariffChanged?.Invoke($"Тариф добавлен: {city} - {pricePerMinute} руб/мин.");
        }

        public void AddClient(string name, string surname)
        {
            clients.Add(new Client(name, surname));
            ClientAdded?.Invoke($"Клиент добавлен: {name} {surname}.");
        }

        public void RegisterCall(string clientSurname, string city, int duration)
        {
            Client? client = FindClientBySurname(clientSurname);
            if (client != null)
            {
                client.Calls.Add(new Call(city, duration));
                CallRegistered?.Invoke($"Звонок зарегистрирован: {clientSurname} в {city}, продолжительность: {duration} мин.");
            }
        }

        public decimal CalculateTotalCostForClient(string surname)
        {
            Client? client = FindClientBySurname(surname);
            if (client == null) return 0;

            decimal totalCost = 0;
            foreach (var call in client.Calls)
            {
                Tariff? tariff = FindTariffByCity(call.City);
                if (tariff != null)
                {
                    totalCost += Multiply(tariff.PricePerMinute, call.Duration);
                }
            }

            return totalCost;
        }

        public decimal CalculateTotalCostForAllCalls()
        {
            decimal totalCost = 0;

            foreach (var client in clients)
            {
                totalCost += CalculateTotalCostForClient(client.Surname);
            }

            return totalCost;
        }

        public int CountCallsToCity(string city)
        {
            int callCount = 0;

            foreach (var client in clients)
            {
                foreach (var call in client.Calls)
                {
                    if (call.City == city)
                    {
                        callCount++;
                    }
                }
            }

            return callCount;
        }

        private Client? FindClientBySurname(string surname)
        {
            foreach (var client in clients)
            {
                if (client.Surname == surname) return client;
            }
            return null;
        }

        private Tariff? FindTariffByCity(string city)
        {
            foreach (var tariff in tariffs)
            {
                if (tariff.City == city) return tariff;
            }
            return null;
        }

        private static T Multiply<T>(T a, int b)
            where T : INumber<T>
        {
            return a * T.CreateChecked(b);
        }
    }
}
