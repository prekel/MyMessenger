using System;

namespace MyMessenger.Server.Entities
{
	public class Launch
	{
		public int LaunchId { get; set; }

		public DateTimeOffset LaunchDateTime { get; set; }

		public string User { get; set; }

		public string MachineName { get; set; }

		public int Pid { get; set; }

		public string AssemblyVersion { get; set; }
	}
}