using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
            _channel.ExchangeDeclare(exchange: "v2h.product", type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
            //_channel.QueueDeclare(queue: "album.created.queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            //_channel.QueueBind(queue: "album.created.queue", exchange: "v2h.album", routingKey: "album.created");
        }

        public Task Publish(Domain.Aggregates.Album album)
        {
            var correlationId = Guid.NewGuid().ToString();

            var musicEvent = new AlbumContent
            {
                CorrelationId = correlationId,
                Id = album.Id,
                ProducerId = album.ProducerId,
                AddedAt = album.AddedAt,
                UpdatedAt = album.UpdatedAt,
            };

            var productEvent = new ProductContent
            {
                CorrelationId = correlationId,
                AlbumId = album.Id,
                ProducerId = album.ProducerId,
                Title = album.Title,
                Description = album.Description,
                Price = album.Price,
                ImageUrl = album.ImageUrl
            };

            var musicBody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(musicEvent));
            var productBody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(productEvent));

            var musicProps = _channel.CreateBasicProperties();
            musicProps.CorrelationId = correlationId;

            var productProps = _channel.CreateBasicProperties();
            productProps.CorrelationId = correlationId;

            try
            {
                _channel.BasicPublish(exchange: "v2h.album", routingKey: "album.created", basicProperties: musicProps, body: musicBody);
                _channel.BasicPublish(exchange: "v2h.product", routingKey: "album.created", basicProperties: productProps, body: productBody);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error publishing event: {ex.Message}");
            }
            return Task.CompletedTask;
        }
    }
}
