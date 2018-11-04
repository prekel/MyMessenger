using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using MyMessenger.Core;

namespace MyMessenger.Server.Entities
{
	[JsonObject(MemberSerialization.OptIn)]
	public class Message : IMessage
	{
		[JsonProperty]
		public int Id { get; set; }

		[JsonProperty]
		public string Text { get; set; }

		public IDialog Dialog => Dialog1;

		public IAccount Author => Author1;

		public virtual Dialog Dialog1 { get; set; }
		
		[JsonProperty]
		public virtual Account Author1 { get; set; }
	}
}