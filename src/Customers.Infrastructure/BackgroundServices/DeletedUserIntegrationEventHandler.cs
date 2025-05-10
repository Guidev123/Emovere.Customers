using Customers.Application.Commands.Customers.Delete;
using Emovere.Communication.IntegrationEvents;
using Emovere.Infrastructure.Bus;
using Emovere.SharedKernel.Abstractions.Mediator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Customers.Infrastructure.BackgroundServices
{
    public sealed class DeletedUserIntegrationEventHandler(IMessageBus bus, IServiceProvider serviceProvider) : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetSubscribers();
            return Task.CompletedTask;
        }

        private void SetSubscribers()
        {
            bus.SubscribeAsync<DeletedUserIntegrationEvent>("DeletedUser", HandleDeletedUserIntegrationEvent);
            bus.AdvancedBus.Connected += OnConnect!;
        }

        private void OnConnect(object s, EventArgs e) => SetSubscribers();

        private async Task HandleDeletedUserIntegrationEvent(DeletedUserIntegrationEvent @event)
        {
            using var scope = serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<DeletedUserIntegrationEventHandler>>();

            logger.LogInformation("Starting DeleteCustomerCommand now.");

            var result = await mediator.SendCommand(new DeleteCustomerCommand(@event.UserId)).ConfigureAwait(false);

            if (result.IsSuccess)
            {
                logger.LogInformation("DeleteCustomerCommand completed successfully.");
                return;
            }

            logger.LogError("DeleteCustomerCommand failed with errors: {Errors}", result.Errors);
        }
    }
}