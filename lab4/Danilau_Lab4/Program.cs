using Danilau_Lab4;

internal class Program
{
    private static readonly string folderName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Andrianov_Lab4");

    private static void Main(string[] args)
    {
        if (Directory.Exists(folderName))
        {
            Directory.Delete(folderName, true);
            Console.WriteLine($"Папка {folderName} уже существовала, ее содержимое было удалено.\n");
        }

        Directory.CreateDirectory(folderName);

        string[] extensions = { ".txt", ".rtf", ".dat", ".inf" };
        Random random = new Random();

        for (int i = 0; i < 10; i++)
        {
            string fileName = Path.Combine(folderName, Path.GetRandomFileName());
            string extension = extensions[random.Next(extensions.Length)];
            File.Create(fileName + extension).Close();
        }

        // Чтение содержимого папки и вывод списка файлов
        Console.WriteLine("Список файлов в папке:");
        var files = Directory.GetFiles(folderName);
        foreach (var file in files)
        {
            string extension = Path.GetExtension(file);
            Console.WriteLine($"Файл: {Path.GetFileName(file)} имеет расширение {extension}");
        }

        // Создание коллекции объектов класса Resident
        List<Resident> residents =
        [
            new Resident("Иван Иванов", 12),
            new Resident("Алексей Смирнов", 8),
            new Resident("Ольга Петрова", 21),
            new Resident("Мария Кузнецова", 15),
            new Resident("Дмитрий Сидоров", 5)
        ];

        // Сохранение коллекции в файл residents.dat с помощью FileService
        string residentFile = Path.Combine(folderName, "residents.dat");
        var fileService = new FileService<Resident>();
        fileService.SaveData(residents, residentFile);

        Console.WriteLine($"\nФайл {residentFile} был создан и записан.");

        // Переименование файла
        string newResidentFile = Path.Combine(folderName, "new_residents.dat");
        File.Move(residentFile, newResidentFile);
        Console.WriteLine($"\nФайл переименован в {newResidentFile}");

        // Чтение данных из нового файла в новую коллекцию
        List<Resident> loadedResidents = fileService.ReadFile(newResidentFile).ToList();

        // Вывод исходной коллекции
        Console.WriteLine("\nИсходная коллекция:");
        foreach (var resident in residents)
        {
            Console.WriteLine(resident);
        }

        // Вывод коллекции, прочитанной из файла
        Console.WriteLine("\nКоллекция, прочитанная из файла:");
        foreach (var resident in loadedResidents)
        {
            Console.WriteLine(resident);
        }

        // Сортировка с использованием MyCustomComparer
        var sortedResidents = loadedResidents.OrderBy(r => r, new MyCustomComparer<Resident>()).ToList();

        Console.WriteLine("\nКоллекция, отсортированная по Name (с использованием MyCustomComparer):");
        foreach (var resident in sortedResidents)
        {
            Console.WriteLine(resident);
        }

        // Сортировка с использованием лямбда-выражения по ApartmentNumber
        var sortedByApartment = loadedResidents.OrderBy(r => r.ApartmentNumber).ToList();

        Console.WriteLine("\nКоллекция, отсортированная по ApartmentNumber (с использованием лямбда-выражения):");
        foreach (var resident in sortedByApartment)
        {
            Console.WriteLine(resident);
        }
    }
}
