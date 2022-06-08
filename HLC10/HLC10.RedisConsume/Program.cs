using StackExchange.Redis;

var redis = ConnectionMultiplexer.Connect("localhost:6379,password=test");

while (true)
{
    var value = redis.GetDatabase().ListLeftPop("queue-1");

    if (value.HasValue)
    {
        Console.WriteLine($"Last handled at {DateTime.UtcNow.Subtract(new DateTime(2022, 6, 8, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds}");
    }
}