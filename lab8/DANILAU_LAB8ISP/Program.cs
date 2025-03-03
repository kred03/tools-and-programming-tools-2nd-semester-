namespace DANILAULAB8ISP
{

    class Program
    {
        static async Task Main(string[] args)
        {
            var employees = new List<Employee>();
            for (int i = 0; i < 100; i++)
            {
                var employee = new Employee(i, $"Employee {i}", new Random().Next(20, 60));
                employees.Add(employee);
            }

            Console.WriteLine($"Количество сотрудников в коллекции: {employees.Count}");
            var streamService = new StreamService<Employee>();

            using (MemoryStream memoryStream = new MemoryStream())
            {

                IProgress<string> progress = new Progress<string>(message => Console.WriteLine(message));


                await streamService.WriteToStreamAsync(memoryStream, employees, progress);


                string fileName = "employees.txt";
                await streamService.CopyFromStreamAsync(memoryStream, fileName, progress);

                Console.WriteLine($"Сотрудники успешно скопированы в файл {fileName}");
            }
        }
    }
}
