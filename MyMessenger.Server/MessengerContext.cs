using Microsoft.EntityFrameworkCore;
using MyMessenger.Server.Configs;
using MyMessenger.Server.Entities;

namespace MyMessenger.Server
{
	public class MessengerContext : DbContext
	{
		public DbSet<Dialog> Dialogs { get; set; }
		public DbSet<Message> Messages { get; set; }

		public static Config Config;

		public MessengerContext(Config config)
		{
			Config = config;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseMySql(new ConnectionStringCompiler(Config.DbConfig).Compile());
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Dialog>(entity =>
			{
				entity.HasKey(e => e.ID);
				//entity.Property(e => e.).IsRequired();
			});

			modelBuilder.Entity<Message>(entity =>
			{
				entity.HasKey(e => e.ID);
				entity.Property(e => e.Text).IsRequired();
				entity.HasOne(d => d.Dialog)
					.WithMany(p => p.Messages);
			});
		}
	}
}