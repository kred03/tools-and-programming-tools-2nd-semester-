using Danillau_353505_Lab6.Entities;
using Danillau_353505_Lab6.Interfaces;
using System.Reflection;

List<Employee> employees =
[
    new Employee { Name = "Matvey", Age = 18, IsMale = true },
    new Employee { Name = "Rygor", Age = 19, IsMale = true },
    new Employee { Name = "Volchok", Age = 19, IsMale = true },
    new Employee { Name = "Kristina", Age = 18, IsMale = false },
    new Employee { Name = "Dima", Age = 19, IsMale = true }
];

var asm = Assembly.LoadFrom($"FIleService.dll");
var fileServiceType = asm.GetType("FileService.FileService`1")?.MakeGenericType(typeof(Employee));
var fileService = Activator.CreateInstance(fileServiceType!) as IFileService<Employee>;


var path = Directory.GetCurrentDirectory;
fileService?.SaveData(employees, $"{path}employees.json");
Console.WriteLine("Employees added to json");

var readEmployees = fileService?.ReadFile($"{path}employees.json");
Console.WriteLine("Employees from data/employees.json...");
foreach (var e in readEmployees!)
{
    Console.WriteLine($"Employee: Name: {e.Name}, Age: {e.Age}, Male?: {e.IsMale}");
}