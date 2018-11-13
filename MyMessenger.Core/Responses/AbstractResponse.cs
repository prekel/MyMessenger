using Newtonsoft.Json;

namespace MyMessenger.Core.Responses
{
	[JsonObject]
	public abstract class AbstractResponse
	{
		[JsonProperty]
		public ResponseCode Code { get; set; }
	}
}