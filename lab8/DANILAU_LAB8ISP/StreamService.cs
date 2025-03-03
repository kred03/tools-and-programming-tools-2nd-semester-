namespace DANILAULAB8ISP
{
    public class StreamService<T>
    {
        private readonly object _syncLock = new object(); 

        
        public async Task WriteToStreamAsync(Stream stream, IEnumerable<T> data, IProgress<string> progress)
        {
            progress.Report($"Поток {Thread.CurrentThread.ManagedThreadId}: Начало записи в поток.");
            using (StreamWriter writer = new StreamWriter(stream, leaveOpen: true))
            {
                foreach (var item in data)
                {
                    await Task.Delay(100); 
                    await writer.WriteLineAsync(item?.ToString());
                }
            }
            progress.Report($"Поток ------> {Thread.CurrentThread.ManagedThreadId} ");
            progress.Report($"Поток {Thread.CurrentThread.ManagedThreadId}: Запись в поток завершена.");
        }

        
        public async Task CopyFromStreamAsync(Stream stream, string filename, IProgress<string> progress)
        {
            progress.Report($"Поток------ > { Thread.CurrentThread.ManagedThreadId}");
            progress.Report($"Поток {Thread.CurrentThread.ManagedThreadId}: Начало копирования.");
            lock (_syncLock)
            {
               stream.Position = 0; 
                using (FileStream fileStream = new FileStream(filename, FileMode.Create))
                {
                   stream.CopyTo(fileStream);
                }
            }
           progress.Report($"Поток {Thread.CurrentThread.ManagedThreadId}: Копирование завершено.");
        }
    }
}
