using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Producer.API.Domain.Events;
using RabbitMQ.Client;

namespace Producer.API.Infrastructure.Messaging
{
    public class ProducerCreatedEvent : IProducerCreatedEvent
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public ProducerCreatedEvent()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "v2h.producer", type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
            _channel.QueueDeclare(queue: "producer.created.queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: "producer.created.queue", exchange: "v2h.producer", routingKey: "producer.created");
        }

        public Task Publish(Guid producerId, Guid userId, string name)
        {
            var message = new ProducerCreated
            {
                CorrelationId = Guid.NewGuid().ToString(),
                ProducerId = producerId,
                UserId = userId,
                Name = name
            };

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            var properties = _channel.CreateBasicProperties();
            properties.CorrelationId = message.CorrelationId;
            try
            {
                _channel.BasicPublish(exchange: "v2h.producer", routingKey: "producer.created", basicProperties: properties, body: body);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error publishing event: {ex.Message}");

            }
            return Task.CompletedTask;
        }
    }
}