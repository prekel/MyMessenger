using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using MyMessenger.Core;
using System;

namespace MyMessenger.Server.Entities
{
	[JsonObject(MemberSerialization.OptIn)]
	public class Message : IMessage
	{
		[JsonProperty]
		public int Id { get; set; }

		[JsonProperty]
		public string Text { get; set; }

		[JsonProperty]
		public IDialog Dialog => Dialog1;

		[JsonProperty]
		public IAccount Author => Author1;

		//[JsonProperty]
		public virtual Dialog Dialog1 { get; set; }
		
		//[JsonProperty]
		public virtual Account Author1 { get; set; }

		public DateTime SendDateTime { get; set; }
	}
}