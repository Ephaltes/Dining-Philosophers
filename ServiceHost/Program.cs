// See https://aka.ms/new-console-template for more information

using ServiceHost;

Console.WriteLine("Enter number of Philosophers dinning: ");
int philosopherCount = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Enter max thinking Time: ");
int maxThinkingTime = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Enter max eating Time: ");
int maxEatingTime = Convert.ToInt32(Console.ReadLine());

List<Fork> forks =  new List<Fork>();
List<Philosopher> philosophers = new List<Philosopher>();

for (int i = 0; i < philosopherCount; i++)
{
    forks.Add(new Fork($"Fork_{i}"));
}

for (int i = 0; i < philosopherCount; i++)
{
    philosophers.Add(new Philosopher(
        $"Philosopher_{i}",
        forks[i],
        forks[(i+1) % philosopherCount],
        maxThinkingTime,
        maxEatingTime
    ));
}

foreach (Philosopher philosopher in philosophers)
{
    philosopher.Eat();
}