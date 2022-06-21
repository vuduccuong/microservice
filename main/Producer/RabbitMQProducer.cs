using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace main.Producer;

public interface IMessageProducer
{
    void SendMessage<T>(T message);
}

public class RabbitMQProducer : IMessageProducer
{
    private ILogger<RabbitMQProducer> _logger;
    public RabbitMQProducer(ILogger<RabbitMQProducer> logger)
    {
        _logger = logger;
    }
    public void SendMessage<T>(T message)
    {
        var factory = new ConnectionFactory();
        factory.Uri = new Uri("amqp://guest:guest@localhost:49154/");
        var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        var properties = channel.CreateBasicProperties();

        var json = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(json);
        _logger.LogInformation(json);

        channel.BasicPublish(exchange: "", routingKey: "like_products", body: body, basicProperties:properties);
    }
}