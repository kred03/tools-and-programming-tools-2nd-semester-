using System.Text.Json;
using Danillau_353505_Lab6.Interfaces;

namespace FileService
{
    public class FileService<T> : IFileService<T>
        where T : class
    {
        public IEnumerable<T> ReadFile(string fileName)
        {
            using var fs = new FileStream(fileName, FileMode.OpenOrCreate);
            return JsonSerializer.Deserialize<IEnumerable<T>>(fs)!;
        }

        public void SaveData(IEnumerable<T> data, string fileName)
        {
            using var fs = new FileStream(fileName, FileMode.OpenOrCreate);
            JsonSerializer.Serialize(fs, data);
        }
    }
}
