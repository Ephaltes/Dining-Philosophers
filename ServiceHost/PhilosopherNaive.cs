namespace ServiceHost;

public class PhilosopherNaive
{
    private readonly int _eatingTime;
    private readonly Fork _leftFork;
    private readonly string _name;
    private readonly Random _random;
    private readonly Fork _rightFork;
    private readonly int _thinkingTime;

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

        TakeLeftFork();
        Thread.Sleep(500); //to force easier deadlocks
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
        Console.WriteLine($"Name: {_name} want Right-Fork: {_rightFork.Name}");
        Monitor.Enter(_rightFork);
        Console.WriteLine($"Name: {_name} took Right-Fork: {_rightFork.Name}");
    }

    private void TakeLeftFork()
    {
        Console.WriteLine($"Name: {_name} want Left-Fork: {_leftFork.Name}");
        Monitor.Enter(_leftFork);
        Console.WriteLine($"Name: {_name} took Left-Fork: {_leftFork.Name}");
    }
}