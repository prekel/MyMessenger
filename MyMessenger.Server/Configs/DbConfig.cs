using System.Net;
using System.Security;

using Newtonsoft.Json;

namespace MyMessenger.Server.Configs
{
	[JsonObject]
	public class DbConfig
	{
		[JsonProperty]
		public string Server { get; set; }
		[JsonProperty]
		public int Port { get; set; }
		[JsonProperty]
		public string Name { get; set; }
		[JsonProperty]
		public string User { get; set; }
		[JsonProperty]
		public string SslMode { get; set; } = "none";
		[JsonIgnore]
		public string Password { get; set; }
	}
}