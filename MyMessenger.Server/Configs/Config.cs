using Newtonsoft.Json;

namespace MyMessenger.Server.Configs
{
	[JsonObject]
	public class Config
	{
		[JsonProperty]
		public DbConfig DbConfig { get; set; }
	}
}