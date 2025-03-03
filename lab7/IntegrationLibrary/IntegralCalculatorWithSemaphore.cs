using System.Diagnostics;

namespace IntegrationLibrary
{
    public class IntegralCalculatorWithSemaphore
    {
        private static Semaphore semaphore;

        public event EventHandler<double> OnProgress;
        public event EventHandler<(double result, long ticks)> OnCompleted;

        
        public IntegralCalculatorWithSemaphore(int maxConcurrentThreads)
        {
            semaphore = new Semaphore(maxConcurrentThreads, maxConcurrentThreads); 
        }

        public void Calculate()
        {
            semaphore.WaitOne();
            double step = 0.00001;
            double result = 0.0;
            long iterations = (long)(1 / step);
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            for (long i = 0; i < iterations; i++)
            {
                double x = i * step;
                result += Math.Sin(x) * step;
                
                for (int j = 0; j < 10000; j++)
                {
                    double temp = 1.0 * 2.0; 
                }

               
                if (i % (iterations / 1000) == 0)
                {
                    OnProgress?.Invoke(this, (double)i / iterations * 100);
                }
            }
            stopwatch.Stop();
         
            OnCompleted?.Invoke(this, (result, stopwatch.ElapsedTicks));

            semaphore.Release(); 
        }
    }
}