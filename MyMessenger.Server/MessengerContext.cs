using Microsoft.EntityFrameworkCore;
using MyMessenger.Server.Configs;
using MyMessenger.Server.Entities;

namespace MyMessenger.Server
{
	public class MessengerContext : DbContext
	{
		public DbSet<Dialog> Dialogs { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<Account> Accounts { get; set; }

		public static Config Config;

		public MessengerContext(Config config)
		{
			Config = config;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder
				//.UseLazyLoadingProxies()
				.UseMySql(new ConnectionStringCompiler(Config.DbConfig).Compile());
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Account>(entity =>
			{
				entity.HasKey(e => e.ID);
				entity.Property(e => e.Nickname).IsRequired();
				entity.Property(e => e.PasswordHash).IsRequired();
				entity.Property(e => e.PasswordSalt).IsRequired();

				entity.HasMany(e => e.Dialogs);
				entity.HasMany(e => e.Messages);
			});

			modelBuilder.Entity<Dialog>(entity =>
			{
				entity.HasKey(e => e.ID);

				entity.HasOne(e => e.FirstMember)
					.WithMany(e => e.Dialogs);
				entity.HasOne(e => e.FirstMember)
					.WithMany(e => e.Dialogs);
				entity.HasMany(e => e.Messages);
			});

			modelBuilder.Entity<Message>(entity =>
			{
				entity.HasKey(e => e.ID);
				entity.Property(e => e.Text).IsRequired();

				entity.HasOne(e => e.Author)
					.WithMany(p => p.Messages);
				entity.HasOne(d => d.Dialog)
					.WithMany(p => p.Messages);
			});
		}
	}
}