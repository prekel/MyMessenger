using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using MyMessenger.Core;

namespace MyMessenger.Server.Entities
{
	[JsonObject]
	public class Message : IMessage
	{
		[JsonProperty]
		public int Id { get; set; }

		[JsonProperty]
		public string Text { get; set; }

		[JsonIgnore]
		public virtual IDialog Dialog { get; set; }
		
		[JsonIgnore]
		public virtual IAccount Author { get; set; }
	}
}