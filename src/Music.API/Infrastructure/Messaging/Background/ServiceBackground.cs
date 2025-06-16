using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music.API.Infrastructure.Messaging.Background
{

    public class ServiceBackground : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ServiceBackground(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<IProducerCreatedEvent>();
            await consumer.Consume();
        }
    }
}
