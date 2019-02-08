using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MyMessenger.Server.Tests
{
	public class TestDbOptions<T>
	{
		public static DbContextOptions<MessengerContext> Options { get; } = new DbContextOptionsBuilder<MessengerContext>()
			.UseLazyLoadingProxies()
			.UseInMemoryDatabase(typeof(T).Name)
			.Options;
	}

	public class TestDbOptions
	{
		public static DbContextOptions<MessengerContext> NewGuidOptions => new DbContextOptionsBuilder<MessengerContext>()
			.UseLazyLoadingProxies()
			.UseInMemoryDatabase(Guid.NewGuid().ToString())
			.Options;
	}
}
