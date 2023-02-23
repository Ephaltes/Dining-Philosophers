namespace ServiceHost;

public class Philosopher
{
    public string Name { get; set; }
    public Fork LeftFork { get; set; }
    public Fork RightFork { get; set; }
    private int _thinkingTime;
    private int _eatingTime;

    public Philosopher(string name, Fork leftFork, Fork rightFork, int thinkingTime, int eatingTime)
    {
        Name = name;
        LeftFork = leftFork;
        RightFork = rightFork;
        _thinkingTime = thinkingTime;
        _eatingTime = eatingTime;
    }

    public void Eat()
    {
        
    }
}