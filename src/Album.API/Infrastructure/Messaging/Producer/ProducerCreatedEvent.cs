using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Album.API.Domain.Interfaces;
using Album.API.Domain.Snapshots;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Album.API.Infrastructure.Messaging
{
    public class ProducerCreatedEvent : IProducerCreatedEvent
    {

        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ProducerCreatedEvent(IServiceScopeFactory serviceScopeFactory)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "v2h.producer", type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
            _channel.QueueDeclare(queue: "producer.created.album.queue", durable: true, exclusive: false, autoDelete: false);
            _channel.QueueBind(queue: "producer.created.album.queue", exchange: "v2h.producer", routingKey: "producer.created");

            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task Consume()
        {

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                using var scope = _serviceScopeFactory.CreateScope();

                var _producerSnapRepo = scope.ServiceProvider.GetRequiredService<IProducerSnapshotRepository>();

                var body = ea.Body.ToArray();
                var requestBody = Encoding.UTF8.GetString(body);
                var request = JsonConvert.DeserializeObject<Request>(requestBody);

                try
                {
                    if (request != null)
                    {
                        var snapshot = new ProducerSnap
                        {
                            ProducerId = request.ProducerId,
                            UserId = request.UserId,
                            Name = request.Name
                        };

                        await _producerSnapRepo.SaveAsync(snapshot);
                        _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing the message: {ex.Message}");
                    _channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: false);
                }
            };
            _channel.BasicConsume(queue: "producer.created.album.queue", autoAck: false, consumer: consumer);
            return Task.CompletedTask;
        }
    }
}