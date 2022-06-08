
using Turbocharged.Beanstalk;

IConsumer consumer = await BeanstalkConnection.ConnectConsumerAsync("localhost:11300");
await consumer.WatchAsync("mytube");

while (true)
{
    Job job = await consumer.ReserveAsync();

    if (job != null && job.Data != null)
    {
        Console.WriteLine($"Last handled at {DateTime.UtcNow.Subtract(new DateTime(2022, 6, 8, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds}");
        await consumer.DeleteAsync(job.Id);
    }
}

