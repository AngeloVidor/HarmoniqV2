using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Music.API.Infrastructure.Messaging.Album;

namespace Music.API.Infrastructure.Messaging.Background
{
    public class AlbumCreatedConsumerBackground : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public AlbumCreatedConsumerBackground(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<IAlbumCreatedEvent>();
            await consumer.Consume();
        }
    }
}