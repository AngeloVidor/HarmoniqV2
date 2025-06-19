using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Product.API.Domain.Aggregates;
using Product.API.Domain.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Product.API.Infrastructure.Messaging
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

            _channel.ExchangeDeclare(exchange: "v2h.product", type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
            _channel.QueueDeclare(queue: "album.product.created.queue", durable: true, exclusive: false, autoDelete: false);
            _channel.QueueBind(queue: "album.product.created.queue", exchange: "v2h.product", routingKey: "album.created");

            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task Consume()
        {
            Console.WriteLine("Listening!!");
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var requestBody = Encoding.UTF8.GetString(body);
                var request = JsonConvert.DeserializeObject<ProductContent>(requestBody);
                try
                {
                    if (request != null)
                    {
                        using (var scope = _serviceScopeFactory.CreateScope())
                        {
                            var productRepository = scope.ServiceProvider.GetRequiredService<IAlbumProductRepository>();
                            var product = new AlbumProduct(request.AlbumId, request.ProducerId, request.Title, request.Description, request.Price, request.ImageUrl);
                            await productRepository.SaveAsync(product);
                            _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing the message: {ex.Message}");
                    _channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: false);
                }
            };
            _channel.BasicConsume(queue: "album.product.created.queue", autoAck: false, consumer: consumer);
            return Task.CompletedTask;
        }
    }
}
