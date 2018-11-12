using Newtonsoft.Json;

namespace MyMessenger.Core.Parameters
{
	[JsonObject]
	public abstract class AbstractParameters
	{
		[JsonProperty]
		public abstract string CommandName { get; set; }
	}
}
