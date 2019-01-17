using Microsoft.EntityFrameworkCore.Design;
using MyMessenger.Server.Configs;

namespace MyMessenger.Server
{
	public class MessengerContextFactory : IDesignTimeDbContextFactory<MessengerContext>
	{
		public MessengerContext CreateDbContext(string[] args)
		{
			var c = new Config
			{
				DbConfig = new DbConfig
				{
					Server = "localhost",
					Port = 3306,
					Name = "MyMessenger5",
					User = "root",
					SslMode = "none",
					Password = "123456"
				}
			};
			return new MessengerContext(c);
		}
	}
}