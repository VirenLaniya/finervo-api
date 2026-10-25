using Finervo.Core.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Infrastructure.Persistence.Interceptors
{
    public sealed class PublishDomainEventsInterceptor(IPublisher publisher)
    : SaveChangesInterceptor
    {
        // Must use SavedChangesAsync — AFTER save, not before
        public override async ValueTask<int> SavedChangesAsync(
            SaveChangesCompletedEventData eventData,
            int result,
            CancellationToken ct = default)
        {
            await PublishDomainEventsAsync(eventData.Context, ct);
            return await base.SavedChangesAsync(eventData, result, ct);
        }

        private async Task PublishDomainEventsAsync(
            DbContext? context,
            CancellationToken ct)
        {
            if (context is null) return;

            var aggregates = context.ChangeTracker
                .Entries<AggregateRoot>()
                .Where(e => e.Entity.DomainEvents.Any())
                .Select(e => e.Entity)
                .ToList();

            var domainEvents = aggregates
                .SelectMany(a => a.DomainEvents)
                .ToList();

            // Clear BEFORE publishing — prevent re-publishing
            aggregates.ForEach(a => a.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await publisher.Publish(domainEvent, ct);
            }
        }
    }
}
