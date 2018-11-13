using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyMessenger.Core.Responses
{
	[JsonObject]
	public class CreateDialogResponse : AbstractResponse
	{
		[JsonProperty]
		public int DialogId { get; set; }
	}
}