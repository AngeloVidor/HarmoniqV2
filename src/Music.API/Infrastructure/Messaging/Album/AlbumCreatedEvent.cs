using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Music.API.Domain.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Music.API.Infrastructure.Messaging.Album
{
    public class AlbumCreatedEvent : IAlbumCreatedEvent
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public AlbumCreatedEvent(IServiceScopeFactory serviceScopeFactory)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "v2h.album", type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
            _channel.QueueDeclare(queue: "album.created.queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: "album.created.queue", exchange: "v2h.album", routingKey: "album.created");

            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task Consume()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var requestBody = Encoding.UTF8.GetString(body);
                var request = JsonConvert.DeserializeObject<AlbumContent>(requestBody);
                if (request != null)
                {

                    using var scope = _serviceScopeFactory.CreateScope();
                    var _albumRepository = scope.ServiceProvider.GetRequiredService<IAlbumRepository>();

                    try
                    {
                        var snapshot = new Domain.Entities.Album
                        {
                            AlbumId = request.Id,
                            ProducerId = request.ProducerId,
                            AddedAt = request.AddedAt,
                            UpdatedAt = request.UpdatedAt
                        };

                        await _albumRepository.SaveAsync(snapshot);
                        _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing the message: {ex.Message}");
                        _channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: false);
                    }
                }

            };
            _channel.BasicConsume(queue: "album.created.queue", autoAck: false, consumer: consumer);
            return Task.CompletedTask;
        }
    }
}