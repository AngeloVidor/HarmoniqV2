using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Consumer.API.Infrastructure.Messaging
{
    public class ConsumerCreatedEvent : IConsumerCreatedEvent
    {

        private readonly IConnection _connection;
        private readonly IModel _channel;

        public ConsumerCreatedEvent()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "v2h.consumer", type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
            _channel.QueueDeclare(queue: "consumer.created", durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: "consumer.created", exchange: "v2h.consumer", routingKey: "consumer.created");
        }
        public Task Publish(Domain.Aggregates.Consumer consumer)
        {
            var @event = new Event
            {
                CorrelationId = Guid.NewGuid().ToString(),
                ConsumerId = consumer.Id,
                UserId = consumer.UserId,
                Name = consumer.Name
            };

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event));
            var properties = _channel.CreateBasicProperties();
            properties.CorrelationId = @event.CorrelationId;
            try
            {
                _channel.BasicPublish(exchange: "v2h.consumer", routingKey: "consumer.created", basicProperties: properties, body: body);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error publishing event: {ex.Message}");
                throw;
            }
            return Task.CompletedTask;
        }
    }
}