using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notes.Core.Models;
using Notes.DataAccess.Entities;

namespace Notes.DataAccess.Configurations;

public class UsersConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.FirstName)
            .HasMaxLength(User.MaxFirstNameLength)
            .IsRequired();

        builder.Property(u => u.LastName)
            .HasMaxLength(User.MaxLastNameLength)
            .IsRequired();

        builder.Property(u => u.Email)
            .HasMaxLength(User.MaxEmailLength)
            .IsRequired();

        builder.Property(u => u.PasswordHash)
            .IsRequired();
    }
}