using System;
using Contracts;
using Entities;
using Collections;
using Interfaces;

namespace HotelManagement
{
    public class HotelSystem : IHotelSystem
    {
        private MyCustomCollection<Room> rooms;
        private MyCustomCollection<Customer> customers;

        public HotelSystem()
        {
            rooms = new MyCustomCollection<Room>();
            customers = new MyCustomCollection<Customer>();
        }

        public void RegisterCustomer(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                var customer = new Customer(name);
                customers.Add(customer);
                Console.WriteLine($"Клиент {name} зарегистрирован.");
            }
            else
            {
                Console.WriteLine("Имя клиента не может быть пустым.");
            }
        }

        public bool BookRoom(string customerName, int roomNumber)
        {
            bool customerExists = false;
            for (int i = 0; i < customers.Count; i++)
            {
                if (customers[i].Name == customerName)
                {
                    customerExists = true;
                    break;
                }
            }

            if (!customerExists)
            {
                Console.WriteLine("Клиент не найден.");
                return false;
            }

            for (int i = 0; i < rooms.Count; i++)
            {
                var room = rooms[i];
                if (room.Number == roomNumber)
                {
                    if (room.IsBooked)
                    {
                        Console.WriteLine("Комната уже забронирована!");
                        return false;
                    }
                    room.IsBooked = true;
                    room.Customers.Add(customerName);
                    return true;
                }
            }
            Console.WriteLine("Комната не найдена!");
            return false;
        }

        public void ShowAvailableRooms()
        {
            Console.WriteLine("Доступные комнаты:");
            for (int i = 0; i < rooms.Count; i++)
            {
                var room = rooms[i];
                if (!room.IsBooked)
                {
                    Console.WriteLine($"Комната номер: {room.Number}, Цена: {room.Price}");
                }
            }
        }

        public decimal CalculateStayCost(string customerName)
        {
            decimal totalCost = 0;
            for (int i = 0; i < rooms.Count; i++)
            {
                var room = rooms[i];
                if (room.Customers.Contains(customerName))
                {
                    totalCost += room.Price;
                }
            }
            return totalCost;
        }

        public int GetMostPopularRoom()
        {
            int mostPopularRoom = -1;
            int maxCount = 0;

            for (int i = 0; i < rooms.Count; i++)
            {
                var room = rooms[i];
                if (room.Customers.Count > maxCount)
                {
                    maxCount = room.Customers.Count;
                    mostPopularRoom = room.Number;
                }
            }

            return mostPopularRoom;
        }

        public void AddRoom(int number, decimal price)
        {
            rooms.Add(new Room(number, price));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            HotelSystem hotel = new HotelSystem();

            hotel.AddRoom(101, 100);
            hotel.AddRoom(102, 150);
            hotel.AddRoom(103, 200);

            while (true)
            {
                Console.WriteLine("\nСистема управления гостиницей");
                Console.WriteLine("1. Зарегистрировать клиента");
                Console.WriteLine("2. Забронировать комнату");
                Console.WriteLine("3. Показать доступные комнаты");
                Console.WriteLine("4. Подсчитать стоимость проживания");
                Console.WriteLine("5. Получить самую популярную комнату");
                Console.WriteLine("6. Выход");
                Console.Write("Выберите опцию: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Введите имя клиента: ");
                        string customerName = Console.ReadLine();
                        hotel.RegisterCustomer(customerName);
                        break;
                    case "2":
                        Console.Write("Введите имя клиента: ");
                        customerName = Console.ReadLine();
                        Console.Write("Введите номер комнаты: ");
                        if (int.TryParse(Console.ReadLine(), out int roomNumber))
                        {
                            hotel.BookRoom(customerName, roomNumber);
                        }
                        else
                        {
                            Console.WriteLine("Неверный номер комнаты.");
                        }
                        break;
                    case "3":
                        hotel.ShowAvailableRooms();
                        break;
                    case "4":
                        Console.Write("Введите имя клиента: ");
                        customerName = Console.ReadLine();
                        decimal cost = hotel.CalculateStayCost(customerName);
                        Console.WriteLine($"Общая стоимость для {customerName}: {cost}");
                        break;
                    case "5":
                        int popularRoom = hotel.GetMostPopularRoom();
                        if (popularRoom != -1)
                        {
                            Console.WriteLine($"Самая популярная комната: {popularRoom}");
                        }
                        else
                        {
                            Console.WriteLine("Пока ни одна комната не была забронирована.");
                        }
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Неверная опция. Пожалуйста, попробуйте снова.");
                        break;
                }
            }
        }
    }
}