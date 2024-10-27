using Danilau_Lab4.Interfaces;


namespace Danilau_Lab4
{
    public class FileService<T> : IFileService<T>
        where T : ISerializable, new()
    {
        public IEnumerable<T> ReadFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException($"Файл {fileName} не найден.");
            }

            using var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            using var reader = new BinaryReader(fs);
            while (fs.Position < fs.Length)
            {
                T? item = default;
                try
                {
                    item = new T();
                    item.Deserialize(reader);

                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Ошибка при десериализации объекта.", ex);
                }

                yield return item;
            }
        }

        public void SaveData(IEnumerable<T> data, string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                using (var writer = new BinaryWriter(fs))
                {
                    foreach (var item in data)
                    {
                        item.Serialize(writer);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка при записи файла: {ex.Message}");
                throw;  // Пробрасываем исключение дальше, если нужно
            }
        }
    }
}
