using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.User;

public class UserConfiguration : IEntityTypeConfiguration<global::User.Domain.User>
{
    public void Configure(EntityTypeBuilder<global::User.Domain.User> builder)
    {
        builder.Ignore(x => x.DomainEvents);
        builder
            .HasMany(x => x.Invites)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.CreatedBy);

        builder
            .HasMany(x => x.FriendInvitesSent)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.CreatedBy);
        
        builder
            .HasMany(x => x.FriendInvitesReceived)
            .WithOne(x => x.Friend)
            .HasForeignKey(x => x.FriendsUsername);
        
        builder
            .HasMany(x => (IEnumerable<global::Order.Domain.Order>)x.Orders)
            .WithOne(x => (global::User.Domain.User?)x.User)
            .HasForeignKey(x => x.UserId);
    }
}