using StackExchange.Redis;

var RequestCount = 100000;

var dummyCheck = new List<int>();
for (int i = 0; i < RequestCount; i++)
{
    dummyCheck.Add(i);
}

var redis = ConnectionMultiplexer.Connect("localhost:6379,password=test");

Console.WriteLine($"Start time - {DateTime.UtcNow.Subtract(new DateTime(2022, 6, 8, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds}");

await Parallel.ForEachAsync(dummyCheck, async (i, token) =>
{
    redis.GetDatabase().ListRightPush("queue-1", $"Message{i}");
});

Console.ReadLine();