using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain;

namespace Infrastructure.Configurations.User;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        //builder.Ignore(x => x.DomainEvents);
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(25).IsRequired();
        builder.Property(x => x.MaxNumberOfPeople).HasDefaultValue(1);
        builder.Property(x => x.AdminUserId).IsRequired();

        builder
            .HasMany(x => x.Invites)
            .WithOne(x => x.Group)
            .HasForeignKey(x => x.GroupId);

        builder
            .HasMany(x => (IEnumerable<global::Table.Domain.Table>)x.Tables)
            .WithOne(x => (Group)x.Group)
            .HasForeignKey(x => x.GroupId);
    }
}