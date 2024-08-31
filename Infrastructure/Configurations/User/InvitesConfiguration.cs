using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain;

namespace Infrastructure.Configurations.User;

public class InvitesConfiguration : IEntityTypeConfiguration<GroupInvite>
{
    public void Configure(EntityTypeBuilder<GroupInvite> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Invites)
            .HasForeignKey(x => x.CreatedBy);
        
        builder
            .HasOne(x => x.Group)
            .WithMany(x => x.GroupInvites)
            .HasForeignKey(x => x.GroupId);

        builder.Property(x => x.InvitationAccepted)
            .HasDefaultValue(false);
    }
}