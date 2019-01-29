using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NLog;

using MyMessenger.Server.Configs;
using MyMessenger.Server.Entities;

namespace MyMessenger.Server
{
	public class MessengerContext : DbContext
	{
		private static Logger Log { get; } = LogManager.GetCurrentClassLogger();

		public DbSet<Dialog> Dialogs { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<Account> Accounts { get; set; }
		public DbSet<AccountDialog> AccountsDialogs { get; set; }
		public DbSet<Launch> Launches { get; set; }

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

			modelBuilder.Entity<Launch>(entity =>
			{
				entity.HasKey(e => e.LaunchId);
				entity.Property(e => e.LaunchDateTime).IsRequired();
				entity.Property(e => e.MachineName).IsRequired();
				entity.Property(e => e.Pid).IsRequired();
				entity.Property(e => e.User).IsRequired();
				entity.Property(e => e.AssemblyVersion);
			});
		}

		//public override int SaveChanges()
		//{
		//	Log.Trace("Изменения сохранены в базу данных");
		//	return base.SaveChanges();
		//}

		public override int SaveChanges(bool a)
		{
			Log.Trace("Изменения сохранены в базу данных");
			return base.SaveChanges(a);
		}

		public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
		{
			Log.Trace("Изменения сохранены в базу данных");
			return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}
	}
}