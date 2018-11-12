using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using MyMessenger.Core;

namespace MyMessenger.Server.Entities
{
	[JsonObject(MemberSerialization.OptIn)]
	public class Account : IAccount
	{
		[JsonProperty]
		public int Id { get; set; }
		[JsonProperty]
		public string Nickname { get; set; }

		[MaxLength(32)]
		public byte[] PasswordHash { get; set; }
		public int PasswordSalt { get; set; }

		//[JsonProperty]
		public virtual IList<Dialog> Dialogs { get; set; }
		//[JsonProperty]
		public virtual IList<Message> Messages { get; set; }
	}
}