using Azure.Messaging.ServiceBus;

public class ServiceBusTopicReceiverService
{
    private readonly string _connectionString;
    private readonly string _topicName;

    public ServiceBusTopicReceiverService(IConfiguration configuration)
    {
        _connectionString = configuration["AzureServiceBus:ConnectionString"];
        _topicName = configuration["AzureServiceBus:TopicName"];
    }

    // Receives messages from the morning news subscription
    public async Task<string> ReceiveMorningNewsMessageAsync()
    {
        var client = new ServiceBusClient(_connectionString);
        var receiver = client.CreateReceiver(_topicName, "morningNewsSubscrption");

        var message = await receiver.ReceiveMessageAsync();
        if (message != null && message.ApplicationProperties.TryGetValue("msg", out var msgValue) && msgValue.ToString() == "m")
        {
            await receiver.CompleteMessageAsync(message);
            return $"Received Morning News: {message.Body.ToString()}";
        }

        return "No morning news messages available.";
    }

    // Receives messages from the evening news subscription
    public async Task<string> ReceiveEveningNewsMessageAsync()
    {
        var client = new ServiceBusClient(_connectionString);
        var receiver = client.CreateReceiver(_topicName, "EveningSubscription");

        var message = await receiver.ReceiveMessageAsync();
        if (message != null && message.ApplicationProperties.TryGetValue("msg", out var msgValue) && msgValue.ToString() == "e")
        {
            await receiver.CompleteMessageAsync(message);
            return $"Received Evening News: {message.Body.ToString()}";
        }

        return "No evening news messages available.";
    }
}
