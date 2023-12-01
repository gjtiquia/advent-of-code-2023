using Project.Core;

namespace TestProject;

public class CounterTests
{
    protected Counter _counter;

    [SetUp]
    public void SetupBeforeEveryTest()
    {
        _counter = new Counter();
    }

    [Test]
    public void CounterStartsAtZero()
    {
        Assert.That(_counter.Value, Is.EqualTo(0));
    }

    [Test]
    public void CounterCanIncrement()
    {
        _counter.Increment();
        Assert.That(_counter.Value, Is.EqualTo(1));
    }

    [Test]
    public void CounterCanDecrement()
    {
        _counter.Decrement();
        Assert.That(_counter.Value, Is.EqualTo(-1));
    }
}