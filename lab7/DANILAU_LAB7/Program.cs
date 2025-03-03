using IntegrationLibrary;

namespace DANILAU_LAB7
{
    class Program
    {
        static EventWaitHandle event1Done = new AutoResetEvent(false);
        static EventWaitHandle event2Done = new AutoResetEvent(false);

        static void Main(string[] args)
        {
         
            IntegralCalculator calculator1 = new IntegralCalculator();
            IntegralCalculatorWithMutex calculator2 = new IntegralCalculatorWithMutex();
            IntegralCalculatorWithSemaphore calculator3 = new IntegralCalculatorWithSemaphore(5);
            
            SubscribeToEvents(calculator1, ConsoleColor.Green, "IntegralCalculator");
            SubscribeToEvents(calculator2, ConsoleColor.Red, "IntegralCalculatorWithMutex");
            SubscribeToEvents(calculator3, ConsoleColor.Yellow, "IntegralCalculatorWithSemaphore");
            
            Thread thread1 = new Thread(() => {
                calculator1.Calculate();
                event1Done.Set(); 
            });
            Thread thread2 = new Thread(() => {
                event1Done.WaitOne(); 
                calculator2.Calculate();
                event2Done.Set(); 
            });
            Thread thread3 = new Thread(() => {
                event2Done.WaitOne(); 
                calculator3.Calculate();
            });

            
            thread1.Priority = ThreadPriority.Highest;
            thread2.Priority = ThreadPriority.Normal;
            thread3.Priority = ThreadPriority.Lowest;
            
            thread1.Start();
            thread2.Start();
            thread3.Start();

            thread1.Join();  
            thread2.Join();  
            thread3.Join();  

            Console.WriteLine("Все вычисления завершены.");
        }
        
        private static void SubscribeToEvents(object calculator, ConsoleColor color, string calculatorName)
        {
            if (calculator is IntegralCalculator calc1)
            {
                calc1.OnProgress += (sender, progress) =>
                {
                    PrintProgress(progress, color, calculatorName);
                };

                calc1.OnCompleted += (sender, result) =>
                {
                    PrintResult(result.result, result.ticks, calculatorName, ConsoleColor.Blue);
                };
            }
            else if (calculator is IntegralCalculatorWithMutex calc2)
            {
                calc2.OnProgress += (sender, progress) =>
                {
                    PrintProgress(progress, color, calculatorName);
                };

                calc2.OnCompleted += (sender, result) =>
                {
                    PrintResult(result.result, result.ticks, calculatorName, ConsoleColor.Blue);
                };
            }
            else if (calculator is IntegralCalculatorWithSemaphore calc3)
            {
                calc3.OnProgress += (sender, progress) =>
                {
                    PrintProgress(progress, color, calculatorName);
                };

                calc3.OnCompleted += (sender, result) =>
                {
                    PrintResult(result.result, result.ticks, calculatorName, ConsoleColor.Blue);
                };
            }
        }

       
        private static void PrintProgress(double progress, ConsoleColor color, string calculatorName)
        {
                Console.ForegroundColor = color;
                Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} ({calculatorName}): [{GetProgressBar(progress)}] {progress:F1}%");
               }
        
        private static void PrintResult(double result, long ticks, string calculatorName, ConsoleColor color)
        {
                Console.ForegroundColor = color;
                Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} ({calculatorName}): Завершен с результатом: {result}, Время (в тиках): {ticks}");
           }
        
        private static string GetProgressBar(double progress)
        {
            int totalBlocks = 50;
            int filledBlocks = (int)(progress / 100 * totalBlocks);
            string bar = new string('=', filledBlocks).PadRight(totalBlocks);
            return $"{bar}>"; 
        }
    }
}
