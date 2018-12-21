using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyMessenger.Core.Responses
{
	[JsonObject]
	public class GetDialogByIdResponse : AbstractResponse
	{
		[JsonProperty]
		public IDialog Dialog { get; set; }
	}
}