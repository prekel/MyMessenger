using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyMessenger.Core.Responses
{
	[JsonObject]
	public class GetAccountByIdResponse : AbstractResponse
	{
		[JsonProperty]
		public IAccount Account { get; set; }
	}
}