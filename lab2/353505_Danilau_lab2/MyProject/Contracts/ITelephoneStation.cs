using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Contracts
{
    public interface ITelephoneStation
    {
        void AddTariff(string city, decimal pricePerMinute); // Ввод информации о тарифах
        void AddClient(string name, string surname); // Ввод информации о клиентах
        void RegisterCall(string clientSurname, string city, int duration); // Регистрация звонков
        decimal CalculateTotalCostForClient(string surname); // Вычисление стоимости всех звонков клиента
        decimal CalculateTotalCostForAllCalls(); // Вычисление общей стоимости всех звонков на АТС
        int CountCallsToCity(string city); // Вычисление количества всех звонков в заданный город
    }
}
