using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cart.API.Infrastructure.Messaging.Background
{
    public class ConsumerBackground : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ConsumerBackground(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<IConsumerCreatedEvent>(); ;
            await consumer.Consume();
        }
    }
}