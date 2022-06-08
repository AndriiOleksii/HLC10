using Turbocharged.Beanstalk;

var RequestCount = 10000;

var dummyCheck = new List<int>();
for (int i = 0; i < RequestCount; i++)
{
    dummyCheck.Add(i);
}

Console.WriteLine($"Start time - {DateTime.UtcNow.Subtract(new DateTime(2022, 6, 8, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds}");


IProducer producer = await BeanstalkConnection.ConnectProducerAsync("localhost:11300");
await producer.UseAsync("mytube");

await Parallel.ForEachAsync(dummyCheck, async (i, token) =>
{
    await producer.PutAsync<string>($"Message {i}", 5, TimeSpan.Zero, TimeSpan.Zero);
});


Console.WriteLine("Finish");

Console.ReadLine();