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
		public IDialog Dialog => Dialog1;

		[JsonIgnore]
		public IAccount Author => Author1;

		[JsonIgnore]
		public virtual Dialog Dialog1 { get; set; }
		
		[JsonIgnore]
		public virtual Account Author1 { get; set; }
	}
}