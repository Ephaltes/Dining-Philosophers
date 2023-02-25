using System.Diagnostics;

namespace ServiceHost;

public class Philosopher 
{
    private readonly int _eatingTime;
    private readonly Fork _leftFork;
    private readonly string _name;
    private readonly Random _random;
    private readonly Fork _rightFork;
    private readonly StartFork _startFork;
    private readonly int _thinkingTime;
    private readonly Stopwatch _totalTimeStopwatch;
    private readonly Stopwatch _forkTimeStopwatch;

    public Philosopher(string name, Fork leftFork, Fork rightFork, int thinkingTime, int eatingTime,
        StartFork startFork)
    {
        _name = name;
        _leftFork = leftFork;
        _rightFork = rightFork;
        _thinkingTime = thinkingTime;
        _eatingTime = eatingTime;
        _random = new Random(Guid.NewGuid().GetHashCode());
        _startFork = startFork;
        _totalTimeStopwatch = new Stopwatch();
        _forkTimeStopwatch = new Stopwatch();
        _totalTimeStopwatch.Start();
    }

    public void Eat()
    {
        int thinkingTime = _random.Next(_thinkingTime);
        int eatingTime = _random.Next(_eatingTime);

        Thread.Sleep(thinkingTime);
        //Console.WriteLine($"Name: {_name} finished Thinking");

        if (_startFork == StartFork.Left)
        {
            TakeLeftFork();
            //Thread.Sleep(500); //to force easier deadlocks
            TakeRightFork();
        }
        else
        {
            TakeRightFork();
            //Thread.Sleep(500); //to force easier deadlocks
            TakeLeftFork();
        }

        Thread.Sleep(eatingTime);
        //Console.WriteLine($"Name: {_name} finished eating");

        PutForksBack();
    }

    private void PutForksBack()
    {
        Monitor.Exit(_leftFork);
        Monitor.Exit(_rightFork);
    }

    private void TakeRightFork()
    {
        Console.WriteLine($"Name: {_name} want Right-Fork: {_rightFork.Name}");
        _forkTimeStopwatch.Start();
        Monitor.Enter(_rightFork);
        _forkTimeStopwatch.Stop();
        Console.WriteLine($"Name: {_name} took Right-Fork: {_rightFork.Name}");
    }

    private void TakeLeftFork()
    {
        Console.WriteLine($"Name: {_name} want Left-Fork: {_leftFork.Name}");
        _forkTimeStopwatch.Start();
        Monitor.Enter(_leftFork);
        _forkTimeStopwatch.Stop();
        Console.WriteLine($"Name: {_name} took Left-Fork: {_leftFork.Name}");
    }

    public void PrintTimes()
    {
        _totalTimeStopwatch.Stop();
        Console.WriteLine($"{_name}: Total Time: {_totalTimeStopwatch.Elapsed.TotalSeconds}," +
                          $" Time waiting for Fork: {_forkTimeStopwatch.Elapsed.TotalSeconds}");
    }
}

public enum StartFork
{
    Left,
    Right
}