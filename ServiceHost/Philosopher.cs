namespace ServiceHost;

public class Philosopher
{
    private readonly string _name;
    private readonly Fork _leftFork;
    private readonly Fork _rightFork;
    private readonly int _thinkingTime;
    private readonly int _eatingTime;
    private readonly Random _random;

    public Philosopher(string name, Fork leftFork, Fork rightFork, int thinkingTime, int eatingTime)
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
        
        Console.WriteLine($"Name: {_name} takes Left-Fork: {_leftFork.Name}");
        TakeLeftFork();
        Console.WriteLine($"Name: {_name} takes Right-Fork: {_rightFork.Name}");
        TakeRightFork();

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