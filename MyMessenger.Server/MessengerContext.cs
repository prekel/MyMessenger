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
		public DbSet<AccountDialog> AccountsDialogs { get; set; }

		public static Config Config;

		public MessengerContext(Config config)
		{
			Config = config;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder
				.UseLazyLoadingProxies()
				.UseMySql(new ConnectionStringCompiler(Config.DbConfig).Compile());
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<AccountDialog>(entity =>
			{
				entity.HasKey(t => new { t.AccountId, t.DialogId });
				//entity.HasKey(t => t.AccountDialogId);

				entity.HasOne(ad => ad.Account)
					.WithMany(a => a.Dialogs)
					.HasForeignKey(ad => ad.AccountId);

				entity.HasOne(ad => ad.Dialog)
					.WithMany(a => a.Members)
					.HasForeignKey(ad => ad.DialogId);
			});

			modelBuilder.Entity<Account>(entity =>
			{
				entity.HasKey(e => e.AccountId);

				entity.Property(e => e.Nickname).IsRequired();
				entity.Property(e => e.PasswordHash).IsRequired();
				entity.Property(e => e.PasswordSalt).IsRequired();
				entity.Property(e => e.RegistrationDateTime).IsRequired();
				entity.Property(e => e.LoginDateTime).IsRequired();
				//entity.Property(e => e.TimeZone).IsRequired();

				entity.HasMany(e => e.Messages)
					.WithOne(e => e.Author)
					.HasForeignKey(p => p.AuthorId);

				entity.HasMany(e => e.Dialogs)
					.WithOne(e => e.Account)
					.HasForeignKey(p => p.AccountId);
			});

			modelBuilder.Entity<Dialog>(entity =>
			{
				entity.HasKey(e => e.DialogId);

				entity.HasMany(e => e.Messages)
					.WithOne(e => e.Dialog)
					.HasForeignKey(p => p.DialogId);

				entity.HasMany(e => e.Members)
					.WithOne(e => e.Dialog)
					.HasForeignKey(p => p.DialogId);
			});

			modelBuilder.Entity<Message>(entity =>
			{
				entity.HasKey(e => e.MessageId);

				entity.Property(e => e.Text).IsRequired();
				entity.Property(e => e.SendDateTime).IsRequired();

				entity.HasOne(e => e.Author)
					.WithMany(p => p.Messages)
					.HasForeignKey(p => p.AuthorId);
				entity.HasOne(d => d.Dialog)
					.WithMany(p => p.Messages)
					.HasForeignKey(p => p.DialogId);
			});
		}
	}
}