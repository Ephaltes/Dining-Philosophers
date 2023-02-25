// See https://aka.ms/new-console-template for more information

namespace ServiceHost;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Enter number of Philosophers dinning: ");
        int philosopherCount = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter max thinking Time: ");
        int maxThinkingTime = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter max eating Time: ");
        int maxEatingTime = Convert.ToInt32(Console.ReadLine());
        
        List<Fork> forks = new();
        List<Thread> threadList = new();
        List<Philosopher> philosophers = new List<Philosopher>();
        
        for (int i = 0; i < philosopherCount; i++) forks.Add(new Fork($"Fork_{i}"));
        
        for (int i = 0; i < philosopherCount; i++)
        {
            // PhilosopherNaive philosopher = new PhilosopherNaive(
            //     $"Philosopher_{i}",
            //     forks[i],
            //     forks[(i + 1) % philosopherCount],
            //     maxThinkingTime,
            //     maxEatingTime,
            //     stopwatch
            // );
            
            Philosopher philosopher = new Philosopher(
                $"Philosopher_{i}",
                forks[i],
                forks[(i + 1) % philosopherCount],
                maxThinkingTime,
                maxEatingTime,
                i % 2 == 0 ? StartFork.Right: StartFork.Left
            );
        
            
            Thread thread = new(() => {
                for(int j=0;j<50;j++)
                    philosopher.Eat();
            });
        
            philosophers.Add(philosopher);
            threadList.Add(thread);
        }
        
        Parallel.ForEach(threadList, (listItem) =>
        {
            listItem.Start();
        });
        
        threadList.ForEach(thread => thread.Join());
        
        Console.WriteLine("all finished Eating");
        
        //Zeit verändert sich kaum von den Threads, weil durch das Thread sleep auch die Stopwatch angehalten wird
        philosophers.ForEach(philosopher => philosopher.PrintTimes());
        
        Console.ReadLine();
    }
}