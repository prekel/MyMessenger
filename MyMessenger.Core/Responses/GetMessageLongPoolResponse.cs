using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyMessenger.Core.Responses
{
	public class GetMessageLongPoolResponse : AbstractResponse
	{
		[JsonProperty]
		public IMessage Content { get; set; }
	}
}