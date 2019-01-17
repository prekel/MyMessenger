using Newtonsoft.Json;

using MyMessenger.Core;
using System.Collections.Generic;

namespace MyMessenger.Client
{
	[JsonObject]
	public class Dialog : IDialog
	{
		[JsonProperty]
		public int DialogId { get; set; }

		[JsonProperty]
		public IEnumerable<int> MembersIds { get; }
	}
}