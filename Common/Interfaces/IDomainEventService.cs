﻿namespace Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}