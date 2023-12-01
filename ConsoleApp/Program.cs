using Project.Core;

Counter counter = new Counter();
counter.Increment();
counter.Increment();
counter.Increment();
counter.Decrement();

Console.WriteLine($"Counter current value: {counter.Value}");