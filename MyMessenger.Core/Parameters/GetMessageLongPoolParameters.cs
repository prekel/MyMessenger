using Newtonsoft.Json;

namespace MyMessenger.Core.Parameters
{
	public class GetMessageLongPoolParameters : AbstractParameters
	{
		public override CommandType CommandName { get; set; } = CommandType.GetMessageLongPool;

		[JsonProperty] 
		public int DialogId { get; set; }

		[JsonProperty] 
		public string Token { get; set; }
	}
}