using Newtonsoft.Json;

namespace MyMessenger.Core.Parameters
{
	[JsonObject]
	public abstract class AbstractParameters
	{
		[JsonProperty]
		public abstract CommandType CommandName { get; set; }
	}
}
