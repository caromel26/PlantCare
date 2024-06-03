using Microsoft.EntityFrameworkCore;

namespace PlantCare.API.Models.Context
{
    public partial class PlantCareContext : DbContext
    {
        public PlantCareContext() { }

        public PlantCareContext(DbContextOptions<PlantCareContext> options) : base(options) { }

        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<Note> Notes { get; set; } = null!;
        public virtual DbSet<Plant> Plants { get; set; } = null!;
        public virtual DbSet<PlantTag> PlantTags { get; set; } = null!;
        public virtual DbSet<PlantTask> PlantTasks { get; set; } = null!;
        public virtual DbSet<Species> Species { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<TaskType> TaskTypes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Reminder> Reminders { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(255);
            });

            modelBuilder.Entity<Species>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
            });

            modelBuilder.Entity<Plant>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Plants)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Species)
                    .WithMany(p => p.Plants)
                    .HasForeignKey(d => d.SpeciesId);
            });

            modelBuilder.Entity<PlantTag>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.PlantTags)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.PlantTags)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ImageUrl).HasMaxLength(1000);

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<TaskType>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
            });

            modelBuilder.Entity<PlantTask>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DueDate).IsRequired();
                entity.Property(e => e.CompletionStatus).IsRequired();

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.PlantTasks)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TaskType)
                    .WithMany(p => p.PlantTasks)
                    .HasForeignKey(d => d.TaskTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Reminder>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ReminderDate).IsRequired();
                entity.Property(e => e.IsCompleted).IsRequired();

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.Reminders)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
