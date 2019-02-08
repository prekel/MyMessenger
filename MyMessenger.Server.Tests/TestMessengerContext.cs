using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyMessenger.Server.Configs;

namespace MyMessenger.Server.Tests
{
	public class TestMessengerContext : MessengerContext
	{
		public TestMessengerContext() : base(TestDbOptions.NewGuidOptions)
		{
		}
	}
}
