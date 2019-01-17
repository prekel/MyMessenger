using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

using MyMessenger.Server.Configs;
using MyMessenger.Server.Entities;

namespace MyMessenger.Server
{
	public class ProgramMigrate
	{
		public static Config Config;

		public static void Main(Config config)
		{
			Config = config;

			using (var context = new MessengerContext(Config))
			{
				context.Database.Migrate();
			}
		}
	}
}
