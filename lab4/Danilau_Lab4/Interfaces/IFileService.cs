namespace Danilau_Lab4.Interfaces
{
    interface IFileService<T>
    {
        /// <summary> Именованный итератор, считывающий данные из бинарного файла. </summary>
        IEnumerable<T> ReadFile(string fileName);

        /// <summary> Сохраняет коллекцию data в бинарный файл. </summary>
        void SaveData(IEnumerable<T> data, string fileName);
    }
}
