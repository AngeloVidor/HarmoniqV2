using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Album.API.Infrastructure.Messaging.Album
{
    public class AlbumCreatedEvent : IAlbumCreatedEvent
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public AlbumCreatedEvent()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "v2h.album", type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
            _channel.QueueDeclare(queue: "album.created.queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: "album.created.queue", exchange: "v2h.album", routingKey: "album.created");
        }

        public Task Publish(Domain.Aggregates.Album album)
        {
            var message = new AlbumContent
            {
                CorrelationId = Guid.NewGuid().ToString(),
                Id = album.Id,
                ProducerId = album.ProducerId,
                AddedAt = album.AddedAt,
                UpdatedAt = album.UpdatedAt,
            };

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            var properties = _channel.CreateBasicProperties();
            properties.CorrelationId = message.CorrelationId;
            try
            {
                _channel.BasicPublish(exchange: "v2h.album", routingKey: "album.created", basicProperties: properties, body: body);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error publishing event: {ex.Message}");
            }
            return Task.CompletedTask;
        }
    }

}
