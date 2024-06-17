    using Microsoft.EntityFrameworkCore;

    namespace Network.models
    {
        internal class ChatContext : DbContext
        {
            public DbSet<User> Users { get; set; }
            public DbSet<Message> Messages { get; set; }

            public ChatContext() { }

            public ChatContext(DbContextOptions context) : base(context)
            {

            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer("Server=localHost; Database=mydb; Trusted_Connection=True")
                    .UseLazyLoadingProxies();
            }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<User>(entity =>
                {
                    entity.ToTable("users");

                    entity.HasKey(x => x.ID).HasName("user_pkey");
                    entity.HasIndex(x => x.FullName).IsUnique();


                    entity.Property(e => e.FullName)
                    .HasColumnName("FullName")
                    .HasMaxLength(255)
                    .IsRequired();
                });
                modelBuilder.Entity<Message>(entity =>
                {
                    entity.ToTable("messages");

                    entity.HasKey(x => x.MessageID).HasName("message_pk");


                    entity.Property(e => e.Text)
                    .HasColumnName("message_text");
                    entity.Property(e => e.DateSend)
                    .HasColumnName("message_data");
                    entity.Property(e => e.IsSent)
                    .HasColumnName("is_sent");
                    entity.Property(e => e.MessageID)
                    .HasColumnName("id");

                    entity.HasOne(x => x.UserTo)
                    .WithMany(m => m.MessagesTo)
                    .HasForeignKey(x => x.UserToId).HasConstraintName("message_to_user_fk"); ;
                    entity.HasOne(x => x.UserFrom)
                    .WithMany(m => m.MessagesFrom)
                    .HasForeignKey(x => x.UserFromId).HasConstraintName("message_from_user_fk");
                });
            }
        }
    }
