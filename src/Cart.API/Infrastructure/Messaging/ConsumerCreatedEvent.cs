using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cart.API.Domain.Interfaces;
using Cart.API.Domain.Snapshots;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Cart.API.Infrastructure.Messaging
{
    public class ConsumerCreatedEvent : IConsumerCreatedEvent
    {

        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ConsumerCreatedEvent(IServiceScopeFactory serviceScopeFactory)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "v2h.consumer", type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
            _channel.QueueDeclare(queue: "consumer.created", durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: "consumer.created", exchange: "v2h.consumer", routingKey: "consumer.created");

            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Consume()
        {
            System.Console.WriteLine("Listening!!");
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var msg = Encoding.UTF8.GetString(body);
                var @event = JsonConvert.DeserializeObject<Event>(msg);

                if (@event != null)
                {
                    try
                    {
                        var domainObject = MapToDomainObject(@event);
                        await SaveSnapshotAsync(domainObject);

                        _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing the message: {ex.Message}");
                        _channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: false);
                    }
                }
            };
            _channel.BasicConsume(queue: "consumer.created", autoAck: false, consumer: consumer);
        }

        private async Task SaveSnapshotAsync(Consumer consumer)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var _consumerRepository = scope.ServiceProvider.GetRequiredService<IConsumerRepository>();

            await _consumerRepository.AddAsync(consumer);
        }

        private Consumer MapToDomainObject(Event @event)
        {
            return new Consumer
            (
                consumerId: @event.ConsumerId,
                userId: @event.UserId,
                name: @event.Name
            );
        }

    }
}