using System.Diagnostics;

namespace ServiceHost;

public class PhilosopherNaive
{
    private readonly string _name;
    private readonly Fork _leftFork;
    private readonly Fork _rightFork;
    private readonly int _thinkingTime;
    private readonly int _eatingTime;
    private readonly Random _random;

    public PhilosopherNaive(string name, Fork leftFork, Fork rightFork, int thinkingTime, int eatingTime)
    {
        _name = name;
        _leftFork = leftFork;
        _rightFork = rightFork;
        _thinkingTime = thinkingTime;
        _eatingTime = eatingTime;
        _random = new Random(Guid.NewGuid().GetHashCode());
    }

    public void Eat()
    {
        int thinkingTime = _random.Next(_thinkingTime);
        int eatingTime = _random.Next(_eatingTime);
        
        Thread.Sleep(thinkingTime);
        Console.WriteLine($"Name: {_name} finished Thinking");
        
        Console.WriteLine($"Name: {_name} want Left-Fork: {_leftFork.Name}");
        TakeLeftFork();
        Console.WriteLine($"Name: {_name} took Left-Fork: {_leftFork.Name}");

        Thread.Sleep(500); //to force easier deadlocks
        
        Console.WriteLine($"Name: {_name} want Right-Fork: {_rightFork.Name}");
        TakeRightFork();
        Console.WriteLine($"Name: {_name} took Right-Fork: {_rightFork.Name}");


        Thread.Sleep(eatingTime);
        Console.WriteLine($"Name: {_name} finished eating");

        PutForksBack();
    }

    private void PutForksBack()
    {
        Monitor.Exit(_leftFork);
        Monitor.Exit(_rightFork);
    }

    private void TakeRightFork()
    {
        Monitor.Enter(_rightFork);
    }

    private void TakeLeftFork()
    {
        Monitor.Enter(_leftFork);
    }
}