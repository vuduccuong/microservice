using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using main.Models;
using main.Helpers;

namespace main.Consumer;

public class ProductConsumer : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<ProductConsumer> _loger;
    private IConnection _connection;
    private IModel _channel;
    public ProductConsumer(ILogger<ProductConsumer> loger, IServiceScopeFactory serviceScopeFactory)
    {
        _loger = loger;
        InitRabbitMQ();
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            var product = JsonConvert.DeserializeObject<Product>(message);

            if (product != null)
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MainDBContext>();
                dbContext.Products.Add(product);
                dbContext.SaveChanges();
            }
            _loger.LogInformation($"{product?.Id} - {product?.Title} - {product?.Image}");
        };

        _channel.BasicConsume(queue: "create_product", autoAck: true, consumer: consumer);
        return Task.CompletedTask;
    }

    private void InitRabbitMQ()
    {
        _loger.LogInformation("InitRabbitMQ");
        var factory = new ConnectionFactory();
        factory.Uri = new Uri("amqp://guest:guest@localhost:49154/");
        if (_connection == null)
        {
            _connection = factory.CreateConnection();
        }
        _channel = _connection.CreateModel();
        _channel.QueueDeclare("create_product");
    }

    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }
}