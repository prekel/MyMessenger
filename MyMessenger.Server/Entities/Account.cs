using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using MyMessenger.Core;

namespace MyMessenger.Server.Entities
{
	[JsonObject]
	public class Account : IAccount
	{
		[JsonIgnore]
		public int Id { get; set; }
		[JsonProperty]
		public string Nickname { get; set; }

		[MaxLength(32)]
		[JsonIgnore]
		public byte[] PasswordHash { get; set; }
		[JsonIgnore]
		public int PasswordSalt { get; set; }

		[JsonIgnore]
		public virtual IList<Dialog> Dialogs { get; set; }
		[JsonIgnore]
		public virtual IList<Message> Messages { get; set; }
	}
}