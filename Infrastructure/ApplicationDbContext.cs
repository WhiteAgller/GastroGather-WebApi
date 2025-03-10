﻿using System.Reflection;
using Common;
using Common.BaseEntity;
using Common.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using User.Domain;

namespace Infrastructure;

public class ApplicationDbContext : IdentityDbContext
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTime _dateTime;
    private readonly IDomainEventService _domainEventService;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ICurrentUserService currentUserService,
        IDomainEventService domainEventService,
        IDateTime dateTime)
        : base(options)
    {
        _currentUserService = currentUserService;
        _domainEventService = domainEventService;
        _dateTime = dateTime;
    }

    public DbSet<Product.Domain.Product> Products => Set<Product.Domain.Product>();
    
    public DbSet<User.Domain.User> Users => Set<User.Domain.User>();

    public DbSet<Group> Groups => Set<Group>();
    
    public DbSet<GroupInvite> GroupInvites => Set<GroupInvite>();

    public DbSet<Product.Domain.Category> Categories => Set<Product.Domain.Category>();
    
    public DbSet<Table.Domain.Table> Tables => Set<Table.Domain.Table>();
    public DbSet<Table.Domain.Place> Places => Set<Table.Domain.Place>();
    public DbSet<Order.Domain.Order> Orders => Set<Order.Domain.Order>();
    public DbSet<Order.Domain.OrderItem> OrderItems => Set<Order.Domain.OrderItem>();
    public DbSet<FriendInvite> FriendInvites => Set<FriendInvite>();

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUserService.UserId;
                    entry.Entity.Created = _dateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _currentUserService.UserId;
                    entry.Entity.LastModified = _dateTime.Now;
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    break;
                default:
                    break;
            }
        }
        foreach (var entry in ChangeTracker.Entries<BaseEntity<int>>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = _currentUserService.UserId!;
            }
        }
          
        var events = ChangeTracker.Entries<IHasDomainEvent>()
                .Select(x => x.Entity.DomainEvents)
                .SelectMany(x => x)
                .Where(domainEvent => !domainEvent.IsPublished)
                .ToArray();

        var result = await base.SaveChangesAsync(cancellationToken);

        await DispatchEvents(events);
        await base.SaveChangesAsync(cancellationToken);
        return result;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

    private async Task DispatchEvents(DomainEvent[] events)
    {
        foreach (var @event in events)
        {
            @event.IsPublished = true;
            await _domainEventService.Publish(@event);
        }
    }
}