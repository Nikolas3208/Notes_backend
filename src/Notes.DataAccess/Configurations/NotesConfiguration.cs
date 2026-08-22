using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notes.Core.Models;
using Notes.DataAccess.Entities;

namespace Notes.DataAccess.Configurations;

public class NotesConfiguration : IEntityTypeConfiguration<NoteEntity>
{
    public void Configure(EntityTypeBuilder<NoteEntity> builder)
    {
        builder.HasKey(n => n.Id);

        builder.Property(n => n.OwnerId)
            .IsRequired();

        builder.Property(n => n.Title)
            .HasMaxLength(Note.MaxTitleLength)
            .IsRequired();

        builder.Property(n => n.Text)
            .IsRequired();

        builder.Property(n => n.Created)
            .IsRequired();
    }
}