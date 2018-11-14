using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyMessenger.Core.Responses
{
	[JsonObject]
	public class DialogSessionResponse : AbstractResponse
	{
		//[JsonProperty]
		//public IList<IMessage> Content { get; set; }
	}
}